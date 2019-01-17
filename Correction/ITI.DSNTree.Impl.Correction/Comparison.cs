using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class Comparison
	{
		public static void CompareDictionary(Dictionary<string, string> newDict, Dictionary<string, string> oldDict, List<DataItem> dataItems, bool includUnchanged = false)
		{
			bool changed = true;
			DataItem dataItem = null;
			List<string> all = new List<string>(newDict.Keys);
			all = all.Union(oldDict.Keys).ToList();
			all.Sort();
			foreach (var k in all)
			{
				changed = true;
				dataItem = new DataItem { Key = k };
				if (newDict.ContainsKey(k))
				//inserted or modified
				{
					if (oldDict.ContainsKey(k))
					//modified or unchanged
					{
						if (string.Compare(newDict[k], oldDict[k]) != 0)
						//modified
						{
							dataItem.Status = ChangeStatus.Modified;
						}
						else
						//Unchanged
						{
							changed = false;
							dataItem.Status = ChangeStatus.Unchanged;
						}
						dataItem.Value = newDict[k];
						dataItem.OldValue = oldDict[k];
					}
					else
					//Inserted
					{
						dataItem.Status = ChangeStatus.Inserted;
						dataItem.Value = newDict[k];
					}
				}
				else
				//Deleted
				{
					dataItem.Status = ChangeStatus.Deleted;
					dataItem.OldValue = oldDict[k];
				}
				if (changed || includUnchanged)
					dataItems.Add(dataItem);
			}
		}

		public static void TextDiff(string newText, string oldText, List<DataItem> dataItems, bool includUnchanged = false)
		{
			if (string.IsNullOrEmpty(newText) || string.IsNullOrEmpty(newText))
				return;

			Dictionary<int, DataItem> deletedItems = new Dictionary<int, DataItem>();
			DataItem dataItem = null;

			var builder = new InlineDiffBuilder(new Differ());
			var result = builder.BuildDiffModel(oldText, newText);
			DiffPiece diffPiece = null;
			int pos = -1;
			foreach (var diff in result.Lines)
			{
				pos++;
				string[] kv = diff.Text.Split(',');
				if (kv.Length == 1)
					continue;
				kv[1] = kv[1].Substring(1, kv[1].Length - 2);
				kv[1] += "|" + diff.Position + "|" + pos;
				dataItem = new DataItem { Key = kv[0] };
				switch (diff.Type)
				{
					case ChangeType.Inserted:
						diffPiece = result.Lines[(int)diff.Position - 1];
						string[] cv = diffPiece.Text.Split(',');
						if ((diffPiece.Type == ChangeType.Deleted)
							&& (string.Compare(dataItem.Key, cv[0]) == 0))
						{
							dataItem.Value = kv[1];
							dataItem.OldValue = cv[1].Substring(1, cv[1].Length - 2);
							dataItem.Status = ChangeStatus.Modified;
							dataItems.Add(dataItem);
							//dataItems.Remove(deletedItems[(int)diff.Position - 1]);
						}
						else
						{
							dataItem.Status = ChangeStatus.Inserted;
							dataItem.Value = kv[1];
							dataItems.Add(dataItem);
						}
						break;
					case ChangeType.Deleted:
						dataItem.OldValue = kv[1];
						dataItem.Status = ChangeStatus.Deleted;
						dataItems.Add(dataItem);
						deletedItems.Add(pos, dataItem);
						break;
					default:
						if (includUnchanged)
						{
							dataItem.Value = kv[1];
							dataItem.Status = ChangeStatus.Unchanged;
							dataItems.Add(dataItem);
						}
						break;
				}
			}
		}

	}
}

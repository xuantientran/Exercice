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

			Dictionary<int, DataItem> modifiedItems = new Dictionary<int, DataItem>();
			DataItem dataItem = null;

			var builder = new InlineDiffBuilder(new Differ());
			var result = builder.BuildDiffModel(oldText, newText);
			DataItem modifiedItem = null;
			DiffPiece diffPiece = null;

			//the current line index
			int pos = -1;
			//the number of modifications done
			int modiToBeDone = 0;
			int modiDone = 0;
			bool lastmodified = false;
			foreach (var diff in result.Lines)
			{
				pos++;
				string[] kv = diff.Text.Split(',');
				if (kv.Length == 1)
					continue;
				kv[1] = kv[1].Substring(1, kv[1].Length - 2);
				kv[1] += "|" + (diff.Position -1) + "|" + pos;
				dataItem = new DataItem { Key = kv[0] };
				switch (diff.Type)
				{
					case ChangeType.Inserted:
						dataItem.Status = ChangeStatus.Inserted;
						dataItem.Value = kv[1];
						dataItems.Add(dataItem);

						lastmodified = true;
						break;
					case ChangeType.Deleted:
						dataItem.OldValue = kv[1];
						dataItem.Status = ChangeStatus.Deleted;
						dataItems.Add(dataItem);
						modifiedItems.Add(pos, dataItem);
						lastmodified = false;
						break;
					default:
						if (includUnchanged)
						{
							dataItem.Value = kv[1];
							dataItem.Status = ChangeStatus.Unchanged;
							dataItems.Add(dataItem);
						}
						lastmodified = false;
						break;
				}
			}
		}

	}
}

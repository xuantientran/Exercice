using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using System;
using System.Collections.Generic;
using System.IO;
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


		public static int TextDiff(string newText, string oldText, List<DataItem> dataItems, bool includUnchanged = false)
		{
			if (string.IsNullOrEmpty(newText) || string.IsNullOrEmpty(oldText))
				return 0;

			Dictionary<int, DataItem> modis = new Dictionary<int, DataItem>();

			DataItem dataItem = null;

			var builder = new InlineDiffBuilder(new Differ());
			var result = builder.BuildDiffModel(oldText, newText);
			int modiPos;
			int pos = 0;
			foreach (var diff in result.Lines)
			{
				pos++;
				string[] kv = diff.Text.Split(',');
				if (kv.Length <= 1)
					continue;
				kv[1] = kv[1].Substring(1, kv[1].Length - 2);
				//kv[1] += "|" + diff.Position + "|" + pos;
				dataItem = new DataItem { Key = kv[0] };
				switch (diff.Type)
				{
					case ChangeType.Inserted:
						pos = (int)diff.Position;
						modiPos = pos;
						if (modis.ContainsKey(modiPos))
						{
							modis[modiPos].Value = kv[1];
							modis[modiPos].Status = ChangeStatus.Modified;
						}
						else
						{
							dataItem.Status = ChangeStatus.Inserted;
							dataItem.Value = kv[1];
							modis[pos] = dataItem;
						}
						break;
					case ChangeType.Deleted:
						dataItem.OldValue = kv[1];
						dataItem.Status = ChangeStatus.Deleted;
						modis[pos] = dataItem;
						break;
					default:
						pos = (int)diff.Position;
						if (includUnchanged)
						{
							dataItem.Value = kv[1];
							dataItem.OldValue = kv[1];
							dataItem.Status = ChangeStatus.Unchanged;
							modis[pos] = dataItem;
						}
						break;
				}
			}
			foreach (var m in modis)
			{
				dataItems.Add(m.Value);
			}
			return modis.Count;
		}

	}
}

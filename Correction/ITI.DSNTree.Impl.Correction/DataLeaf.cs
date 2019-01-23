using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class DataLeaf : IDataLeaf
	{
		IDataBlock _block;
		Dictionary<string, string> _data;

		public DataLeaf(IDataBlock block, Dictionary<string, string> data)
		{
			_block = block;
			_data = data;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (var kv in _data)
				sb.AppendLine(kv.Key + ",'" + kv.Value + "'");
			return sb.ToString();
		}

		public Dictionary<string, string> Data { get => _data; set => _data = value; }
		public IDataBlock Block { get => _block; set => _block = value; }
	}
}
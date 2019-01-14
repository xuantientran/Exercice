using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DATATree
{
	public class Leaf : ILeaf
	{
		IBlock _iblock;
		Dictionary<string, string> _leafData;
		public Dictionary<string, string> LeafData { get => _leafData; set => _leafData = value; }
		public IBlock Bloc { get => _iblock; set => _iblock = value; }
	}
}

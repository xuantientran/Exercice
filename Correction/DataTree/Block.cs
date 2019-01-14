using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DATATree
{
	public class Block : IBlock
	{
		string _id;
		List<ILeaf> _leaves;
		public string Id { get => _id; set => _id = value; }
		public List<ILeaf> Leaves { get => _leaves; set => _leaves = value; }
	}
}

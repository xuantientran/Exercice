using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class DataBlock : IDataBlock
	{
		string _id;
		IDataBlock _parent;
		List<IDataBlock> _children;
		List<IDataLeaf> _leaves;

		public DataBlock(string id)
		{
			_id = id;
			_children = new List<IDataBlock>();
			_leaves = new List<IDataLeaf>();
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			IDataBlock current = this;
			Stack<IDataBlock> stack = new Stack<IDataBlock>();
			stack.Push(current);
			while (stack.Count > 0)
			{
				current = stack.Pop();

				for (var i = current.Children.Count - 1; i >= 0; i--)
					stack.Push(current.Children[i]);

				foreach (var leaf in current.Leaves)
					sb.Append(leaf.ToString());
			}
			return sb.ToString();
		}


		public string Id { get => _id; set => _id = value; }
		public IDataBlock Parent { get => _parent; set => _parent = value; }
		public List<IDataLeaf> Leaves { get => _leaves; set => _leaves = value; }
		public List<IDataBlock> Children { get => _children; set => _children = value; }
	}
}

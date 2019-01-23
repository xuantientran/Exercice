using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class DsnNode : IDsnNode
	{
		string _id;
		int _level;
		DsnCardinality _cardinality;
		string _label;
		IDsnNode _parent;
		List<IDsnNode> _children;

		public DsnNode(string id, IDsnNode parent, string label, int level, DsnCardinality cardinality)
		{
			_id = id;
			_parent = parent;
			_label = label;
			_level = level;
			_cardinality = cardinality;
			_children = new List<IDsnNode>();
			if (_parent != null)
				_parent.Children.Add(this);
		}

		public string Id => _id;

		public int Level => _level;

		public DsnCardinality Cardinality => _cardinality;

		public string Label => _label;

		public IDsnNode Parent => _parent;

		public List<IDsnNode> Children { get => _children; }

		public bool CheckCardinality()
		{
			throw new NotImplementedException();
		}
	}
}
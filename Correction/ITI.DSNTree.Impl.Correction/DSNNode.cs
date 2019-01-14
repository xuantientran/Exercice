using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class DSNNode : IDSNNode
	{
		string _id;
		int _level;
		DSNCardinality _cardinality;
		string _label;
		IDSNNode _parent;
		List<IDSNNode> _children;

		public DSNNode(string id, IDSNNode parent, string label, int level, DSNCardinality cardinality)
		{
			_id = id;
			_parent = parent;
			_label = label;
			_level = level;
			_cardinality = cardinality;
			_children = new List<IDSNNode>();
			if (_parent != null)
				_parent.Children.Add(this);
		}

		public string Id => _id;

		public int Level => _level;

		public DSNCardinality Cardinality => _cardinality;

		public string Label => _label;

		public IDSNNode Parent => _parent;

		public List<IDSNNode> Children { get => _children;}

		public bool CheckCardinality()
		{
			throw new NotImplementedException();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public interface IDSNTree
	{
		/// <summary>
		/// Gets the total number of nodes.
		/// </summary>
		int Count { get; }

		/// <summary>
		/// Gets the root of the tree
		/// </summary>
		IDSNNode Root { get; }

		/// <summary>
		/// Get a node by its id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		IDSNNode Find(string id);

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public interface IDsnTree
	{
		/// <summary>
		/// Gets the total number of nodes.
		/// </summary>
		int Count { get; }

		/// <summary>
		/// Gets the root of the tree
		/// </summary>
		IDsnNode Root { get; }

		/// <summary>
		/// Get a node by its id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		IDsnNode Find(string id);

	}
}

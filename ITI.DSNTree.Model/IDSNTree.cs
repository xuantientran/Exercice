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

		//On a besoin d'une racine pour prendre arbre
		IDsnNode Root { get; }

		//On récupère un noeud par son id en le cherchant dans le dictionnaire
		IDsnNode Find(string id);

	}
}

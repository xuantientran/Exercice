using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public interface IDSNNode
	{
		/// <summary>
		/// Gets the node identifier.
		/// Example: "S10.G01.00"
		/// </summary>
		string Id { get; }

		//S00.G00.00 - Racine(1,1)								<= niveau 0
		//	S10.G01.00 - Emetteur(1,1)						<= niveau 1
		//		S10.G01.01 - Contacts Emetteur(1,*)	<= niveau 2
		int Level { get; }

		//S10.G01.01 - Contacts Emetteur(1,*)
		//le parent S00.G00.00 a au moins 1 instance du noeud S10.G01.01 (InsMin 1)
		//et plusieurs instance du noeud S10.G01.01 (InsMax = 0)
		DSNCardinality Cardinality { get; }

		//libellé du noeud S10.G01.00 est "Emetteur"
		string Label { get; }

		/// <summary>
		/// Gets the parent node.
		/// Null when this is the root node.
		/// </summary>
		IDSNNode Parent { get; }

		/// <summary>
		/// Gets the immutable list of children.
		/// Must never be null, but may be empty.
		/// </summary>
		IReadOnlyList<IDSNNode> Children { get; set; }

		/// <summary>
		/// Must check whether the <see cref="Cardinality"/> is correct regarding the 
		/// count of <see cref="Children"/>.
		/// </summary>
		/// <returns>True if the cardinality is correct. False if the cardinality doesn't match.</returns>
		bool CheckCardinality();
	}
}

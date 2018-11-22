using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbre
{
	public interface INoeud
	{
		//S10.G01.00
		string Id { get; }

		//S00.G00.00 - Racine(1,1)								<= niveau 0
		//	S10.G01.00 - Emetteur(1,1)						<= niveau 1
		//		S10.G01.01 - Contacts Emetteur(1,*)	<= niveau 2
		int Niveau { get; set; }

		//S10.G01.01 - Contacts Emetteur(1,*)
		//le parent S00.G00.00 a au moins 1 instance du noeud S10.G01.01 (InsMin 1)
		//et plusieurs instance du noeud S10.G01.01 (InsMax = 0)
		int InsMin { get; set; }
		int InsMax { get; set; }

		//libellé du noeud S10.G01.00 est "Emetteur"
		string Libelle { get; set; }

		//un noeud peut avoir un ou zéro parent
		INoeud Parent { get; set; }

		//un noeud peut avoir zéro ou plusieurs enfant
		List<INoeud> Enfants { get; set; }
	}
}

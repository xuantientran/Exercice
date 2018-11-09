using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbre
{
	public interface INoeud
	{
		string Id { get; }
		int Niveau { get; set; }
		int InsMin { get; set; }
		int InsMax { get; set; }
		string Libelle { get; set; }
		INoeud Parent { get; set; }
		List<INoeud> Enfants { get; set; }
	}
}

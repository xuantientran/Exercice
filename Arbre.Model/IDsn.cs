using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbre
{
	public interface IDsn
	{
		Dictionary<string, INoeud> NoeudBlocs { get; set; }
		INoeud NoeudBlocRacine { get; set; }
	}
}

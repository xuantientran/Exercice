using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbre
{
	public class Dsn : IDsn
	{
		public Dictionary<string, INoeud> NoeudBlocs { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public INoeud NoeudBlocRacine { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public INoeud ObtenirNoeud(string id)
		{
			throw new NotImplementedException();
		}
	}
}

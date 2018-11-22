using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbre
{
	public class Dsn : IDsn
	{
		StringBuilder _log;

		public void JoindreLog(string entree) => _log.AppendLine(entree);

		public void EcrireLog(string fichierLog = "DsnLog.txt")
		{
			using (StreamWriter ecriture = new StreamWriter(fichierLog, false))
			{
				ecriture.Write(_log.ToString());
				_log.Clear();
			}
		}

		public Dictionary<string, INoeud> NoeudBlocs { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public INoeud NoeudBlocRacine { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public INoeud ObtenirNoeud(string id)
		{
			throw new NotImplementedException();
		}


	}
}

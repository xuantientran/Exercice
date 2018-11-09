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
		Dictionary<string, INoeud> _noeuBlocs;
		INoeud _noeuBlocRacine;
		StringBuilder _log;

		public Dsn()
		{
			_noeuBlocs = new Dictionary<string, INoeud>();
			_log = new StringBuilder();
		}

		public Dictionary<string, INoeud> NoeudBlocs { get => _noeuBlocs; set => _noeuBlocs = value; }
		public INoeud NoeudBlocRacine { set => _noeuBlocRacine = value; get => _noeuBlocRacine; }

		public string LibelleBloc(string cle)
		{
			_noeuBlocs.TryGetValue(cle, out INoeud bloc);
			if (bloc != null)
				return bloc.Libelle;
			else
				return "";
		}

		public void JoindreLog(string entree) => _log.AppendLine(entree);

		public void EcrireLog(string fichierLog = "DsnLog.txt")
		{
			using (StreamWriter ecriture = new StreamWriter(fichierLog, false))
			{
				ecriture.Write(_log.ToString());
				_log.Clear();
			}
		}

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbre
{
	public interface IDsn
	{
		//Chaque noeud a un identifant unique, exemple S10.G01.00
		//tous les noeuds se trouvent dans le dictionnaire identifiés par leurs id
		Dictionary<string, INoeud> NoeudBlocs { get; set; }

		//On a besoin d'une racine pour prendre arbre
		INoeud NoeudBlocRacine { get; set; }

		//On récupère un noeud par son id en le cherchant dans le dictionnaire
		INoeud ObtenirNoeud(string id);

		//Joindre une chaine au StringBuilder
		void JoindreLog(string chaine);

		//Ecrire StringBuilder sur un fichier et vider le StringBuilder
		void EcrireLog(string fichier);

	}
}

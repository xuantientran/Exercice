using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Arbre
{
	public class Utilitaire
	{
		public static bool ChargerBlocs(Dsn dsn)
		{
			string dossier = @"..\..\..\..\";
			//string dossier = @"..\..\..\..\..\";
			string fichierEnvoi = dossier + @"Donnee\Envoi.txt";
			string fichierEnvoiTotaux = dossier + @"Donnee\Envoi Totaux.txt";
			string fichierArbre = dossier + @"Donnee\DADSU COMPLETE.txt";
			string exp = @"(.+)\s+\(([0-9]),([0-9]|\*)\)$";
			//On trouve toujours le parent au sommet de la pile
			Stack<INoeud> pileParents = new Stack<INoeud>();
			INoeud noeuBloc;
			INoeud parent;
			//Il faut ajouter la racine artificielement
			INoeud noeuBlocRacine = new NoeudBloc("S00.G00.00") { Libelle = "Racine", Niveau = 0, Parent = null, InsMax = 1, InsMin = 1 };
			dsn.NoeudBlocRacine = noeuBlocRacine;
			dsn.NoeudBlocs.Add(noeuBlocRacine.Id, noeuBlocRacine);

			noeuBloc = noeuBlocRacine;
			parent = noeuBlocRacine;

			//Toutes les rubriques se trouvent dans le fichiers envoi doivent augement un niveau
			int niveauBase = 1;
			int dernierNiveau = 0;
			string ligne;
			int nligne = 0;
			int niveau = 0;
			//Traitement de la S10
			//, Encoding.GetEncoding("iso-8859-1")
			using (StreamReader lecture = new StreamReader(fichierEnvoi))
			{
				while ((ligne = lecture.ReadLine()) != null)
				{
					nligne++;
					//On ne traite pas les lignes vides
					if (string.IsNullOrEmpty(ligne))
						continue;

					string[] cv = ligne.Split('-');

					//On ne traite pas les lignes sans avoir le caratère '-'
					if (cv.Length == 1)
						continue;

					//On compte le niveau de la rubrique
					niveau = 0;
					for (int i = 0; i < cv[0].Length; i++)
					{
						if (cv[0][i] == '\t')
							niveau = i + 1;
						else
							break;
					}
					//Le niveau doit être absolu
					niveau += niveauBase;

					//On enlève tous les caratères \t pour trouver la rubrique
					string cle = cv[0].Trim();

					//On récupère le reste comme valeur
					cv[0] = "";
					string valeur = string.Join("-", cv).Substring(1).Trim();

					//la rubrique doit se trouver dans le dictionaire
					//alimanté des rubriques auparavant
					//if (dsn.NoeudBlocs.ContainsKey(cle))
					//	continue;

					int sautArriere = dernierNiveau - niveau;

					//si le niveau augmente par rapport au dernier niveau
					//Alors on met la dernier bloc dans la pile des parents
					//S10.G01.00 - Emetteur (1,1)
					//	S10.G01.01 - Contacts Emetteur(1, *)
					//sautArriere = -1
					if (sautArriere < 0)
					{
						parent = noeuBloc;
						pileParents.Push(parent);
					}
					//Si le niveau déminue alors enlève autant de parents que néscessaire
					//pour trouver le bon parent.
					//			S70.G10.15 - Rémunérations (0,*)
					//	S80.G01.00 - Identification INSEE des établissements(1, *)
					//sautArriere = 2;
					else if (sautArriere > 0)
					{
						for (int i = 0; i < sautArriere; i++)
						{
							parent = pileParents.Pop();
						}
						parent = (NoeudBloc)parent.Parent;
					}

					//on crée un noeu et spécise son parent
					noeuBloc = new NoeudBloc(cle, parent);

					//On cherche les informations dans la ligne pour alimenter le noeu
					Match m = Regex.Match(valeur, exp);
					if (m.Success)
					{
						noeuBloc.Libelle = m.Groups[1].Value;
						noeuBloc.InsMin = int.Parse(m.Groups[2].Value);
						if (string.Compare(m.Groups[3].Value, "*") == 0)
							noeuBloc.InsMax = 0;
						else
							noeuBloc.InsMax = int.Parse(m.Groups[3].Value);
					}
					//On ajoute le noeu dans le dictionaire
					dsn.NoeudBlocs.Add(cle, noeuBloc);

					dernierNiveau = niveau;
				}
			}
			niveauBase = 2;
			nligne = 0;
			//Traitement du corps
			using (StreamReader lecture = new StreamReader(fichierArbre))
			{
				while ((ligne = lecture.ReadLine()) != null)
				{
					nligne++;
					//On ne traite pas les lignes vides
					if (string.IsNullOrEmpty(ligne))
						continue;
					string[] cv = ligne.Split('-');

					//On ne traite pas les lignes sans avoir le caratère '-'
					if (cv.Length == 1)
						continue;

					//On compte le niveau de la rubrique
					niveau = 0;
					for (int i = 0; i < cv[0].Length; i++)
					{
						if (cv[0][i] == '\t')
							niveau = i + 1;
						else
							break;
					}
					//On traite la partie S20.G00.05 qui est supérieux que niveau de base
					niveau += niveauBase;

					//On elève tous les caratères \t pour trouver la rubrique
					string cle = cv[0].Trim();
					cv[0] = "";
					//On récupère le reste comme valeur
					string valeur = string.Join("-", cv).Substring(1).Trim();
					//la rubrique doit se trouver dans le dictionaire
					//alimanté des rubriques auparavant
					if (dsn.NoeudBlocs.ContainsKey(cle))
						continue;

					int sautArriere = dernierNiveau - niveau;

					//si le niveau augmente par rapport au dernier niveau
					//Alors on met la dernier bloc dans la pile des parents
					if (sautArriere < 0)
					{
						parent = noeuBloc;
						pileParents.Push(parent);
					}
					//Si le niveau déminue alors enlève autant de parents que néscessaire
					//pour trouver le bon parent.
					else if (sautArriere > 0)
					{
						for (int i = 0; i < sautArriere; i++)
						{
							parent = pileParents.Pop();
						}
						parent = (NoeudBloc)parent.Parent;
					}

					//On crée un neeu et chercher les informations dans la ligne
					//pour alimenter le noeu
					noeuBloc = new NoeudBloc(cle, parent);

					Match m = Regex.Match(valeur, exp);
					if (m.Success)
					{
						noeuBloc.Libelle = m.Groups[1].Value;
						noeuBloc.InsMin = int.Parse(m.Groups[2].Value);
						if (string.Compare(m.Groups[3].Value, "*") == 0)
							noeuBloc.InsMax = 0;
						else
							noeuBloc.InsMax = int.Parse(m.Groups[3].Value);
					}
					//On ajoute le noeu dans le dictionaire
					dsn.NoeudBlocs.Add(cle, noeuBloc);

					dernierNiveau = niveau;
				}
			}
			niveauBase = 1;
			nligne = 0;
			//Traitement du envoi total
			using (StreamReader lecture = new StreamReader(fichierEnvoiTotaux))
			{
				while ((ligne = lecture.ReadLine()) != null)
				{
					nligne++;
					//On ne traite pas les lignes vides
					if (string.IsNullOrEmpty(ligne))
						continue;
					string[] cv = ligne.Split('-');

					//On ne traite pas les lignes sans avoir le caratère '-'
					if (cv.Length == 1)
						continue;

					//On compte le niveau de la rubrique
					niveau = 0;
					for (int i = 0; i < cv[0].Length; i++)
					{
						if (cv[0][i] == '\t')
							niveau = i + 1;
						else
							break;
					}
					//On traite la partie S20.G00.05 qui est supérieux que niveau de base
					niveau += niveauBase;

					//On elève tous les caratères \t pour trouver la rubrique
					string cle = cv[0].Trim();
					cv[0] = "";
					//On récupère le reste comme valeur
					string valeur = string.Join("-", cv).Substring(1).Trim();
					//la rubrique doit se trouver dans le dictionaire
					//alimanté des rubriques auparavant
					if (dsn.NoeudBlocs.ContainsKey(cle))
						continue;

					int sautArriere = dernierNiveau - niveau;

					//si le niveau augmente par rapport au dernier niveau
					//Alors on met la dernier bloc dans la pile des parents
					if (sautArriere < 0)
					{
						parent = noeuBloc;
						pileParents.Push(parent);
					}
					//Si le niveau déminue alors enlève autant de parents que néscessaire
					//pour trouver le bon parent.
					else if (sautArriere > 0)
					{
						for (int i = 0; i < sautArriere; i++)
						{
							parent = pileParents.Pop();
						}
						parent = (NoeudBloc)parent.Parent;
					}

					//On crée un neeu et chercher les informations dans la ligne
					//pour alimenter le noeu
					noeuBloc = new NoeudBloc(cle, parent);

					Match m = Regex.Match(valeur, exp);
					if (m.Success)
					{
						noeuBloc.Libelle = m.Groups[1].Value;
						noeuBloc.InsMin = int.Parse(m.Groups[2].Value);
						if (string.Compare(m.Groups[3].Value, "*") == 0)
							noeuBloc.InsMax = 0;
						else
							noeuBloc.InsMax = int.Parse(m.Groups[3].Value);
					}
					//On ajoute le noeu dans le dictionaire
					dsn.NoeudBlocs.Add(cle, noeuBloc);

					dernierNiveau = niveau;
				}
			}
			ParcourirArbre(dsn);
			return true;
		}

		public static string Tabs(int numTabs, string tab = "\t")
		{
			string sortie = "";
			for (int i = 0; i < numTabs; i++)
				sortie += tab;
			return sortie;
		}

		public static void ParcourirArbre(Dsn dsn)
		{
			INoeud courant = dsn.NoeudBlocRacine;
			Stack<INoeud> pileNoeu = new Stack<INoeud>();
			//On met le premier noeu dans la pile
			pileNoeu.Push(courant);
			//Lors que la pile n'est pas encore vide
			//on en enlève les noeu pour les utiliser 
			//et on alimente la pile des noeus enfants trouvés dans l'arbre
			//Pour faire tourner la boucle chaque enfant devient parent.
			//La boucle s'arrête lors que ne noeu n'a pas d'enfant.
			while (pileNoeu.Count > 0)
			{
				courant = pileNoeu.Pop();
				if (courant == null)
					continue;
				//On veut le primier enfant mis dans la pile doit être elévé le premier
				//Alors on enverse l'ordre des enfants dans le dictionaire des enfants
				for (var i = courant.Enfants.Count - 1; i >= 0; i--)
					pileNoeu.Push(courant.Enfants[i]);

				dsn.JoindreLog(Tabs(courant.Niveau) + courant.Id + " - " + courant.Libelle + " (" + courant.InsMin + "," + ((courant.InsMax == 0) ? "*)" : courant.InsMax + ")"));
			}
		}

		public static void FusionnerArbre()
		{
			using (StreamWriter ecriture = new StreamWriter("Arbre.txt", false))
			{

				string ligne = "S00.G00.00 - Racine (1,1)";
				ecriture.WriteLine(ligne);
				using (StreamReader lecture = new StreamReader("Envoi.txt"))
				{
					while ((ligne = lecture.ReadLine()) != null)
					{
						ecriture.WriteLine(Tabs(1) + ligne);
					}
				}
				using (StreamReader lecture = new StreamReader("DADSU COMPLETE.txt"))
				{
					while ((ligne = lecture.ReadLine()) != null)
					{
						ecriture.WriteLine(Tabs(2) + ligne);
					}
				}
				using (StreamReader lecture = new StreamReader("Envoi Totaux.txt"))
				{
					while ((ligne = lecture.ReadLine()) != null)
					{
						ecriture.WriteLine(Tabs(1) + ligne);
					}
				}
			}
		}

	}
}

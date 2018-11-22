using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbre
{
	public class FactoryArbre
	{
		public static bool FichiersEgaux(string fichierA = @"..\..\..\..\Donnee\Arbre.txt", string fichierB = @"..\..\..\..\Donnee\ArbreResultat.txt")
		{
			if (!File.Exists(fichierA))
				return false;
			if (!File.Exists(fichierB))
				return false;
			byte[] fA = File.ReadAllBytes(fichierA);
			byte[] fB = File.ReadAllBytes(fichierB);
			if (fA.Length == fB.Length)
			{
				for (int i = 0; i < fA.Length; i++)
				{
					if (fA[i] != fB[i])
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		public static Dsn CreerArbre()
		{
			Dsn dsn = new Dsn();
			//On construit l'arbre
			//En parcourant l'arbre, on joindre l'information au StringBuilder
			//Lors qu'on finit on écrit StringBuilder sur le fichier ArbreResultat.txt
			//Il est identique avec le fichier Arbre.txt

			dsn.EcrireLog(@"..\..\..\..\Donnee\ArbreResultat.txt");
			return dsn;
		}
	}
}

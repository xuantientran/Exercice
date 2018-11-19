using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbre
{
	public class UsineArbre
	{
		public static Dsn CreerArbre()
		{
			Dsn dsn = new Dsn();
			return dsn;
		}

		public static bool FichiersEgaux(string fichierA = @"..\..\..\..\Arbre.txt", string fichierB = @"..\..\..\..\ChargerBlocs.txt")
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


	}
}

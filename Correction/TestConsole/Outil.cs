using Arbre;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestConsole
{
	public class Outil
	{
		const string dossier = @"..\..\..\..\..\Donnee\";

		public static void TestArbre()
		{
			Dsn dsn = new Dsn();
			Utilitaire.ChargerBlocs(dsn);
			Utilitaire.ParcourirArbre(dsn);
			//Utilitaire.FusionnerArbre();
			dsn.EcrireLog("ChargerBlocs.txt");
			if (FactoryArbre.FichiersEgaux("ChargerBlocs.txt", "Arbre.txt"))
				Console.WriteLine("Bon travail");
			else
				Console.WriteLine("Non! Je vous invite de le refaire");
		}

	}
}

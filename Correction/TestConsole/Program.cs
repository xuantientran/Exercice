using Arbre;
using System;

namespace TestConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Dsn dsn = new Dsn();
			Utilitaire.ChargerBlocs(dsn);
			Utilitaire.ParcourirArbre(dsn);
			//Utilitaire.FusionnerArbre();
			dsn.EcrireLog("ChargerBlocs.txt");
			Console.WriteLine("Résultat : ChargerBlocs.txt");
			Console.ReadKey();
		}
	}
}

using Arbre;
using System;

namespace TestConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Dsn dsn = FactoryArbre.CreerArbre();
			Console.ReadKey();
		}
	}
}

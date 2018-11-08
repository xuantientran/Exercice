using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbre
{
	interface IFeuille
	{
		Dictionary<string, string> Rubriques { get; set; }
		IBloc Bloc { get; set; }
	}
}

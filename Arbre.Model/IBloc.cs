using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbre
{
	public interface IBloc
	{
		string Id { get; }
		List<IBloc> Enfants { get; set; }
		IBloc Parent { get; set; }
	}
}

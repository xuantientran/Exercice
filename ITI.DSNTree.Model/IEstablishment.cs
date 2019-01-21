using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public interface IEstablishment
	{
		string Nic { get; }
		string Enseigne { get; }
		IDataBlock EstablishmentBlock { get; }
	}
}

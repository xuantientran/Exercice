using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public interface IHonoraire
	{
		string Quality { get; }
		string LastName { get; }
		string FirstName { get; }
		string Siren { get; }
		string Nic { get; }
		string RaisonSociale { get; }
		string Address { get; }
		IDataBlock HonoraireDataBlock { get; }
	}
}

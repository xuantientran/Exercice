using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public interface IEmployee
	{
		string Nir { get; }
		string LastName { get; }
		string FirstName { get; }
		string Matricule { get; }
		IDataBlock DataBlock { get; }
		List<IActivityPeriod> ActivityPeriods { get; set; }
}
}

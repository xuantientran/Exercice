using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public interface IActivityPeriod
	{
		string BeginDate { get; }
		IDataBlock ActivityPeriodDataBlock { get; }
	}
}

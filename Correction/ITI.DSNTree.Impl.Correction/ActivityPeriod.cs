using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class ActivityPeriod : IActivityPeriod
	{
		string _beginDate;
		IDataBlock _activityPeriodDataBlock;

		public ActivityPeriod(IDataBlock activityPeriodDataBlock, string beginDate)
		{
			_activityPeriodDataBlock = activityPeriodDataBlock;
			_beginDate = beginDate;
		}

		public string BeginDate => _beginDate;
		public IDataBlock ActivityPeriodDataBlock => _activityPeriodDataBlock;
	}
}

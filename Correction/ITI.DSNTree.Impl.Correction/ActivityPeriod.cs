using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class ActivityPeriod : IActivityPeriod
	{
		public const string KeyActivityPeriodBlock = "S40.G01.00";
		const string activityPeriodBeginDateKey = "S40.G01.00.001";
		IDataBlock _activityPeriodDataBlock;
		string _beginDate;

		public ActivityPeriod(IDataBlock activityPeriodDataBlock)
		{
			_activityPeriodDataBlock = activityPeriodDataBlock;
			LoadActivityPeriod();
		}

		void LoadActivityPeriod()
		{
			_activityPeriodDataBlock.Leaves.First().Data.TryGetValue(activityPeriodBeginDateKey, out _beginDate);

		}

		public override string ToString() => _activityPeriodDataBlock.ToString();

		public string BeginDate => _beginDate;
		public IDataBlock ActivityPeriodDataBlock => _activityPeriodDataBlock;
	}
}

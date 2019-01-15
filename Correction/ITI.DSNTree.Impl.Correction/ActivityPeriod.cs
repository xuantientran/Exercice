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
		IDataBlock _dataBlock;

		public ActivityPeriod(IDataBlock dataBlock, string beginDate)
		{
			_dataBlock = dataBlock;
			_beginDate = beginDate;
		}

		public string BeginDate => _beginDate;
		public IDataBlock DataBlock => _dataBlock;
	}
}

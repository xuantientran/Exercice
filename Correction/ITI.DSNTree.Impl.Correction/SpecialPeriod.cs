using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class SpecialPeriod : ISpecialPeriod
	{
		string _beginDate;
		IDataBlock _specialPeriodDataBlock;

		public SpecialPeriod(IDataBlock specialPeriodDataBlock, string beginDate)
		{
			_specialPeriodDataBlock = specialPeriodDataBlock;
			_beginDate = beginDate;
		}

		public string BeginDate => _beginDate;
		public IDataBlock SpecialPeriodDataBlock => _specialPeriodDataBlock;
	}
}

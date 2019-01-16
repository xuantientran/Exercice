using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class SpecialPeriod : ISpecialPeriod
	{
		const string specialPeriodBeginDateKey = "S60.G05.00.002";

		IDataBlock _specialPeriodDataBlock;
		string _beginDate;

		public SpecialPeriod(IDataBlock specialPeriodDataBlock)
		{
			_specialPeriodDataBlock = specialPeriodDataBlock;
			LoadSpecialPeriod();
		}

		void LoadSpecialPeriod() =>_specialPeriodDataBlock.Leaves.First().Data.TryGetValue(specialPeriodBeginDateKey, out _beginDate);

		public string BeginDate => _beginDate;
		public IDataBlock SpecialPeriodDataBlock => _specialPeriodDataBlock;
	}
}

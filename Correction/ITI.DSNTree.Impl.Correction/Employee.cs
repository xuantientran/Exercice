using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	class Employee : IEmployee
	{
		const string activityPeriodBlockKey = "S40.G01.00";
		const string activityPeriodBeginDateKey = "S40.G01.00.001";
		IDataTree _dataTree;
		IDataBlock _dataBlock;
		string _nir;
		string _lastName;
		string _firstName;
		string _matricule;
		List<IActivityPeriod> _activityPeriods;

		public Employee(IDataTree dataTree, IDataBlock dataBlock, string nir, string lastName, string firstName, string matricule)
		{
			_dataTree = dataTree;
			_dataBlock = dataBlock;
			_nir = nir;
			_lastName = lastName;
			_firstName = firstName;
			_matricule = matricule;
			_activityPeriods = new List<IActivityPeriod>();
			LoadActivityPeriods();
		}

		void LoadActivityPeriods()
		{
			IActivityPeriod activityPeriod;
			List<IDataBlock> activityPeriodBlocks = ((DataTree)_dataTree).FindBlock(activityPeriodBlockKey, _dataBlock);
			foreach (var p in activityPeriodBlocks)
			{
				p.Leaves.First().Data.TryGetValue(activityPeriodBeginDateKey, out string activityPeriodBeginDate);
				activityPeriod = new ActivityPeriod(p, activityPeriodBeginDate);
				_activityPeriods.Add(activityPeriod);
			}
		}

		public string Nir { get => _nir; }
		public string LastName { get => _lastName; }
		public string FirstName { get => _firstName; }
		public string Matricule { get => _matricule; }
		public IDataBlock DataBlock { get => _dataBlock; set => _dataBlock = value; }
		public List<IActivityPeriod> ActivityPeriods { get => _activityPeriods; set => _activityPeriods = value; }
	}
}

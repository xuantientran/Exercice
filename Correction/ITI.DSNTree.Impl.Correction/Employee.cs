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

		const string specialPeriodBlockKey = "S60.G05.00";
		const string specialPeriodBeginDateKey = "S60.G05.00.002";

		IDataTree _dataTree;
		
		//Employee block
		IDataBlock _employeeDataBlock;
		string _nir;
		string _lastName;
		string _firstName;
		string _matricule;
		List<IActivityPeriod> _activityPeriods;
		List<ISpecialPeriod> _specialPeriods;

		public Employee(IDataTree dataTree, IDataBlock employeeDataBlock,
			string nir, string lastName, string firstName, string matricule)
		{
			_dataTree = dataTree;
			_employeeDataBlock = employeeDataBlock;
			_nir = nir;
			_lastName = lastName;
			_firstName = firstName;
			_matricule = matricule;
			_activityPeriods = new List<IActivityPeriod>();
			_specialPeriods = new List<ISpecialPeriod>();
			LoadActivityPeriods();
			LoadSpecialPeriods();
		}

		void LoadSpecialPeriods()
		{
			ISpecialPeriod specialPeriod;
			List<IDataBlock> specialPeriodBlocks = ((DataTree)_dataTree).FindBlock(specialPeriodBlockKey, _employeeDataBlock);
			foreach (var sBlock in specialPeriodBlocks)
			{
				sBlock.Leaves.First().Data.TryGetValue(specialPeriodBeginDateKey, out string specialPeriodBeginDate);
				specialPeriod = new SpecialPeriod(sBlock, specialPeriodBeginDate);
				_specialPeriods.Add(specialPeriod);
			}
		}

		void LoadActivityPeriods()
		{
			IActivityPeriod activityPeriod;
			List<IDataBlock> activityPeriodBlocks = ((DataTree)_dataTree).FindBlock(activityPeriodBlockKey, _employeeDataBlock);
			foreach (var pBlock in activityPeriodBlocks)
			{
				pBlock.Leaves.First().Data.TryGetValue(activityPeriodBeginDateKey, out string activityPeriodBeginDate);
				activityPeriod = new ActivityPeriod(pBlock, activityPeriodBeginDate);
				_activityPeriods.Add(activityPeriod);
			}
		}

		public string Nir { get => _nir; }
		public string LastName { get => _lastName; }
		public string FirstName { get => _firstName; }
		public string Matricule { get => _matricule; }
		public IDataBlock EmployeeDataBlock { get => _employeeDataBlock; set => _employeeDataBlock = value; }
		public List<IActivityPeriod> ActivityPeriods { get => _activityPeriods; set => _activityPeriods = value; }
		public List<ISpecialPeriod> SpecialPeriods { get => _specialPeriods; set => _specialPeriods = value; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class Employee : IEmployee
	{
		public const string KeyNir = "S30.G01.00.001";
		public const string KeyLastName = "S30.G01.00.002";
		public const string KeyFirstName = "S30.G01.00.003";
		public const string KeyMatricule = "S30.G01.00.019";

		public const string KeyEmployeeBlock = "S30.G01.00";
		public const string KeySpecialPeriodBlock = "S60.G05.00";

		IDataTree _dataTree;

		//Employee block
		IDataBlock _employeeDataBlock;
		string _nir;
		string _lastName;
		string _firstName;
		string _matricule;
		List<IActivityPeriod> _activityPeriods;
		List<ISpecialPeriod> _specialPeriods;

		public Employee(IDataTree dataTree, IDataBlock employeeDataBlock)
		{
			_dataTree = dataTree;
			_employeeDataBlock = employeeDataBlock;
			_activityPeriods = new List<IActivityPeriod>();
			_specialPeriods = new List<ISpecialPeriod>();
			LoadEmployee();
			LoadActivityPeriods();
			LoadSpecialPeriods();
		}

		void LoadEmployee()
		{
			var eData = _employeeDataBlock.Leaves.First().Data;
			eData.TryGetValue(KeyNir, out _nir);
			eData.TryGetValue(KeyLastName, out _lastName);
			eData.TryGetValue(KeyFirstName, out _firstName);
			eData.TryGetValue(KeyMatricule, out _matricule);
		}

		void LoadActivityPeriods()
		{
			IActivityPeriod activityPeriod;
			List<IDataBlock> activityPeriodBlocks = ((DataTree)_dataTree).FindBlock(ActivityPeriod.KeyActivityPeriodBlock, _employeeDataBlock);
			foreach (var pBlock in activityPeriodBlocks)
			{
				activityPeriod = new ActivityPeriod(pBlock);
				_activityPeriods.Add(activityPeriod);
			}
		}

		void LoadSpecialPeriods()
		{
			ISpecialPeriod specialPeriod;
			List<IDataBlock> specialPeriodBlocks = ((DataTree)_dataTree).FindBlock(KeySpecialPeriodBlock, _employeeDataBlock);
			foreach (var sBlock in specialPeriodBlocks)
			{
				specialPeriod = new SpecialPeriod(sBlock);
				_specialPeriods.Add(specialPeriod);
			}
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(_employeeDataBlock.Leaves.First().ToString());
			foreach (var activityPeriod in _activityPeriods)
				sb.Append(activityPeriod.ToString());
			foreach (var specialPeriod in _specialPeriods)
				sb.Append(specialPeriod.ToString());
			return sb.ToString();
		}

		public string Nir => _nir;
		public string LastName => _lastName;
		public string FirstName => _firstName;
		public string Matricule => _matricule;
		public IDataBlock EmployeeDataBlock => _employeeDataBlock;
		public List<IActivityPeriod> ActivityPeriods => _activityPeriods;
		public List<ISpecialPeriod> SpecialPeriods => _specialPeriods;
	}
}

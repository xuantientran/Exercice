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

		IDataTree _dataTree;

		//Employee block
		IDataBlock _employeeDataBlock;
		string _nir;
		string _lastName;
		string _firstName;
		string _matricule;
		List<IActivityPeriod> _activityPeriods;

		public Employee(IDataTree dataTree, IDataBlock employeeDataBlock)
		{
			_dataTree = dataTree;
			_employeeDataBlock = employeeDataBlock;
			_activityPeriods = new List<IActivityPeriod>();
			LoadEmployee();
			LoadActivityPeriods();
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
			List<IDataBlock> periodBlocks = ((DataTree)_dataTree).FindBlock(ActivityPeriod.KeyActivityPeriodBlock, _employeeDataBlock);
			foreach (var pBlock in periodBlocks)
				_activityPeriods.Add(new ActivityPeriod(pBlock));
		}

		public string Nir => _nir;
		public string LastName => _lastName;
		public string FirstName => _firstName;
		public string Matricule => _matricule;
		public IDataBlock EmployeeDataBlock => _employeeDataBlock;
		public List<IActivityPeriod> ActivityPeriods => _activityPeriods;
	}
}

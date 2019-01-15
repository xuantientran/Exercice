using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class DsnData : IDsnData
	{
		IDsnTree _dsnTree;
		IDataTree _dataTree;
		Dictionary<string, IEmployee> _employees;

		const string nirKey = "S30.G01.00.001";
		const string lastNameKey = "S30.G01.00.002";
		const string firstNameKey = "S30.G01.00.003";
		const string matriculeKey = "S30.G01.00.019";
		const string employeeBlocKey = "S30.G01.00";

		public DsnData(IDataTree dataTree)
		{
			_dataTree = dataTree;
			_dsnTree = _dataTree.DsnTree;
			_employees = new Dictionary<string, IEmployee>();
			LoadEmplyees();
		}

		void LoadEmplyees()
		{
			List<IDataBlock> employeeBlocks = ((DataTree)_dataTree).FindBlock(employeeBlocKey);
			IEmployee employee;
			foreach (var e in employeeBlocks)
			{
				var eData = e.Leaves.First().Data;
				eData.TryGetValue(nirKey, out string nir);
				eData.TryGetValue(lastNameKey, out string lastName);
				eData.TryGetValue(firstNameKey, out string firstName);
				eData.TryGetValue(matriculeKey, out string matricule);
				employee = new Employee(_dataTree, e, nir, lastName, firstName, matricule);
				_employees.Add(matricule + "-" + nir, employee);
			}
		}

		public IDataTree DataTree  => _dataTree;
		public Dictionary<string, IEmployee> Employees => _employees;
	}
}

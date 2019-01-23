using System;
using System.Collections.Generic;
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
		Dictionary<string, IEstablishment> _establishments;

		public DsnData(IDataTree dataTree)
		{
			_dataTree = dataTree;
			_dsnTree = _dataTree.DsnTree;
			_employees = new Dictionary<string, IEmployee>();
			_establishments = new Dictionary<string, IEstablishment>();
			LoadEmplyees();
			LoadEstablishments();
		}

		void LoadEmplyees()
		{
			List<IDataBlock> employeeBlocks = ((DataTree)_dataTree).FindBlock(Employee.KeyEmployeeBlock);
			IEmployee employee;
			foreach (var eBlock in employeeBlocks)
			{
				employee = new Employee(_dataTree, eBlock);
				_employees.Add(employee.Matricule + "-" + employee.Nir, employee);
			}
		}

		void LoadEstablishments()
		{
			IEstablishment establishment;
			List<IDataBlock> establishmentBlocks = ((DataTree)_dataTree).FindBlock(Establishment.KeyEstablishmentBlock);
			foreach (var eBlock in establishmentBlocks)
			{
				establishment = new Establishment(eBlock);
				_establishments.Add(establishment.Nic, establishment);
			}
		}

		public IDataTree DataTree => _dataTree;
		public Dictionary<string, IEmployee> Employees => _employees;
		public Dictionary<string, IEstablishment> Establishments => _establishments;
	}
}

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
		List<IHonorairePayer> _honorairePayers;

		public DsnData(IDataTree dataTree)
		{
			_dataTree = dataTree;
			_dsnTree = _dataTree.DsnTree;
			_employees = new Dictionary<string, IEmployee>();
			_honorairePayers = new List<IHonorairePayer>();
			LoadEmplyees();
			LoadHonorairePayers();
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

		void LoadHonorairePayers()
		{
			List<IDataBlock> honorairePayerBlocks = ((DataTree)_dataTree).FindBlock(HonorairePayer.KeyHonorairePayerBlock);
			IHonorairePayer honorairePayer;
			foreach (var pBlock in honorairePayerBlocks)
			{
				honorairePayer = new HonorairePayer(_dataTree, pBlock);
				_honorairePayers.Add(honorairePayer);
			}
		}

		public IDataTree DataTree => _dataTree;

		public Dictionary<string, IEmployee> Employees => _employees;

		public List<IHonorairePayer> HonorairePayers => _honorairePayers;
	}
}

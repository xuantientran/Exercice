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

		const string employeeBlockKey = "S30.G01.00";
		const string nirKey = "S30.G01.00.001";
		const string lastNameKey = "S30.G01.00.002";
		const string firstNameKey = "S30.G01.00.003";
		const string matriculeKey = "S30.G01.00.019";

		const string honoraireBlockKey = "S70.G05.00";
		const string honoraireNicKey = "S70.G05.00.001";

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
			List<IDataBlock> employeeBlocks = ((DataTree)_dataTree).FindBlock(employeeBlockKey);
			IEmployee employee;
			foreach (var eBlock in employeeBlocks)
			{
				var eData = eBlock.Leaves.First().Data;
				eData.TryGetValue(nirKey, out string nir);
				eData.TryGetValue(lastNameKey, out string lastName);
				eData.TryGetValue(firstNameKey, out string firstName);
				eData.TryGetValue(matriculeKey, out string matricule);
				employee = new Employee(_dataTree, eBlock, nir, lastName, firstName, matricule);
				_employees.Add(matricule + "-" + nir, employee);
			}
		}

		void LoadHonorairePayers()
		{
			List<IDataBlock> honorairePayerBlocks = ((DataTree)_dataTree).FindBlock(honoraireBlockKey);
			IHonorairePayer honorairePayer;
			foreach (var pBlock in honorairePayerBlocks)
			{
				var pData = pBlock.Leaves.First().Data;
				pData.TryGetValue(honoraireNicKey, out string nic);
				honorairePayer = new HonorairePayer(_dataTree, pBlock, nic);
				_honorairePayers.Add(honorairePayer);
			}
		}

		public IDataTree DataTree  => _dataTree;

		public Dictionary<string, IEmployee> Employees => _employees;

		public List<IHonorairePayer> HonorairePayers => _honorairePayers;
	}
}

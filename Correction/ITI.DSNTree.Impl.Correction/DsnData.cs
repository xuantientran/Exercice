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
		IDispatch _dispatch;

		Dictionary<string, IEmployee> _employees;
		List<IHonorairePayer> _honorairePayers;
		Dictionary<string, IEstablishment> _establishments;

		public DsnData(IDataTree dataTree)
		{
			_dataTree = dataTree;
			_dsnTree = _dataTree.DsnTree;
			_employees = new Dictionary<string, IEmployee>();
			_honorairePayers = new List<IHonorairePayer>();
			_establishments = new Dictionary<string, IEstablishment>();
			LoadDispatch();
			LoadEmplyees();
			LoadHonorairePayers();
			LoadEstablishments();
		}

		void LoadDispatch()
		{
			List<IDataBlock> dispatchBlocks = ((DataTree)_dataTree).FindBlock(Dispatch.KeyDispatchBlock);
			_dispatch = new Dispatch(dispatchBlocks.First());
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
			foreach (var pBlock in honorairePayerBlocks)
				_honorairePayers.Add(new HonorairePayer(_dataTree, pBlock));
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

		public IDispatch DataDispatch => _dispatch;
		public IDataTree DataTree => _dataTree;
		public Dictionary<string, IEmployee> Employees => _employees;
		public List<IHonorairePayer> HonorairePayers => _honorairePayers;
		public Dictionary<string, IEstablishment> Establishments => _establishments;
	}
}

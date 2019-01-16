using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class HonorairePayer : IHonorairePayer
	{
		IDataTree _dataTree;
		IDataBlock _honorairePayerBlock;
		string _nic;

		public HonorairePayer(IDataTree dataTree, IDataBlock honorairePayerBlock, string nic)
		{
			_dataTree = dataTree;
			_honorairePayerBlock = honorairePayerBlock;
			_nic = nic;
		}

		public string Nic => _nic;

		public IDataBlock HonorairePayerBlock => _honorairePayerBlock;
	}
}

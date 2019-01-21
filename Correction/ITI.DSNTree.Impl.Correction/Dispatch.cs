using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class Dispatch : IDispatch
	{
		public const string KeyDispatchBlock = "S10.G01.00";

		public const string KeyDispatchSiren = "S10.G01.00.001.001";
		public const string KeyDispatchNic = "S10.G01.00.001.001";
		public const string KeyDispatchName = "S10.G01.00.001.001";

		public const string KeyDispatchSenderBlock = "S10.G01.00";

		IDataBlock _dispatchBlock;
		string _siren;

		string _nic;
		string _name;

		public Dispatch(IDataBlock dispatchBlock)
		{
			_dispatchBlock = dispatchBlock;
			LoadDispatch();
		}

		void LoadDispatch()
		{
			var dData = _dispatchBlock.Leaves.First().Data;
			dData.TryGetValue(KeyDispatchSiren, out _siren);
			dData.TryGetValue(KeyDispatchNic, out _nic);
			dData.TryGetValue(KeyDispatchName, out _name);
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(_dispatchBlock.Leaves.First().ToString());
			return sb.ToString();
		}

		public string Siren => _siren;
		public string Nic => _nic;
		public string Name => _name;
		public IDataBlock DispatchBlock => _dispatchBlock;
	}
}

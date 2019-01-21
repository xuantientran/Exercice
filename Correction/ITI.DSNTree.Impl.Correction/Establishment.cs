using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class Establishment : IEstablishment
	{
		public const string KeyEstablishmentBlock = "S80.G01.00";
		public const string KeyEstablishmentNic = "S80.G01.00.001.002";
		public const string KeyEstablishmentEnseigne = "S80.G01.00.002";

		string _nic;
		string _enseigne;
		IDataBlock _establishmentBlock;

		public Establishment(IDataBlock establishmentBlock)
		{
			_establishmentBlock = establishmentBlock;
			LoadEstablishment();
		}

		void LoadEstablishment()
		{
			var eData = _establishmentBlock.Leaves.First().Data;
			eData.TryGetValue(KeyEstablishmentNic, out _nic);
			eData.TryGetValue(KeyEstablishmentEnseigne, out _enseigne);
		}

		public override string ToString() => _establishmentBlock.ToString();

		public string Nic => _nic;
		public string Enseigne => _enseigne;
		public IDataBlock EstablishmentBlock => _establishmentBlock;
	}
}

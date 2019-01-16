using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class Honoraire : IHonoraire
	{
		IDataTree _dataTree;
		IDataBlock _honoraireBlock;

		string _quality;
		string _lastName;
		string _firstName;
		string _siren;
		string _nic;
		string _raisonSociale;
		string _address;

		const string beneficiaryQualityKey = "S70.G10.00.001";
		const string beneficiaryLastNameKey = "S70.G10.00.002.001";
		const string beneficiaryFirstNameKey = "S70.G10.00.002.002";
		const string beneficiarySirenKey = "S70.G10.00.003.001";
		const string beneficiaryNicKey = "S70.G10.00.003.002";
		const string beneficiaryRaisonSocialeKey = "S70.G10.00.003.003";
		const string beneficiaryAdressKey = "S70.G10.00.004.006";

		public Honoraire(IDataTree dataTree, IDataBlock honoraireBlock)
		{
			_dataTree = dataTree;
			_honoraireBlock = honoraireBlock;
			LoadHonoraire();
		}

		void LoadHonoraire()
		{
			var hData = _honoraireBlock.Leaves.First().Data;
			hData.TryGetValue(beneficiaryQualityKey, out _quality);
			hData.TryGetValue(beneficiaryLastNameKey, out _lastName);
			hData.TryGetValue(beneficiaryFirstNameKey, out _firstName);
			hData.TryGetValue(beneficiarySirenKey, out _siren);
			hData.TryGetValue(beneficiaryNicKey, out _nic);
			hData.TryGetValue(beneficiaryRaisonSocialeKey, out _raisonSociale);
			hData.TryGetValue(beneficiaryAdressKey, out _address);
		}

		public string Quality => _quality;
		public string LastName => _lastName;
		public string FirstName => _firstName;
		public string Siren => _siren;
		public string Nic => _nic;
		public string RaisonSociale => _raisonSociale;
		public string Address => _address;
		public IDataBlock HonoraireDataBlock => _honoraireBlock;

	}
}

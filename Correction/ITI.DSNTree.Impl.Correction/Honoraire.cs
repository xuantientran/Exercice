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

		const string KeyBeneficiaryQuality = "S70.G10.00.001";
		const string KeyBeneficiaryLastName = "S70.G10.00.002.001";
		const string KeyBeneficiaryFirstName = "S70.G10.00.002.002";
		const string KeyBeneficiarySiren = "S70.G10.00.003.001";
		const string KeyBeneficiaryNic = "S70.G10.00.003.002";
		const string KeyBeneficiaryRaisonSociale = "S70.G10.00.003.003";
		const string KeyBeneficiaryAdress = "S70.G10.00.004.006";

		public Honoraire(IDataTree dataTree, IDataBlock honoraireBlock)
		{
			_dataTree = dataTree;
			_honoraireBlock = honoraireBlock;
			LoadHonoraire();
		}

		void LoadHonoraire()
		{
			var hData = _honoraireBlock.Leaves.First().Data;
			hData.TryGetValue(KeyBeneficiaryQuality, out _quality);
			hData.TryGetValue(KeyBeneficiaryLastName, out _lastName);
			hData.TryGetValue(KeyBeneficiaryFirstName, out _firstName);
			hData.TryGetValue(KeyBeneficiarySiren, out _siren);
			hData.TryGetValue(KeyBeneficiaryNic, out _nic);
			hData.TryGetValue(KeyBeneficiaryRaisonSociale, out _raisonSociale);
			hData.TryGetValue(KeyBeneficiaryAdress, out _address);
		}

		public override string ToString() => _honoraireBlock.ToString();

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

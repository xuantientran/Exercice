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
		IDataBlock _honoraireDataBlock;

		string _quality;
		string _lastName;
		string _firstName;
		string _siren;
		string _nic;
		string _raisonSociale;
		string _address;

		const string beneficiaryBlockKey		= "S70.G10.00";
		
		const string beneficiaryQualityKey	= "S70.G10.00.001";
		const string beneficiaryLastNameKey = "S70.G10.00.002.001";
		const string beneficiaryFirstNameKey= "S70.G10.00.002.002";
		const string beneficiarySirenKey		= "S70.G10.00.003.001";
		const string beneficiaryNicKey			= "S70.G10.00.003.002";
		const string beneficiaryRaisonSocialeKey = "S70.G10.00.003.003";
		const string beneficiaryAdressKey		= "S70.G10.00.004.006";

		public Honoraire(IDataTree dataTree, IDataBlock honoraireDataBlock, string quality, string lastName, string firstName, string siren, string nic, string raisonSociale, string address)
		{
			_dataTree = dataTree;
			_honoraireDataBlock = honoraireDataBlock;
			_quality = quality;
			_lastName = lastName;
			_firstName = firstName;
			_siren = siren;
			_nic = nic;
			_raisonSociale = raisonSociale;
			_address = address;
		}

		public string Quality => _quality;
		public string LastName => _lastName;
		public string FirstName => _firstName;
		public string Siren => _siren;
		public string Nic => _nic;
		public string RaisonSociale => _raisonSociale;
		public string Address => _address;
		public IDataBlock HonoraireDataBlock => _honoraireDataBlock;

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class HonorairePayer : IHonorairePayer
	{
		public const string KeyHonorairePayerBlock = "S70.G05.00";
		public const string honorairePayerNicKey = "S70.G05.00.001";
		public const string honoraireBlocKey = "S70.G10.00";

		IDataTree _dataTree;
		IDataBlock _honorairePayerBlock;
		string _nic;
		List<IHonoraire> _honoraires;

		public HonorairePayer(IDataTree dataTree, IDataBlock honorairePayerBlock)
		{
			_dataTree = dataTree;
			_honorairePayerBlock = honorairePayerBlock;
			_honoraires = new List<IHonoraire>();
			LoadHonorairePayer();
			LoadHonoraires();
		}

		void LoadHonorairePayer()
		{
			var pData = _honorairePayerBlock.Leaves.First().Data;
			pData.TryGetValue(honorairePayerNicKey, out _nic);
		}

		void LoadHonoraires()
		{
			IHonoraire honoraire;
			List<IDataBlock> honoraireBlocks = ((DataTree)_dataTree).FindBlock(honoraireBlocKey, _honorairePayerBlock);
			foreach (var honoraireBlock in honoraireBlocks)
			{
				honoraire = new Honoraire(_dataTree, honoraireBlock);
				_honoraires.Add(honoraire);
			}
		}

		public string Nic => _nic;

		public IDataBlock HonorairePayerBlock => _honorairePayerBlock;

		public List<IHonoraire> Honoraires => _honoraires;
	}
}

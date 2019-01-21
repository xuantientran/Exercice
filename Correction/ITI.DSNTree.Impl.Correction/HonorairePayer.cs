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
		public const string KeyHonorairePayerNic = "S70.G05.00.001";
		public const string KeyHonoraireBloc = "S70.G10.00";

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
			pData.TryGetValue(KeyHonorairePayerNic, out _nic);
		}

		void LoadHonoraires()
		{
			List<IDataBlock> honoraireBlocks = ((DataTree)_dataTree).FindBlock(KeyHonoraireBloc, _honorairePayerBlock);
			foreach (var honoraireBlock in honoraireBlocks)
				_honoraires.Add(new Honoraire(_dataTree, honoraireBlock));
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(_honorairePayerBlock.Leaves.First().ToString());

			foreach (var honoraire in _honoraires)
				sb.Append(honoraire.ToString());

			return sb.ToString();
		}

		public string Nic => _nic;

		public IDataBlock HonorairePayerBlock => _honorairePayerBlock;

		public List<IHonoraire> Honoraires => _honoraires;
	}
}

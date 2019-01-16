using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public interface IHonorairePayer
	{
		string Nic { get; }
		IDataBlock HonorairePayerBlock { get; }
	}
}

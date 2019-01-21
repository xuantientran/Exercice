using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public interface IDispatch
	{
		string Siren { get; }
		string Nic { get; }
		string Name { get; }
		IDataBlock DispatchBlock { get; }
	}
}

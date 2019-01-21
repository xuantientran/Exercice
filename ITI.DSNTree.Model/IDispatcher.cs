using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public interface IDispatcher
	{
		IDataBlock DispatcherBlock { get; }
	}
}

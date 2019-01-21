using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class Dispatcher : IDispatcher
	{
		public const string KeyDispatcherBlock = "S10.G01.01";
		IDataBlock _dispatcherBlock;

		public Dispatcher(IDataBlock dispatcherBlock)
		{
			_dispatcherBlock = dispatcherBlock;
		}
		public IDataBlock DispatcherBlock => _dispatcherBlock;
	}
}

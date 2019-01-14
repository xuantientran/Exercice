using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DATATree
{
	public interface IDataTree
	{
		IBlock Root { set; get; }
	}
}

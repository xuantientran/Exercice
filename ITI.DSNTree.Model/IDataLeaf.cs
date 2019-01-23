using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public interface IDataLeaf
	{
		Dictionary<string, string> Data { set; get; }
		IDataBlock Block { set; get; }
	}
}
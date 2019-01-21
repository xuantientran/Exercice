using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public interface IDsnData
	{
		IDataTree DataTree { get; }
		Dictionary<string, IEmployee> Employees { get; }
		List<IHonorairePayer> HonorairePayers { get; }
		Dictionary<string, IEstablishment> Establishments { get; }
		IDispatch DataDispatch { get; }
	}
}

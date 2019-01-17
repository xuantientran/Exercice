using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.DSNTree
{
	public class DataItem
	{
		public string Key { get; set; }
		public string Value { get; set; }
		public string OldValue { get; set; }
		public ChangeStatus Status { get; set; }
	}
}

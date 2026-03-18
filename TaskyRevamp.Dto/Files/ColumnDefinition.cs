using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.Files
{
	public class ColumnDefinition<T>
	{
		public string Header { get; set; }
		public Func<T, object> Value { get; set; }
	}
}

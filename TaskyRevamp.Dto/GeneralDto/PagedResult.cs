using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Dto.GeneralDto;

public class PagedResult<TEntity>
{
    public List<TEntity> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}

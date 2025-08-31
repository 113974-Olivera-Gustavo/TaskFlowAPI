using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskflow.Application.RequestDto.Common
{
    public class PaginatedRequest
    {
        [Required] public int Page { get; set; }

        [Required] public int PageSize { get; set; }
    }
}

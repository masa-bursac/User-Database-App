using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationBack.DTO
{
    public class SearchDto
    {
        public String Email { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public SearchDto() { }
    }
}

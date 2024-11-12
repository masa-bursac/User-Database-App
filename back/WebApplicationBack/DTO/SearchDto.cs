using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationBack.DTO
{
    public class SearchDto
    {
        public String Email { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public SearchDto() { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Person
{
    public class PersonSearchDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSaloon.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public bool WomenHaircut { get; set; }
        public bool ManHaircut { get; set; }
        public bool Admin { get; set; }


        public Human Human{ get; set; }
    }
}

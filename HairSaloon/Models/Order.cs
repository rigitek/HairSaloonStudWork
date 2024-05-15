using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSaloon.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public bool WashHair { get; set; }
        public bool State { get; set; }

        public Service? Service { get; set; }
        public Human? Human { get; set; }
        public Employee? Employee { get; set; }

    }
}

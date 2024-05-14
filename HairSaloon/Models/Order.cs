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
        public DateTime Time { get; set; }
        public bool WashHair { get; set; }
        public string State { get; set; }

        public int ServiceId { get; set; }
        public Service? Service { get; set; }
        public int ClientId { get; set; }
        public Human? Human { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

    }
}

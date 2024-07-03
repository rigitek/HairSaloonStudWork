using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSaloon.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }

        public int? OrderId { get; set; }
        public Order? Order { get; set; }

        public override string ToString() => $"Звезд: {Stars}\nКомментарий: {Comment}";
    }
}

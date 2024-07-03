using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairSaloon.Models
{
    public class AddictService
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public override string ToString() => $"{Title}";
    }
}

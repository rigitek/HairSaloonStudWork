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


        public Human Human { get; set; }

        public override string ToString()
        {
            string Master = "";

            if (WomenHaircut == true && ManHaircut == true)
            {
                Master = "Мастер по мужским и женским стрижкам";
                return $"{Human.FirstName} {Human.LastName} \n{Master}";
            }

            if (WomenHaircut == true && ManHaircut == false)
            {
                Master = "Мастер по женским стрижкам";
                return $"{Human.FirstName} {Human.LastName} \n{Master}";
            }
            if (WomenHaircut == false && ManHaircut == true)
            {
                Master = "Мастер по мужским стрижкам";
                return $"{Human.FirstName} {Human.LastName} \n{Master}";
            }
           
            return $"{Human.FirstName} {Human.LastName}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Repository.Domain
{
    public class Budget
    {
        public int IdDepartment { get; set; }
        public int Year { get; set; }
        public decimal Value { get; set; }

        public virtual Department Department { get; set; }
    }
}

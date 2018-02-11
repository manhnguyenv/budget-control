using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Domain
{
    public class Supplier : Base
    {
        public Supplier()
        {
            Requests = new HashSet<Request>();
        }

        public string Name { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}

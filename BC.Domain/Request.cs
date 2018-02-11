using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Domain
{
    public class Request : Base
    {
        public Request()
        {
            RequestsHistoric = new HashSet<RequestHistoric>();
        }

        public int IdProject { get; set; }
        public decimal Value { get; set; }
        public int IdSupplier { get; set; }
        public Status RequestStatus { get; set; }
        public DateTime RequestDate { get; set; }

        public virtual Project Project { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<RequestHistoric> RequestsHistoric { get; set; }
    }

    public enum Status
    {
        Draft = 1,
        Pending = 2,
        Approved = 3,
        Recused = 4,
        Cancelled = 5
    }
}

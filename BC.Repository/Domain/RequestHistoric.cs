using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Repository.Domain
{
    public class RequestHistoric
    {
        public int IdRequest { get; set; }
        public Status RequestStatus { get; set; }
        public DateTime RequestDate { get; set; }

        public virtual Request Request { get; set; }
    }
}

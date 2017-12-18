using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Repository.Domain
{
    public class Project : Base
    {
        public Project()
        {
            Requests = new HashSet<Request>();
        }

        public string Description { get; set; }
        public int IdDepartment { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}

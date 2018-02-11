using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC.Domain;

namespace BC.ViewModel.Resultset
{
    public class RequestResultsetVM
    {
        public Project Project { get; set; }
        public IEnumerable<Supplier> Supplies { get; set; }

        public bool ShowCancelButton(Status statusToCheck)
        {
            switch (statusToCheck)
            {
                case Status.Cancelled:
                case Status.Recused:
                    return false;
            }

            return true;
        }

        public string GetStatuLabel(Status statusToCheck)
        {
            switch (statusToCheck)
            {
                case Status.Pending:
                    return "ui yellow label";
                case Status.Approved:
                    return "ui green label";
                case Status.Cancelled:
                case Status.Recused:
                    return "ui red label";
                case Status.Draft:
                    return "ui grey label";
            }

            return "ui grey label";
        }
    }
}
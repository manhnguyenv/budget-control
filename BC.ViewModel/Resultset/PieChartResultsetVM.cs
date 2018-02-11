using BC.ViewModel.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.ViewModel.Resultset
{
    public class PieChartResultsetVM
    {
        public PieChartResultsetVM()
        {
            configuration = new BaseChart();
            data = new PieChart();
        }

        public BaseChart configuration { get; set; }
        public PieChart data { get; set; }
    }
}

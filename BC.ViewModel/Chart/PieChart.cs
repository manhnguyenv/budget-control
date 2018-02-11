using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.ViewModel.Chart
{
    public class PieChart
    {
        public string[] labels { get; set; }
        public PieDataset[] datasets { get; set; }
    }

    public class PieDataset
    {
        public string[] backgroundColor { get; set; }
        public string[] borderColor { get; set; }
        public int borderWidth { get; set; }
        public double[] data { get; set; }
    }
}

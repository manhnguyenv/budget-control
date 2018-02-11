using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.ViewModel.Register
{
    public class RequestRegisterVM
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Supplier is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Supplier is requided")]
        public int IdSupplier { get; set; }

        [Required(ErrorMessage = "Value is required.")]
        [Range(typeof(decimal), "1", "79228162514264337593543950335", ErrorMessage = "Value must be bigger than 0")]
        public decimal Value { get; set; }

        public int IdProject { get; set; }
    }
}

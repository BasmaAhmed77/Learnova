using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.ModelVM.User
{
    public record UpdatePhoneNumberVM(
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "Please enter a valid Egyptian phone number")]
        string PhoneNumber
    );
}

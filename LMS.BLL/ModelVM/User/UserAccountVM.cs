using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.ModelVM.User
{
    public record UserAccountVM
    {
        public string Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string? ProfilePicture { get; set; }
        public string? PhoneNumber { get; set; }
    }
}

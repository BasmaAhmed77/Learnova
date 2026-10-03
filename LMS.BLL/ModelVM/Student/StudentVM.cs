using LMS.BLL.ModelVM.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BLL.ModelVM.Student
{
    public record StudentVM : UserAccountVM
    {
        public int StudentId { get; set; }
    }
}

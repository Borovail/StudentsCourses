using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamWpf.Model
{
    internal class CoursesPlusUsers
    {
        List<string> students { get; set; }
        public CoursesPlusUsers()
        {
            students = new List<string> { "fd", "fds" };
        }
    }
}

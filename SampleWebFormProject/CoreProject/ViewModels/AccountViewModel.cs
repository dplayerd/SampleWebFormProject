using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreProject.ViewModels
{
    public class AccountViewModel
    {
        public Guid ID { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int UserLevel { get; set; } = 0;

        public string PWD { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

    }
}

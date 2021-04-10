using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreProject.Models
{
    public class AccountModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string PWD { get; set; }
        public int Level { get; set; }
        public string Email { get; set; }
    }
}

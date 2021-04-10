using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreProject.Helpers
{
    public class LoginInfo
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public UserLevel Level { get; set; }
    }
}

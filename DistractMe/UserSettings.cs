using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistractMe
{
    public class UserSettings
    {
        public int DistractionInterval { get; set; }
        public string[] Messages { get; set; }
        public int BlockInterval { get; set; }
        public int NoOfMessages { get; set; }
    }
}

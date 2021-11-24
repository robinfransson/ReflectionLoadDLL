using ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleIO
{
    class Helper : IHelper
    {
        public Action<KeyboardEvent> OnKeyPress { get; set; }
    }
}

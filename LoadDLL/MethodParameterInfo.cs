using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModLoader
{
    public class MethodParameterInfo
    {
        public int Position { get; set; }
        public string Name { get; set; }
        public Type ParameterType { get; set; }
    }
}

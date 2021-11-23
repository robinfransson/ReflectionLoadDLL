using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModLoader
{
    public interface IMod
    {

        string ModName { get; }
        void OnLoad();
        void Test();
    }
}

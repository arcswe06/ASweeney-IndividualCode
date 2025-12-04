using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.MenuSystem
{
    internal abstract class MenuItem
    {
        public abstract void Select();
        public abstract string MenuText();
    }
}

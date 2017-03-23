using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZumConsole
{
    static class GlobalVars
    {
 
        private static string _globalVar = "";

        public static string GlobalVar
        {
            get { return _globalVar; }
            set { _globalVar = value; }
        }
        public static void SetGlobal(string instring)
        {
            _globalVar = instring;
        }
        public static string GetGlobal()
        {
            return _globalVar;
        }
    }

 
}

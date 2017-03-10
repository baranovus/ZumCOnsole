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

    public sealed class SingleGlobal
    {
        private static SingleGlobal instance = null;
        private static readonly object padlock = new object();
        private static string _globalVar = "";
        SingleGlobal()
        {
        }

        public static SingleGlobal Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SingleGlobal();
                    }
                    return instance;
                }
            }
        }
  
        public void SetGlobal(string instring)
        {
            _globalVar = instring;
        }
        public string GetGlobal()
        {
            return _globalVar;
        }

    }
}

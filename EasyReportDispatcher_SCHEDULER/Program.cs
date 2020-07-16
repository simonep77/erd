using EasyReportDispatcher_SCHEDULER.src;
using EasyReportDispatcher_SCHEDULER.src.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EasyReportDispatcher_SCHEDULER
{
    class Program
    {
        static int Main(string[] args)
        {
            AppContextERD.Service.RunByMode(searchRunMode(args));

            return 0;
        }


        static int searchRunMode(string[] args)
        {
            foreach (var arg in args)
            {
                switch (arg.ToLower())
                {
                    case @"/console":
                        return CostantiSched.RunMode.Console;
                    case @"/install":
                        return CostantiSched.RunMode.Install;
                    case @"/uninstall":
                        return CostantiSched.RunMode.Uninstall;
                }
            }

            return CostantiSched.RunMode.Service;

        }
    }
}

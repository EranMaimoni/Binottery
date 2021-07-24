using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Binottery.Log
{
    public static class LogManager
    {
        public static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    }
}

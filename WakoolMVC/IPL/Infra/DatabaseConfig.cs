using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreator.Infra
{
    public static class DatabaseConfig
    {
        public static bool DatabaseCreator;
        public static bool DatabaseChecker;
        public static bool CheckTabel;
        public const string DatabaseName = "WAKOOL.mdf";
        public static string FileName;
    }
}

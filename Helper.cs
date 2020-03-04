using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SaveToDB
{
    public static class Helper
    {
        public static string ConnectionValue(string name)   //returns the connectionstring based on name
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}

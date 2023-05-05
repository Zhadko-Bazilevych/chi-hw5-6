using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW5_6.dbHelpers
{
    abstract class AbstractDbHelper
    {
        protected string connectionString;

        public AbstractDbHelper()
        {
            var json = JObject.Parse(File.ReadAllText("appsettings.json"));
            var connectionToken = json.SelectToken("ConnectionString");
            if (connectionToken != null)
            {
                connectionString = (string)connectionToken;
            }
            else
            {
                connectionString =  "";
            }
        }
    }
}

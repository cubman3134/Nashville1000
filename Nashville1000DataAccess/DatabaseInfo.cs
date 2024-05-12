using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nashville1000DataAccess
{
    public class DatabaseInfo : Interfaces.DatabaseInterface
    {
        private static DatabaseInfo _instance;
        public static DatabaseInfo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseInfo();
                }
                return _instance;
            }
        }

        public override string ConnectionString
        {
            get
            {
                return "Server=localhost\\SQLEXPRESS;Database=Nashville1000;Trusted_Connection=True;";
            }
        }
    }
}

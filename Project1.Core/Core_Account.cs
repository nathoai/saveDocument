using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Project1.Domain;

namespace Project1.Core
{
    public class Core_Account : Base
    {
        public DataTable GetAcc()
        {
            return ExecuteQuery(@"EXEC sp_getAcc");
        }

        public DataTable CheckInfAcc(string user)
        {
            string query = string.Format(@"EXEC sp_infAcc {0}", user);
            return ExecuteQuery(query);
        }

        public bool InsertAcc(Account acc)
        {
            string query = string.Format(@"EXEC sp_insAcc {0}, N'{1}', '{2}', '{3}', '{4}'", acc.id_per, acc.name, acc.username, acc.pass, strTemp);
            return ExecuteNonQuery(query) > 0 ? true : false;
        }

        public bool UpdateAcc(Account acc)
        {
            string query = string.Format(@"EXEC sp_upAcc {0}, N'{1}', '{2}', '{3}', '{4}', '{5}'", acc.id_per, acc.name, acc.username, acc.pass, strTemp, acc.id);
            return ExecuteNonQuery(query) > 0 ? true : false;
        }

        public bool DelAcc(int id)
        {
            string query = string.Format(@"EXEC sp_delAcc {0}", id);
            return ExecuteNonQuery(query) > 0 ? true : false;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Project1.Domain;

namespace Project1.Core
{
    public class Core_Role : Base
    {
        public DataTable GetAll_Role()
        {
            string query = @"SELECT id, name, descr
                             FROM tb_per
                             WHERE stt = 0  
                            ";
            return ExecuteQuery(query);
        }

        public DataTable CheckInfo_Role(string name)
        {
            string query = string.Format(@"SELECT id FROM tb_per WHERE name = '{0}'", name);
            return ExecuteQuery(query);
        }

        public bool Insert_Role(Role rol)
        {
            string query = string.Format(@"INSERT INTO tb_per(name, descr, created, stt) 
                                          VALUES ('{0}', N'{1}', getdate(), 0)", rol.name, rol.descr);
            return ExecuteNonQuery(query) > 0 ? true : false;
        }

        public bool Update_Role(Role rol)
        {
            string sql = string.Format(@"UPDATE tb_per SET name = '{0}', descr = N'{1}', updated = getdate() WHERE id = {2}",
                                                    rol.name, rol.descr, rol.id);
            return ExecuteNonQuery(sql) > 0 ? true : false;
        }

        public bool Delete_Role(long id)
        {
            string sql = string.Format(@"UPDATE tb_per SET stt = 1, updated = getdate() WHERE id = {0}", id);
            return ExecuteNonQuery(sql) > 0 ? true : false;
        }
    }
}

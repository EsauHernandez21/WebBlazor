using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDAL.DALOracle
{
    public  class OracleDbContext
    {

        //Esta clase se encarga de manejar la creación de conexiones a la base de datos Oracle.
        private readonly string _connectionString;

        public OracleDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public OracleConnection CreateConnection()
        {
            return new OracleConnection(_connectionString);
        }
    }
}

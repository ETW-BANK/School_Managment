using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagment.DBconn
{
    internal class Connect
    {
        public SqlConnection _Conn;

        public Connect(SqlConnection Conn)

        {
            this._Conn = Conn;

        }

        public string DBConnect()

        {
            _Conn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SMSDB;Integrated Security=True;";

            _Conn.Open();

            return _Conn.ConnectionString;


        }

    }
    }



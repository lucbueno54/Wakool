using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure
{
    /// <summary>
    /// Classe responsável por gerenciar as conexões com a base de dados.
    /// </summary>
    class ConnectionHelper : IDisposable
    {
        private SqlConnection connection;
        private static string connectionString;

        // Construtor estático que é executado apenas uma vez pela aplicação, permitindo que façamos algum tipo de inicialização mais custosa.
        static ConnectionHelper()
        {
            connectionString = string.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFileName=C:\Users\Public\{0};Integrated Security=True;Connect Timeout=30", "WAKOOL.mdf");
        }

        // Construtor não estático que é executado toda vez que o objeto foi instanciado.
        public ConnectionHelper()
        {
            // Na medida em que um ConnectionHelper é criado, um SqlConnection também é criado.
            connection = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void Dispose()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public void Setup(SqlCommand command)
        {
            command.Connection = connection;
            OpenConnection();
        }
    }
}

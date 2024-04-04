using MySql.Data.MySqlClient;

namespace UniVal.DAOs
{
    public class ConnectionFactory
    {
        public static MySqlConnection Build()
        {
            return new MySqlConnection("Server=localhost;Database=UniVal;Uid=root;Pwd=root;");
        }
    }
}

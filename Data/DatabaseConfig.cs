
using MySql.Data.MySqlClient;

namespace CarRentalSystem.Data;

public static class DatabaseConfig
{
    public static string ConnectionString =
        "Server=localhost;Database=car_rental_db;Uid=root;Pwd=;";

    public static MySqlConnection CreateConnection()
    {
        return new MySqlConnection(ConnectionString);
    }
}

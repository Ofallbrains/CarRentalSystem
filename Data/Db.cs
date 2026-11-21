using System.Data;
using MySql.Data.MySqlClient;
namespace CarRentalSystem.Data;
public static class Db {
 public static DataTable Query(string sql, params MySqlParameter[] p){using var c=DatabaseConfig.CreateConnection();using var cmd=new MySqlCommand(sql,c);cmd.Parameters.AddRange(p);using var a=new MySqlDataAdapter(cmd);var t=new DataTable();a.Fill(t);return t;}
 public static int Execute(string sql, params MySqlParameter[] p){using var c=DatabaseConfig.CreateConnection();c.Open();using var cmd=new MySqlCommand(sql,c);cmd.Parameters.AddRange(p);return cmd.ExecuteNonQuery();}
 public static MySqlParameter P(string n,object? v)=>new(n,v??DBNull.Value);
}
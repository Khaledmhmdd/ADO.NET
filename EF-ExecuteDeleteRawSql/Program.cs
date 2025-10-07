using System.Data;
using EF_ExcuteRawSql;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var conn = new SqlConnection(config.GetSection("constr").Value);

var sql = "DELETE FROM Wallets WHERE Id = @Id ;";

SqlParameter idParameter = new SqlParameter
{
    ParameterName = "@Id",
    SqlDbType = SqlDbType.Decimal,
    Direction = ParameterDirection.Input,
    Value = 9

};

SqlCommand command = new SqlCommand(sql, conn);
command.CommandType = CommandType.Text;

command.Parameters.Add(idParameter);


conn.Open();

if (command.ExecuteNonQuery() > 0)
{
    Console.WriteLine($"Wallet Deleted successfully ");

}



conn.Close();
Console.ReadKey();


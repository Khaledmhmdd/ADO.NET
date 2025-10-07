using System.Data;
using EF_ExcuteRawSql;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var conn = new SqlConnection(config.GetSection("constr").Value);

var sql = "SELECT * FROM WALLETS";

SqlCommand command = new SqlCommand(sql, conn);
command.CommandType = CommandType.Text;

conn.Open();
SqlDataReader reader = command.ExecuteReader();
Wallet wallet;
while (reader.Read())
{
    wallet = new Wallet
    {
        Id = reader.GetInt32("Id"),
        Holder = reader.GetString("Holder"),
        Balance = reader.GetDecimal("Balance"),
    };
    Console.WriteLine(wallet);
}
conn.Close();
Console.ReadKey();


using System.Data;
using EF_ExcuteRawSql;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var conn = new SqlConnection(config.GetSection("constr").Value);

var sql = "UPDATE Wallets SET Holder = @Holder ,Balance = @Balance WHERE Id = @Id ;";

SqlParameter holderParameter = new SqlParameter
{
    ParameterName = "@Holder",
    SqlDbType = SqlDbType.NVarChar,
    Direction = ParameterDirection.Input,
    Value = "Ahmedd"

};
SqlParameter balanceParameter = new SqlParameter
{
    ParameterName = "@Balance",
    SqlDbType = SqlDbType.Decimal,
    Direction = ParameterDirection.Input,
    Value = 9000

};
SqlParameter idParameter = new SqlParameter
{
    ParameterName = "@Id",
    SqlDbType = SqlDbType.Decimal,
    Direction = ParameterDirection.Input,
    Value = 1

};

SqlCommand command = new SqlCommand(sql, conn);
command.CommandType = CommandType.Text;

command.Parameters.Add(holderParameter);
command.Parameters.Add(balanceParameter);
command.Parameters.Add(idParameter);


conn.Open();

if (command.ExecuteNonQuery() > 0)
{
    Console.WriteLine($"Wallet  Updated successfully ");

}



conn.Close();
Console.ReadKey();


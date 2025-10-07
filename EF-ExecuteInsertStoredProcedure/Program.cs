using System.Data;
using EF_ExcuteRawSql;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

Wallet walletToInsert = new Wallet
{
    Holder = "Khaled",
    Balance = 8000
};

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var conn = new SqlConnection(config.GetSection("constr").Value);

SqlParameter holderParameter = new SqlParameter
{
    ParameterName = "@Holder",
    SqlDbType = SqlDbType.NVarChar,
    Direction = ParameterDirection.Input,
    Value = walletToInsert.Holder

};
SqlParameter balanceParameter = new SqlParameter
{
    ParameterName = "@Balance",
    SqlDbType = SqlDbType.Decimal,
    Direction = ParameterDirection.Input,
    Value = walletToInsert.Balance

};
SqlCommand command = new SqlCommand("AddWallet", conn);
command.CommandType = CommandType.StoredProcedure;

command.Parameters.Add(holderParameter);
command.Parameters.Add(balanceParameter);


conn.Open();

if (command.ExecuteNonQuery()>0)
{
    Console.WriteLine($"Wallet for {walletToInsert.Holder} Inserted successfully ");

}



conn.Close();
Console.ReadKey();


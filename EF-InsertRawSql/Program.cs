using System.Data;
using EF_ExcuteRawSql;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

Wallet walletToInsert = new Wallet
{
    Holder = "Menna",
    Balance = 7000
};

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var conn = new SqlConnection(config.GetSection("constr").Value);

var sql = "INSERT INTO WALLETS (Holder,Balance) VALUES (@Holder,@Balance)";
SqlParameter holderParameter = new SqlParameter { 
    ParameterName = "@Holder",
    SqlDbType = SqlDbType.NVarChar,
    Direction = ParameterDirection.Input,
    Value = walletToInsert.Holder

};
SqlParameter balanceParameter = new SqlParameter { 
    ParameterName = "@Balance",
    SqlDbType = SqlDbType.Decimal,
    Direction = ParameterDirection.Input,
    Value = walletToInsert.Balance

};
SqlCommand command = new SqlCommand(sql, conn);
command.CommandType = CommandType.Text;

command.Parameters.Add(holderParameter);
command.Parameters.Add(balanceParameter);


conn.Open();

if (command.ExecuteNonQuery() > 0)
{
    Console.WriteLine($"Wallet for {walletToInsert.Holder} Inserted successfully");
}
else 
{
    Console.WriteLine($"Error happened while inserting wallet for {walletToInsert.Holder}");
}


    conn.Close();
Console.ReadKey();


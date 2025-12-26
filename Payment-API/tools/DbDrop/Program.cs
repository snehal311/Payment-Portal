using System;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

// Load the appsettings.json from the main Payment.API project
var settingsPath = "e:\\Projects\\Payment-portal\\Payment-API\\Payment.API\\appsettings.json";
var builder = new ConfigurationBuilder()
    .AddJsonFile(settingsPath, optional: false, reloadOnChange: false);

var config = builder.Build();
var conn = config.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(conn))
{
    Console.Error.WriteLine("No DefaultConnection found in appsettings.json");
    return 1;
}

try
{
    using var c = new SqlConnection(conn);
    c.Open();
    var cmd = c.CreateCommand();
    cmd.CommandText = "IF OBJECT_ID('dbo.Payment','U') IS NOT NULL DROP TABLE dbo.Payment;";
    cmd.ExecuteNonQuery();
    Console.WriteLine("Dropped table 'Payment' if it existed.");
    return 0;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Error: {ex.Message}");
    return 2;
}

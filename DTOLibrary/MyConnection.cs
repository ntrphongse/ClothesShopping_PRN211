using Microsoft.Extensions.Configuration;
using System.IO;

namespace DTOLibrary
{
    public static class MyConnection
    {
        public static string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", true, true)
                                    .Build();
            var strConnection = config["ConnectionString:ClothesShoppingDB"];
            return strConnection;
        }
    }
}

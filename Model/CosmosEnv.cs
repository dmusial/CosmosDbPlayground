
namespace CosmosDBTest.Model
{
    public static class CosmosEnv
    {
        public static string FlightsDatabaseName = "FlightsDatabase";
        public static string FlightsCollectionName = "FlightsCollection";
        public static string FlightsPartitionKey = "/Origin";
    }
}
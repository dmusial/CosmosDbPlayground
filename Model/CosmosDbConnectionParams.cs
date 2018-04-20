using System;

namespace CosmosDBTest.Model
{
    public class CosmosDbConnectionParams
    {
        public readonly Uri CosmosEndpointUri = new Uri("####");
        public readonly string CosmosPrimaryKey = "####";
        
        public CosmosDbConnectionParams() { }

        public CosmosDbConnectionParams(Uri cosmosEndpointUri, string cosmosKey)
        {
            CosmosEndpointUri = cosmosEndpointUri;
            CosmosPrimaryKey = cosmosKey;
        }
    }
}
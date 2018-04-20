using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CosmosDBTest.Model.Seeders;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace CosmosDBTest.Model
{
    public class FlightsContext : IDisposable
    {
        private DocumentClient _client;

        public FlightsContext(CosmosDbConnectionParams connectionParams)
        {
            _client = new DocumentClient(
                connectionParams.CosmosEndpointUri, 
                connectionParams.CosmosPrimaryKey);
        }

        public string FlightsDatabaseCollectionSelfLink { get; private set; }
        public string FlightsDocumentCollectionSelfLink { get; private set; }

        public async Task InitializeAsync()
        {
            await AssureDatabaseExists(
                CosmosEnv.FlightsDatabaseName, 
                _client);
            await AssureCollectionExists(
                CosmosEnv.FlightsCollectionName, 
                CosmosEnv.FlightsPartitionKey, 
                CosmosEnv.FlightsDatabaseName, 
                _client);
        }

        public async Task SeedAsync(IEnumerable<ISeeder> seeders)
        {
            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(_client);
            }
        }

        public async Task<IEnumerable<T>> QueryDocumentAsync<T>(string query, string documentCollectionSelfLink)
        {
            var queryOptions = new FeedOptions { MaxItemCount = -1 };

            return _client.CreateDocumentQuery<T>(
                documentCollectionSelfLink, new SqlQuerySpec(query), queryOptions);
        }

        private async Task AssureDatabaseExists(string databaseName, DocumentClient client)
        {  
            var database = new Database { Id = databaseName };
            database = await client.CreateDatabaseIfNotExistsAsync(database);
            FlightsDatabaseCollectionSelfLink = database.SelfLink;
        }

        private async Task AssureCollectionExists(string collectionName, string partitionKey, 
            string databaseName, DocumentClient client)
        {
            RequestOptions requestOptions = new RequestOptions
            {
                OfferThroughput = 600
            };

            PartitionKeyDefinition partitionKeyDefinition = new PartitionKeyDefinition
            {
                Paths = new Collection<string> { partitionKey }
            };

            var collection = new DocumentCollection 
            {
                 Id = collectionName,
                 PartitionKey = partitionKeyDefinition
            };

            collection = await client.CreateDocumentCollectionIfNotExistsAsync(
                CosmosUtils.GetDatabaseSelfLink(databaseName),
                collection,
                requestOptions);

            FlightsDocumentCollectionSelfLink = collection.SelfLink;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
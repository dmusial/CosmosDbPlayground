using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CosmosDBTest.Model;
using CosmosDBTest.Model.Seeders;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace CosmosDBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var flightsContext = new FlightsContext(new CosmosDbConnectionParams()))
            {
                flightsContext.InitializeAsync().Wait();
                var flightsSeeder = new FlightsCollectionSeeder(flightsContext.FlightsDocumentCollectionSelfLink);
                flightsContext.SeedAsync(new ISeeder[] { flightsSeeder } ).Wait();

                QueryData(flightsContext).Wait();
            }
        }

        private static async Task QueryData(FlightsContext flightsContext)
        {
            var query = "SELECT * FROM flights WHERE flights.Origin = 'SFO'";
            var flights = await flightsContext.QueryDocumentAsync<Flight>(
                query, 
                flightsContext.FlightsDocumentCollectionSelfLink);

            foreach (var flight in flights)
            {
                await Console.Out.WriteLineAsync(
                    $"[{flight.Number}]\t{flight.Origin} -> {flight.Destination}");
            }
        }
    }
}

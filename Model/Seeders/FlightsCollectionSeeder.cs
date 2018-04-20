using System.Threading.Tasks;
using Microsoft.Azure.Documents;

namespace CosmosDBTest.Model.Seeders
{
    public class FlightsCollectionSeeder : ISeeder
    {
        private string _documentCollectionSelfLink;

        public FlightsCollectionSeeder(string documentCollectionSelfLink)
        {
            _documentCollectionSelfLink = documentCollectionSelfLink;
        }

        public async Task SeedAsync(IDocumentClient client)
        {
            var airportCodes = new string[] { "SFO", "MIA", "KRK", "FRA", "EWR", "WAW", "CDG", "LHR" };
            var flights = new Bogus.Faker<Flight>()
                .RuleFor(f => f.Number, (fake) => fake.Random.Number(300, 500))
                .RuleFor(f => f.Origin, (fake) => fake.PickRandom(airportCodes))
                .RuleFor(f => f.Destination, (fake) => fake.PickRandom(airportCodes))
                .GenerateLazy(100);
            
            foreach (var flight in flights)
            {
                var result = await client.CreateDocumentAsync(_documentCollectionSelfLink, flight);
            }
        }
    }
}
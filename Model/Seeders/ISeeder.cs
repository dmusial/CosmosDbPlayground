using System.Threading.Tasks;
using Microsoft.Azure.Documents;

namespace CosmosDBTest.Model.Seeders
{
    public interface ISeeder
    {
        Task SeedAsync(IDocumentClient client);
    }
}
using System;
using Microsoft.Azure.Documents.Client;

namespace CosmosDBTest
{
    public static class CosmosUtils
    {
        public static Uri GetDatabaseSelfLink(string databaseName)
        {
            return UriFactory.CreateDatabaseUri(databaseName);
        }

        public static Uri GetCollectionSelfLink(string databaseName, string collectionName)
        {
            return UriFactory.CreateDocumentCollectionUri(databaseName, collectionName);
        }
    }
}
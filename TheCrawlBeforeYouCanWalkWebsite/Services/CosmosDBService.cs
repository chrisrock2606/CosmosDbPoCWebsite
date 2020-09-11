namespace TheCrawlBeforeYouCanWalkWebsite.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Fluent;
    using Microsoft.Extensions.Configuration;
    using TheCrawlBeforeYouCanWalkWebsite.Models;


    public class CosmosDbService
    {
        private Container _container;

        public CosmosDbService(CosmosClient dbClient, string databaseName, string containerName)
        {
            _container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task<Person> AddItemAsync(Person person)
        {
            return await _container.CreateItemAsync(person, new PartitionKey(person.Id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await _container.DeleteItemAsync<Person>(id, new PartitionKey(id));
        }

        public async Task<Person> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Person> response = await _container.ReadItemAsync<Person>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Person>> GetItemsAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Person>(new QueryDefinition(queryString));
            List<Person> results = new List<Person>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, Person item)
        {
            await _container.UpsertItemAsync(item, new PartitionKey(id));
        }
    }
}

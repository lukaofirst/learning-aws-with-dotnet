using Amazon.DynamoDBv2.DataModel;
using Domain.Entities;
using Domain.Interfaces;

namespace Infra
{
	public class PersonRepository : IPersonRepository
	{
		private readonly IDynamoDBContext _dynamoDBContext;

		public PersonRepository(IDynamoDBContext dynamoDBContext)
		{
			_dynamoDBContext = dynamoDBContext;
		}

		public async Task<List<Person>> GetAll()
		{
			var search = _dynamoDBContext.ScanAsync<Person>(null);
			var entities = await search.GetRemainingAsync();

			return entities;
		}

		public async Task<Person> GetByUniqueKey(string partitionKey, string sortingKey)
		{
			var entity = await _dynamoDBContext.LoadAsync<Person>(partitionKey, sortingKey);

			return entity;
		}

		public async Task<Person> Create(Person person)
		{
			person.SetPartitionKey(Guid.NewGuid().ToString());
			person.SetSortingKey(Guid.NewGuid().ToString());
			await _dynamoDBContext.SaveAsync(person);

			return person;
		}

		public async Task<Person> Update(Person person)
		{
			await _dynamoDBContext.SaveAsync(person);

			return person;
		}

		public async Task<bool> Delete(string partitionKey, string sortingKey)
		{
			var deletePerson = new Person(partitionKey, sortingKey);

			await _dynamoDBContext.DeleteAsync(deletePerson);

			return true;
		}
	}
}
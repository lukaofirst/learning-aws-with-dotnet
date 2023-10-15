using Domain.Entities;

namespace Domain.Interfaces;

public interface IPersonRepository
{
	Task<List<Person>> GetAll();
	Task<Person> GetByUniqueKey(string partitionKey, string sortingKey);
	Task<Person> Create(Person person);
	Task<Person> Update(Person person);
	Task<bool> Delete(string partitionKey, string sortingKey);
}
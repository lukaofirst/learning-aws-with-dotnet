using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces;

public interface IPersonService
{
	Task<List<PersonViewModelDTO>> GetAll();
	Task<PersonViewModelDTO> GetByUniqueKey(string partitionKey, string sortingKey);
	Task<Person> Create(PersonInputModelDTO person);
	Task<Person> Update(PersonViewModelDTO person);
	Task<bool> Delete(string partitionKey, string sortingKey);
}
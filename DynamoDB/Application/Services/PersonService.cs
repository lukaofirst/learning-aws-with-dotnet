using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class PersonService : IPersonService
{
	private readonly IPersonRepository _personRepository;
	private readonly IMapper _mapper;

	public PersonService(IPersonRepository personRepository, IMapper mapper)
	{
		_mapper = mapper;
		_personRepository = personRepository;
	}

	public async Task<List<PersonViewModelDTO>> GetAll()
	{
		var entities = await _personRepository.GetAll();
		var mappedEntities = _mapper.Map<List<PersonViewModelDTO>>(entities);

		return mappedEntities;
	}

	public async Task<PersonViewModelDTO> GetByUniqueKey(string partitionKey, string sortingKey)
	{
		var entity = await _personRepository.GetByUniqueKey(partitionKey, sortingKey)
			?? throw new Exception($"{nameof(Person)} entity is null");

		var mappedEntity = _mapper.Map<PersonViewModelDTO>(entity);

		return mappedEntity;
	}

	public async Task<Person> Create(PersonInputModelDTO person)
	{
		var mappedEntity = _mapper.Map<Person>(person);
		var createdEntity = await _personRepository.Create(mappedEntity);

		return createdEntity;
	}

	public async Task<Person> Update(PersonViewModelDTO person)
	{
		var mappedEntity = _mapper.Map<Person>(person);
		var updatedEntity = await _personRepository.Update(mappedEntity);

		return updatedEntity;
	}

	public async Task<bool> Delete(string partitionKey, string sortingKey)
	{
		var isDeletedEntity = await _personRepository.Delete(partitionKey, sortingKey);

		return isDeletedEntity;
	}
}
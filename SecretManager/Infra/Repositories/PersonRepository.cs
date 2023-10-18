using Domain.Entities;
using Domain.Interfaces;
using Infra.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Infra
{
	public class PersonRepository : IPersonRepository
	{
		private readonly DataContext _dataContext;

		public PersonRepository(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public async Task<List<Person>> GetAll()
		{
			var entities = await _dataContext.Persons
				.AsNoTracking()
				.ToListAsync();
			return entities;
		}

		public async Task<Person>? GetById(int id)
		{
			var entity = await _dataContext.Persons
				.AsNoTracking()
				.Where(x => x.Id.Equals(id))
				.FirstOrDefaultAsync();

			return entity!;
		}

		public async Task<Person> Create(Person person)
		{
			await _dataContext.Persons.AddAsync(person);

			var result = await _dataContext.SaveChangesAsync();

			return result > 0 ? person : throw new Exception("Could not insert person!");
		}

		public async Task<Person> Update(Person updatedPerson)
		{
			var person = await _dataContext.Persons
				.Where(x => x.Id.Equals(updatedPerson.Id))
				.FirstOrDefaultAsync()
					?? throw new Exception("Could not update person entity, because person is null");

			person.UpdateFields(updatedPerson);

			_dataContext.Persons.Update(person!);

			var result = await _dataContext.SaveChangesAsync();

			return result > 0 ? person : throw new Exception("Could not update person entity");
		}

		public async Task<bool> Delete(int id)
		{
			var person = await _dataContext.Persons
				.Where(x => x.Id.Equals(id))
				.FirstOrDefaultAsync()
					?? throw new Exception("Could not delete person entity, because person is null");

			_dataContext.Persons.Remove(person);

			var result = await _dataContext.SaveChangesAsync();

			return result > 0;
		}
	}
}
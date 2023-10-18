using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
	private readonly IPersonService _personService;

	public PersonController(IPersonService personService)
	{
		_personService = personService;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var entities = await _personService.GetAll();

		return Ok(entities);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		try
		{
			var entity = await _personService.GetById(id);

			return Ok(entity);
		}
		catch (Exception ex)
		{
			return BadRequest(ex);
		}
	}

	[HttpPost]
	public async Task<IActionResult> Post(PersonInputModelDTO person)
	{
		var createdEntity = await _personService.Create(person);

		return Ok(createdEntity);
	}

	[HttpPut]
	public async Task<IActionResult> Update(PersonViewModelDTO person)
	{
		var updatedEntity = await _personService.Update(person);

		return Ok(updatedEntity);
	}

	[HttpDelete]
	public async Task<IActionResult> Delete(int id)
	{
		var isDeletedEntity = await _personService.Delete(id);

		return Ok(isDeletedEntity);
	}
}
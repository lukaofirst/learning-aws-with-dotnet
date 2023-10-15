using System.ComponentModel.DataAnnotations;
using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
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

	[HttpGet(Name = "GetAll")]
	public async Task<IActionResult> GetAll()
	{
		var entities = await _personService.GetAll();

		return Ok(entities);
	}

	[HttpGet("{partitionKey}/{sortingKey}", Name = "GetByUniqueKey")]
	public async Task<IActionResult> GetByUniqueKey([Required] string partitionKey, [Required] string sortingKey)
	{
		try
		{
			var entity = await _personService.GetByUniqueKey(partitionKey, sortingKey);

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
	public async Task<IActionResult> Delete([Required] string partitionKey, [Required] string sortingKey)
	{
		var isDeletedEntity = await _personService.Delete(partitionKey, sortingKey);

		return Ok(isDeletedEntity);
	}
}
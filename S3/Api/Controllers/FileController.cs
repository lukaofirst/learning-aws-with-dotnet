using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
	private readonly IS3Service _s3Service;

	public FileController(IS3Service s3Service)
	{
		_s3Service = s3Service;
	}

	[HttpGet("{fileName}")]
	public async Task<IActionResult> Get(string fileName)
	{
		try
		{
			var (fileBytes, contentType) = await _s3Service.GetFileAsync(fileName);

			return File(fileBytes, contentType);
		}
		catch (Exception ex)
		{
			return BadRequest(ex);
		}
	}

	[HttpPost]
	public async Task<IActionResult> Post(IFormFile formFile)
	{
		try
		{
			var result = await _s3Service.UploadFileAsync(formFile);

			return Ok(result);
		}
		catch (Exception ex)
		{
			return BadRequest(ex);
		}
	}

	[HttpDelete("{fileName}")]
	public async Task<IActionResult> Delete(string fileName)
	{
		try
		{
			var result = await _s3Service.DeleteFileAsync(fileName);

			return Ok(result);
		}
		catch (Exception ex)
		{
			return BadRequest(ex);
		}
	}
}
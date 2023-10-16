using Domain.Entities;
using Domain.Interfaces;
using Domain.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Producer.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MessageController : ControllerBase
	{
		private readonly IPublishService _publishService;
		private readonly IOptions<AWSServices> _options;

		public MessageController(IPublishService publishService, IOptions<AWSServices> options)
		{
			_options = options;
			_publishService = publishService;
		}

		[HttpGet]
		public IActionResult Check()
		{
			var awsServices = _options.Value;

			return Ok(awsServices);
		}

		[HttpPost]
		public async Task<IActionResult> PostMessage(DomainMessage message)
		{
			try
			{
				var result = await _publishService.SendAsync(message);

				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
	}
}
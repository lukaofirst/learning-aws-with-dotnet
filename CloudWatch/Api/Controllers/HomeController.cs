using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class HomeController : ControllerBase
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IActionResult Get()
		{
			_logger.LogInformation("Executing {method}", nameof(Get));

			for (int i = 0; i < 10; i++)
			{
				_logger.LogInformation("Performing {currentIteration} execution", i);
			}

			_logger.LogInformation("Ending {method}", nameof(Get));

			return Ok();
		}
	}
}
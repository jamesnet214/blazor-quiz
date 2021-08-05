using BlazorQuiz.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorQuiz.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class QuizController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Iron Man", "Spider Man", "BlackWidow", "Thor", "Captain Marvel", "Gamora", "Loki", "Falcon", "Captain America", "Groot"
		};

		private readonly ILogger<QuizController> logger;

		public QuizController(ILogger<QuizController> logger)
		{
			this.logger = logger;
		}

		[HttpGet]
		public IEnumerable<WeatherForecast> Get()
		{
			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			})
			.ToArray();
		}
	}
}

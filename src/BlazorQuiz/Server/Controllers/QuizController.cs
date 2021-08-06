using BlazorQuiz.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BlazorQuiz.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class QuizController : ControllerBase
	{
		private static string YamlDataUrl = "https://raw.githubusercontent.com/devncore/blazor-quiz/master/data/quiz-basic.yml";

		private readonly ILogger<WeatherForecastController> _logger;

		public QuizController(ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public QuizModel[] Get()
		{
			var yaml = new HttpClient().GetStringAsync(YamlDataUrl);
			var yamlData = yaml.Result;
			var quizs = ParsePlayer(yamlData);
			Shuffle<QuizModel>(quizs);

			return quizs.Take(5).ToArray();
		}

		public List<QuizModel> ParsePlayer(string ymlContents)
		{
			var deserializer = new DeserializerBuilder()
			  .WithNamingConvention(CamelCaseNamingConvention.Instance)
			  .Build();
			var result = deserializer.Deserialize<QuizPack>(ymlContents);
			return result.Quiz;
		}

		private void Shuffle<T>(IList<T> list)
		{
			Random rng = new Random();
			int n = list.Count;

			while (n > 1) {
				n--;
				int k = rng.Next(n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}
	}
}

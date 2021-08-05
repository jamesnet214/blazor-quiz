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
		private static readonly string[] Summaries = new[]
		{
			"IronMan", "SpiderMan", "BlackWidow", "Thanos", "Thor", "Loki", "Captain Marvel", "Captain America", "Wanda", "Vision"
		};

		private readonly ILogger<WeatherForecastController> _logger;

		public QuizController(ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public Quiz[] Get()
		{

			var q = new List<Quiz>();

			var an = new List<string>();
			an.Add("ASdf");
			an.Add("Asdf");
			q.Add(new Quiz { Question = "AS", Answers = an });

			var an2 = new List<string>();
			an2.Add("ASdf23");
			an2.Add("Asd22f");
			q.Add(new Quiz { Question = "AS22", Answers = an2 });

			QuizPack a1 = new QuizPack();
			a1.Quiz = q;
			ISerializer serializer = new SerializerBuilder()
				.WithNamingConvention(CamelCaseNamingConvention.Instance)
				.Build();

			string yaml = serializer.Serialize(a1);

			HttpClient webClient = new HttpClient();
			var a = webClient.GetStringAsync("https://raw.githubusercontent.com/devncore/blazor-quiz/master/data/quiz-data.yml");

			return ParsePlayer(a.Result).ToArray();
		}

		public static List<Quiz> ParsePlayer(string ymlContents)
		{
			var deserializer = new DeserializerBuilder()
			  .WithNamingConvention(CamelCaseNamingConvention.Instance)
			  .Build();
			var result = deserializer.Deserialize<QuizPack>(ymlContents);
			return result.Quiz;
		}
	}
}

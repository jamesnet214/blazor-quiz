# blazor-quiz

## Overview

간단한 퀴즈를 풀 수 있는 Blazor-WebAssembly 기반의 웹앱입니다.

> ![image](https://user-images.githubusercontent.com/52397976/129287383-de3e1e1d-1b41-46e5-8755-ecf2ee78c12f.png)


[바로가기](https://blazor-quiz.azurewebsites.net/Quiz)

### 개발 환경
- Blazor-WebAssembly

### 웹서버 호스팅
- MS Azure WebApp 배포 (무료 F1)

### 데이터
- 별도의 데이터베이스 없이 GitHub 레포지터리에서 `.yaml` 파일 로드  
  https://github.com/devncore/blazor-quiz/blob/master/data/quiz-basic.yml

### Yaml 파일을 읽어오는 부분

```csharp
[ApiController]
[Route("[controller]")]
public class QuizDataController : ControllerBase
{
	private static string YamlDataUrl = "https://raw.githubusercontent.com/devncore/blazor-quiz/master/data/quiz-basic.yml";

	private readonly ILogger<WeatherForecastController> _logger;

	public QuizDataController(ILogger<WeatherForecastController> logger)
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

		foreach (var quiz in quizs)
		{
			Shuffle<AnswerModel>(quiz.Answers);
		}

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

		while (n > 1) 
		{
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}
}
```





# blazor-quiz

#### 👉 [웹사이트 방문](https://blazor-quiz.azurewebsites.net/Quiz)

## Overview

> 간단한 퀴즈를 풀 수 있는 Blazor-WebAssembly 기반의 웹앱입니다.

<img src="https://user-images.githubusercontent.com/74305823/129328930-6ca3e1e5-83f6-429a-af6a-63d81bd28624.png" width="400"/>

<img src="https://user-images.githubusercontent.com/74305823/129329047-0e6dd8ae-f0a4-4536-a18e-ccedf3f8033a.png" width="400"/>

### 개발 환경
- Blazor-WebAssembly

### 웹서버 호스팅
- MS Azure WebApp 배포 (무료 F1)

### 데이터
- 별도의 데이터베이스 없이 GitHub 레포지터리에서 `.yaml` 파일 로드  
  https://github.com/devncore/blazor-quiz/blob/master/data/quiz-basic.yml

### Yaml Parsing

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



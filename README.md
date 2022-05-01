## Blazor Quiz

Blazor Quiz는 간단한 퀴즈를 풀 수 있는 <b><code>Blazor-WebAssembly</code></b> 기반 웹앱입니다.   <br/>
👉 <a href="https://blazor-quiz.azurewebsites.net/Quiz"><strong>퀴즈 풀러가기</strong></a>

<a href="https://github.com/devncore/devncore"><strong>더 알아보기 »</strong></a>
 
| Star | License | Activity |
|:----:|:-------:|:--------:|
| <a href="https://github.com/devncore/blazor-quiz/stargazers"><img src="https://img.shields.io/github/stars/devncore/blazor-quiz" alt="Github Stars"></a> | <img src="https://img.shields.io/github/license/devncore/the-easiest-yaml" alt="License"> | <a href="https://github.com/devncore/blazor-quiz/pulse"><img src="https://img.shields.io/github/commit-activity/m/devncore/blazor-quiz" alt="Commits-per-month"></a> |

<br />

## 개발 환경
> Blazor-WebAssembly

<br />
  
## 웹서버 호스팅
> MS Azure WebApp 배포 (무료 F1)

<br />

## 데이터
> 별도 데이터베이스 없이 GitHub 레포지터리에서 [`.yaml` 파일](https://github.com/devncore/blazor-quiz/blob/master/data/quiz-basic.yml) 로드  

<br />

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

<br />
  
## 스크린샷

<img src="https://user-images.githubusercontent.com/74305823/129328930-6ca3e1e5-83f6-429a-af6a-63d81bd28624.png" width="450"/>

<img src="https://user-images.githubusercontent.com/74305823/129329047-0e6dd8ae-f0a4-4536-a18e-ccedf3f8033a.png" width="450"/>
	
<br />

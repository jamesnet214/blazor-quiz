using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorQuiz.Shared
{
    public class QuizModel
    {
        public string Question { get; set; }
        public List<object[]> Answers { get; set; }
    }
}

using Markdig;

namespace BlazorQuiz.Local.Access
{
    public class MarkdownAdapter
    {
        public static MarkdownAdapter Instance;

        static MarkdownAdapter()
        {
            Instance = new MarkdownAdapter();
        }

        private MarkdownAdapter()
        { 
            
        }

        private string Download(string url)
        {
            string contents;

            using (var wc = new System.Net.WebClient())
                contents = wc.DownloadString(url);
            return contents;
        }

        internal string DownloadToHtml(string url)
        {
            string text = Download(url);
            return Markdown.ToHtml(text);
        }
    }
}

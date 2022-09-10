using LibWebScrapper.Interfaces;
using LibWebScrapper.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
namespace LibWebScrapper.src
{
    public abstract class WebScrapperBase : IScrapper
    {
        public IEnumerable<string> KeyWords { get; set; }
        public IEnumerable<string> SearchEngines { get; set; }

        // Might change to configurable property for a more versatile implementation
        public const string urlToCheck = "www.sympli.com.au"; 
        protected HttpClient HttpClient { get; set; } = new();
        // no args constructor with vanilla HttpClient
        public WebScrapperBase(){
            KeyWords = new List<string>();
            SearchEngines = new List<string>();
        }
        // constructor with defined properties and vanilla HttpClient
        public WebScrapperBase(IEnumerable<string> keyWords, IEnumerable<string> searchEngines)
        {
            this.KeyWords = keyWords;
            this.SearchEngines = searchEngines;
        }

        //constructor with all self defined properties
        protected WebScrapperBase(IEnumerable<string>? keyWords, IEnumerable<string>? searchEngines, HttpClient httpClient) : this(keyWords, searchEngines)
        {
            HttpClient = httpClient;
        }

        /// <summary>
        /// base implementation of the GetRankingFromSearchingEngine method of IScrapper. This can be overridden in the derived classes.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<SearchEngineRankingModel>> GetRankingFromSearchEngine()
        {
            if (!SearchEngines.Any()) throw new Exception("require at least one search engine.");
            if (!KeyWords.Any()) throw new Exception("Require at least one keyword to search.");
            IEnumerable<SearchEngineRankingModel> results = new List<SearchEngineRankingModel>();
            foreach(string host in SearchEngines)
            {
                Uri uri = new (host);
                if (!uri.IsWellFormedOriginalString()) throw new Exception("search engine Uri is not in the expected format.");
                SearchEngineRankingModel result = new()
                {
                    SearchEngine = host
                };
                foreach (string keyWord in KeyWords)
                {
                    uri = new(uri.ToString() + $"/search?q={keyWord}");
                    
                    HttpRequestMessage request = new()
                    {
                        Method = HttpMethod.Get,
                        RequestUri = uri
                    };
                    HttpResponseMessage response = await HttpClient.SendAsync(request);
                    if (!response.IsSuccessStatusCode)
                    {
                        result.keyWordRanking.Add(keyWord, -1);
                        Console.Error.WriteLine("The search engine returned : "+ response.StatusCode.ToString());
                    }
                    string htmlResponse = await response.Content.ReadAsStringAsync();
                    int rank = RankingFromSearchEngine(htmlResponse);
                    result.keyWordRanking.Add(keyWord, rank);
                }

                results = results.Append(result);
            }
            return results;
        }

        /// <summary>
        /// Base implementation of RankingFromSearchEngine. Derived classes can override to change implementation details.
        /// </summary>
        /// <param name="searchEngineResult"></param>
        /// <returns></returns>
        protected virtual int RankingFromSearchEngine(string searchEngineResult)
        {
            const string regEx = @"(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?";
            var regExResults = Regex.Matches(searchEngineResult, regEx);
            var temp = regExResults.Select((elem, i) => elem.Value.Contains(urlToCheck) ? i : -1);
            var rank = temp.FirstOrDefault(fd => fd > -1);
            // the increment will return the rank instead of array index.
            return ++rank;
        }


    }
}

using LibWebScrapper.Interfaces;
using LibWebScrapper.Models;

namespace LibWebScrapper.src
{
    /// <summary>
    /// This class is used to scrap the search engines for links that contain www.sympli.com.au and return their ranking.
    /// 
    /// </summary>
    public class WebScrapper : WebScrapperBase, IScrapper
    {
        public WebScrapper():base(){}
        public WebScrapper(IEnumerable<string> keyWords, IEnumerable<string> searchEngines):base(keyWords, searchEngines){}
        /// <summary>
        /// Call this method to get the class object which can be converted to JSON or utilized as is.
        /// </summary>
        /// <returns>IEnumerable<SearchEngineRankingModel></returns>
        public new Task<IEnumerable<SearchEngineRankingModel>> GetRankingFromSearchEngine()
        {
            return base.GetRankingFromSearchEngine();
        }

        public Task<int> ScrapeSearchEngine()
        {
            throw new NotImplementedException();
        }

        public Task<int> SearchForRank(string httpResponseString)
        {
            throw new NotImplementedException();
        }
    }
}

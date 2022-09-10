using LibWebScrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibWebScrapper.Interfaces
{
    public interface IScrapper
    {
        /// <summary>
        /// Gets ranking of the keywords from the search engines specified.
        /// </summary>
        /// <returns>IEnumerable<SearchEngineRankingModel></returns>
        Task<IEnumerable<SearchEngineRankingModel>> GetRankingFromSearchEngine();
    }
}

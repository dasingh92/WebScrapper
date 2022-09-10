using LibWebScrapper.Interfaces;
using LibWebScrapper.Models;
using LibWebScrapper.src;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace SympliDevelopment.Api.Controllers
{
    [ApiController]
  [Route("[controller]")]
  public class SearchController : ControllerBase
  {
    private readonly IConfiguration _configuration;
    private readonly IMemoryCache cache;
        private const int CACHE_EXPIRY = 10;
    public SearchController(IConfiguration configuration, IMemoryCache memoryCache)
        {
            _configuration = configuration;
            cache = memoryCache;
        }
    [HttpGet("keywords")]
    public async Task<IActionResult> GetResult([FromQuery] string keywords)
    {
        //Cache expiry defined. By changing the CACHE_EXPIRY property we can change the frequency of calls made. (set to 10 seconds for faster testing else should be 3600 for the given scenario in extension 1. 
        MemoryCacheEntryOptions options = new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(CACHE_EXPIRY)
        };
        IEnumerable<SearchEngineRankingModel> results;
        // if cache has data then return else if expired make the call to search engines.
        //TODO: Improve the cache check. If found in cache, check if saved object has all the keywords if not then make API call. 
        if (cache.TryGetValue("collectiveResults", out results)) return new JsonResult(results);
        
        // Using IEnumerable to handle multiple keywords.
        IEnumerable<string> keywordsList = keywords.Split(',');
        IEnumerable<string> searchEngines = _configuration.GetSection("SearchEngines").Get<IEnumerable<string>>();

        IScrapper scrapper = new WebScrapper(keywordsList, searchEngines);
        results =  await scrapper.GetRankingFromSearchEngine();
        cache.Set("collectiveResults", results, options);
        return new JsonResult(results) ;
    }
    
  }
}
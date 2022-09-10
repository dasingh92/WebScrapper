namespace LibWebScrapper.Models
{
    public class SearchEngineRankingModel
    {
        public string? SearchEngine { get; set; }
        public IDictionary<string, int> keyWordRanking { get; set; } = new Dictionary<string, int>();
    }
}

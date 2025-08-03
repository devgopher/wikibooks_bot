using WikiBooksBot.Integration.Wikimedia;

namespace WikiBooksBot.Integration;

/// <summary>
/// Класс для работы с Wikimedia API.
/// </summary>
public class WikimediaService : IWikimediaService
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Конструктор класса WikimediaService.
    /// </summary>
    /// <param name="httpClient">HttpClient для выполнения запросов.</param>
    public WikimediaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Асинхронный метод для поиска по заголовкам в Wikimedia.
    /// </summary>
    /// <param name="query">Запрос для поиска.</param>
    /// <param name="limit">Максимальное количество результатов.</param>
    /// <exception cref="Exception"></exception>
    /// <returns>Список заголовков страниц.</returns>
    public async Task<List<WikimediaPage>> SearchTitlesAsync(string query, int limit)
    {
        var url = $"https://api.wikimedia.org/core/v1/wikibooks/en/search/title?q={query}&limit={limit}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Error getting data from Wikimedia API!");
        var result = await response.Content.ReadFromJsonAsync<WikimediaResponse>();
        return result?.Pages.Where(p => p is { Description: not null, Title: not null }).ToList() ??
               new List<WikimediaPage>();
    }
}
using System.Net;
using Flurl.Http;
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
        var response = await $"https://api.wikimedia.org/core/v1/wikibooks/en/search/title?q={query}&limit={limit}"
            .WithHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/133.0.0.0 Safari/537.36")
            .GetAsync();

        if (response.StatusCode != (int)HttpStatusCode.OK)
            throw new Exception("Error getting data from Wikimedia API!");
        var result =  await response.GetJsonAsync<WikimediaResponse>();
        return result?.Pages.Where(p => p is { Description: not null, Title: not null }).ToList() ??
               new List<WikimediaPage>();
    }
}
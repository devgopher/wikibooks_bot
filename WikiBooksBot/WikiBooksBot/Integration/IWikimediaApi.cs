using WikiBooksBot.Integration.Wikimedia;

namespace WikiBooksBot.Integration;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

/// <summary>
/// Интерфейс для получения данных из Wikimedia API.
/// </summary>
public interface IWikimediaService
{
    /// <summary>
    /// Асинхронный метод для поиска по заголовкам в Wikimedia.
    /// </summary>
    /// <param name="query">Запрос для поиска.</param>
    /// <param name="limit">Максимальное количество результатов.</param>
    /// <returns>Список заголовков страниц.</returns>
    Task<List<WikimediaPage>> SearchTitlesAsync(string query, int limit);
}

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

        if (!response.IsSuccessStatusCode) throw new Exception("Ошибка при получении данных из Wikimedia API.");
        var result = await response.Content.ReadFromJsonAsync<WikimediaResponse>();
        return result?.Pages ?? new List<WikimediaPage>();

    }
}

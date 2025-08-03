using WikiBooksBot.Integration.Wikimedia;

namespace WikiBooksBot.Integration;

using System.Collections.Generic;
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
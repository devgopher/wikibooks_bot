namespace WikiBooksBot.Integration.Wikimedia;

/// <summary>
/// Класс для представления ответа от Wikimedia API.
/// </summary>
public class WikimediaResponse
{
    /// <summary>
    /// Список страниц, полученных в результате запроса.
    /// </summary>
    public List<WikimediaPage> Pages { get; set; }
}
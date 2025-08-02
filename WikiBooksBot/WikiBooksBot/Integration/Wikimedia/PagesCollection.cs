namespace WikiBooksBot.Integration.Wikimedia;

/// <summary>
///     Класс, представляющий коллекцию страниц.
/// </summary>
public class PagesCollection
{
    /// <summary>
    ///     Список страниц.
    /// </summary>
    public List<WikimediaPage> Pages { get; set; } = new();
}
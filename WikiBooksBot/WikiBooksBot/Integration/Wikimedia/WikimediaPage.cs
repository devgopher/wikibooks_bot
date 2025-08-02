namespace WikiBooksBot.Integration.Wikimedia;

/// <summary>
///     Класс, представляющий страницу с информацией о Земле и землетрясениях.
/// </summary>
public class WikimediaPage
{
    /// <summary>
    ///     Уникальный идентификатор страницы.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Ключ страницы, используемый для идентификации.
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    ///     Заголовок страницы.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    ///     Краткое описание или аннотация страницы.
    /// </summary>
    public string? Excerpt { get; set; }

    /// <summary>
    ///     Соответствующий заголовок, если есть.
    /// </summary>
    public string? MatchedTitle { get; set; }

    /// <summary>
    ///     Якорь для навигации на странице.
    /// </summary>
    public string? Anchor { get; set; }

    /// <summary>
    ///     Описание страницы, если доступно.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Миниатюра или изображение, связанное со страницей.
    /// </summary>
    public string? Thumbnail { get; set; }
}
using Botticelli.Framework.Commands;

namespace WikiBooksBot.Commands;

/// <summary>
/// Find a recipe by keywords
/// </summary>
public class FindBookCommand : ICommand
{
    public Guid Id { get; }
}
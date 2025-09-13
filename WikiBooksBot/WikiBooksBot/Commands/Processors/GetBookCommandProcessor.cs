using System.Text;
using Botticelli.Client.Analytics;
using Botticelli.Framework.Commands.Processors;
using Botticelli.Framework.Commands.Validators;
using Botticelli.Shared.ValueObjects;
using FluentValidation;
using WikiBooksBot.Integration;

namespace WikiBooksBot.Commands.Processors;

public class GetBookCommandProcessor : CommandChainProcessor<FindBookCommand>
{
    private readonly IWikimediaService _wikimediaService;

    public GetBookCommandProcessor(ILogger<CommandChainProcessor<FindBookCommand>> logger,
        ICommandValidator<FindBookCommand> commandValidator,
        MetricsProcessor metricsProcessor,
        IValidator<Message> messageValidator, IWikimediaService wikimediaService) : base(logger,
        commandValidator,
        metricsProcessor,
        messageValidator)
    {
        _wikimediaService = wikimediaService;
    }

    public override async Task ProcessAsync(Message message, CancellationToken token)
    {
        if (message.ProcessingArgs == null || !message.ProcessingArgs.Any())
            return;

        var query = message.ProcessingArgs[0];

        var wikiResponse = await _wikimediaService.SearchTitlesAsync(query, 10);
        
        message.ProcessingArgs = [];

        if (!wikiResponse.Any())
        {
            message.Body = $"🚫 No books were found for '{query}'!";

            await SendMessage(message, token);
        }

        foreach (var article in wikiResponse.AsEnumerable().OrderBy(o => Random.Shared.Next()).Take(3))
        {
            var link = $"https://m.wikibooks.org/w/index.php?curid={article.Id}";
            
            var sb = new StringBuilder();
            sb.Append($"\u2714\u2714 {article.Title} \u2714\u2714\n");
            sb.Append("\nDescription: \n");
            sb.AppendJoin('\n', article.Description?.Replace("[", string.Empty).Replace("]", string.Empty)
                .Replace("\"", string.Empty).Split(",").Select(i => $"\u2705 {i};")!);
            sb.Append($"\n{link}\n\n");
            message.Body = sb.ToString();

            await SendMessage(message, token);
        }
    }

    protected override Task InnerProcess(Message message, CancellationToken token)
    {
        return Task.FromResult(Task.CompletedTask);
    }
}
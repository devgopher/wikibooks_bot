using System.Reflection;
using Botticelli.Client.Analytics;
using Botticelli.Controls.Parsers;
using Botticelli.Framework.Commands.Processors;
using Botticelli.Framework.Commands.Validators;
using Botticelli.Framework.SendOptions;
using Botticelli.Shared.API.Client.Requests;
using Botticelli.Shared.ValueObjects;
using FluentValidation;

namespace WikiBooksBot.Commands.Processors;

public class InfoCommandProcessor<TReplyMarkup> : CommandProcessor<InfoCommand> where TReplyMarkup : class
{
    private readonly SendOptionsBuilder<TReplyMarkup>? _options;

    public InfoCommandProcessor(ILogger<InfoCommandProcessor<TReplyMarkup>> logger,
                                ICommandValidator<InfoCommand> commandValidator,
                                ILayoutSupplier<TReplyMarkup> layoutSupplier,
                                ILayoutParser layoutParser,
                                IValidator<Message> messageValidator)
            : base(logger,
                   commandValidator,
                   messageValidator)
    {
        var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        var responseLayout = layoutParser.ParseFromFile(Path.Combine(location, "start_layout.json"));
        var responseMarkup = layoutSupplier.GetMarkup(responseLayout);

        _options = SendOptionsBuilder<TReplyMarkup>.CreateBuilder(responseMarkup);
    }

    public InfoCommandProcessor(ILogger<InfoCommandProcessor<TReplyMarkup>> logger,
                                ICommandValidator<InfoCommand> commandValidator,
                                ILayoutSupplier<TReplyMarkup> layoutSupplier,
                                ILayoutParser layoutParser,
                                IValidator<Message> messageValidator,
                                MetricsProcessor? metricsProcessor)
            : base(logger,
                   commandValidator,
                   messageValidator,
                   metricsProcessor)
    {
        var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        var responseLayout = layoutParser.ParseFromFile(Path.Combine(location, "start_layout.json"));
        var responseMarkup = layoutSupplier.GetMarkup(responseLayout);

        _options = SendOptionsBuilder<TReplyMarkup>.CreateBuilder(responseMarkup);
    }

    protected override Task InnerProcessContact(Message message, CancellationToken token)
    {
        return Task.CompletedTask;
    }

    protected override Task InnerProcessPoll(Message message, CancellationToken token)
    {
        return Task.CompletedTask;
    }

    protected override Task InnerProcessLocation(Message message, CancellationToken token)
    {
        return Task.CompletedTask;
    }

    protected override async Task InnerProcess(Message message, CancellationToken token)
    {
        var greetingMessageRequest = new SendMessageRequest
        {
            Message = new Message
            {
                Uid = Guid.NewGuid().ToString(),
                ChatIds = message.ChatIds,
                Body = "This is a test bot.\nEnjoy!"
            }
        };

        await SendMessage(greetingMessageRequest, _options, token);
    }
}
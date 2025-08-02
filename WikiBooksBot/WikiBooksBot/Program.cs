using Botticelli.Controls.Parsers;
using Botticelli.Framework.Commands.Validators;
using Botticelli.Framework.Extensions;
using Botticelli.Framework.Telegram;
using Botticelli.Framework.Telegram.Extensions;
using NLog.Extensions.Logging;
using Telegram.Bot.Types.ReplyMarkups;
using WikiBooksBot.Commands;
using WikiBooksBot.Commands.Processors;
using WikiBooksBot.Integration;

var builder = WebApplication.CreateBuilder(args);

var bot = builder.Services
    .AddTelegramBot(builder.Configuration)
    .Prepare();

builder.Services
    .AddTelegramLayoutsSupport()
    .AddLogging(cfg => cfg.AddNLog())
    .AddSingleton<ILayoutParser, JsonLayoutParser>()
    .AddSingleton<InfoCommandProcessor<ReplyKeyboardMarkup>>()
    .AddScoped<ICommandValidator<InfoCommand>, PassValidator<InfoCommand>>()
    .AddScoped<ICommandValidator<FindBookCommand>, PassValidator<FindBookCommand>>()
    .AddScoped<IWikimediaService, WikimediaService>();

builder.Services.AddBotCommand<InfoCommand>()
    .AddProcessor<InfoCommandProcessor<ReplyKeyboardMarkup>>()
    .AddValidator<PassValidator<InfoCommand>>();

builder.Services.AddBotChainProcessedCommand<FindBookCommand, PassValidator<FindBookCommand>>()
    .AddNext<FindBookCommandProcessor>()
    .AddNext<GetBookCommandProcessor>();

var app = builder.Build();
app.Services.RegisterBotChainedCommand<FindBookCommand, TelegramBot>()
    .UseTelegramBot();

await app.RunAsync();
using EdgarBot.Application.Interfaces;
using EdgarBot.Application.Models;
using EdgarBot.Application.Services;
using EdgarBot.Infrastructure.Persistence;
using EdgarBot.Infrastructure.Telegram;
using EdgarBot.Presentation.Telegram;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) => { config.AddJsonFile("appsettings.json", false); })
    .ConfigureServices((context, services) =>
    {
        var config = context.Configuration;
        var telegramSection = config.GetSection("Telegram");
        services.Configure<TelegramOptions>(telegramSection);

        services.AddSingleton<ITelegramBotClient>(sp =>
        {
            var options = telegramSection.Get<TelegramOptions>();
            return new TelegramBotClient(options.BotToken);
        });

        services.AddSingleton<IMappingStore, InMemoryMappingStore>();
        services.AddSingleton<IMessageSender, TelegramMessageSender>();
        services.AddSingleton<IForwardingService, ForwardingService>(sp =>
        {
            var sender = sp.GetRequiredService<IMessageSender>();
            var store = sp.GetRequiredService<IMappingStore>();
            var options = telegramSection.Get<TelegramOptions>();
            return new ForwardingService(sender, store, options.AdminChatId);
        });
        services.AddSingleton<IAdminReplyHandler, AdminReplyHandler>();
        services.AddSingleton<UpdateHandler>(sp =>
        {
            var forwarding = sp.GetRequiredService<IForwardingService>();
            var adminReply = sp.GetRequiredService<IAdminReplyHandler>();
            var options = telegramSection.Get<TelegramOptions>();
            return new UpdateHandler(forwarding, adminReply, options.AdminChatId);
        });
        services.AddSingleton<EdgarUpdateHandler>();
    });

var host = builder.Build();

var botClient = host.Services.GetRequiredService<ITelegramBotClient>();
var updateHandler = host.Services.GetRequiredService<EdgarUpdateHandler>();

using var cts = new CancellationTokenSource();

botClient.StartReceiving(
    updateHandler,
    null,
    cts.Token);

Console.WriteLine("Press any key to exit");
Console.ReadKey();
cts.Cancel();

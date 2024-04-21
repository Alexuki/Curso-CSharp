using Lab01.ConsoleClient;
using Lab01.ConsoleClient.Api;

// ¡Reemplaza el host/puerto por el que uses!
var apiClient = new ContactListApiClient("https://localhost:7179/api/contacts/");
var handler = new CommandHandler(apiClient);

Console.WriteLine("Welcome to your contact list!");
await handler.ExecuteAsync("help", null);
while (true)
{
    Console.Write("Command> ");
    var line = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(line))
        continue;

    var firstSpace = line.IndexOf(' ');
    var command = (firstSpace == -1 ? line : line.Substring(0, firstSpace))
        .Trim().ToLower();
    if (command == "quit")
        break;
    var arguments = line.Substring(command.Length).Trim();
    await handler.ExecuteAsync(command, arguments);
}

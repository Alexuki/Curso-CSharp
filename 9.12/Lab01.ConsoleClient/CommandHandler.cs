using Lab01.ConsoleClient.Api;

namespace Lab01.ConsoleClient;

public class CommandHandler
{
    private readonly ContactListApiClient _apiClient;

    public CommandHandler(ContactListApiClient apiClient)
    {
        _apiClient = apiClient;
    }
    public async Task ExecuteAsync(string command, string args)
    {
        Console.WriteLine();
        switch (command)
        {
            case "list":
                await ExecuteListCommandAsync();
                break;
            case "create":
                await ExecuteCreateCommandAsync(args);
                break;
            case "delete":
                await ExecuteDeleteCommandAsync(args);
                break;
            case "show":
                await ExecuteShowCommandAsync(args);
                break;
            case "help":
                ExecuteHelpCommand();
                break;
            default:
                Console.WriteLine("Error. Use LIST, SHOW, CREATE or DELETE!");
                break;
        }
        Console.WriteLine();
    }

    private void ExecuteHelpCommand()
    {
        Console.WriteLine("Use the following commands:");
        Console.WriteLine("  LIST - List all contacts");
        Console.WriteLine("  CREATE [name],[phone] - Creates a contact");
        Console.WriteLine("  SHOW [id] - Show contact details");
        Console.WriteLine("  DELETE [id] - Deletes a contact");
        Console.WriteLine("  HELP - Shows this help");
        Console.WriteLine("  QUIT - Close application");
    }

    private async Task ExecuteShowCommandAsync(string args)
    {
        // Usar la API para obtener un contacto y mostrarlo llamando a Show()
        var contact = await _apiClient.GetByIdAsync(args);
        if (contact != null)
        {
            Show("Contact details:", contact);
        }
        else
            Console.WriteLine("Error: Contact not found");
    }

    private async Task ExecuteDeleteCommandAsync(string args)
    {
        var result = await _apiClient.DeleteAsync(args);
        Console.WriteLine(result ? $"Ok, contact #{args} deleted" : "Error: Contact not found");
    }

    private async Task ExecuteCreateCommandAsync(string args)
    {
        var argsArray = args.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        if (argsArray.Length == 2)
        {
            var contactDto = new ContactDto()
            {
                Name = argsArray[0],
                Phone = argsArray[1]
            };

            // Usar la API para crear un contacto, y mostrarlo llamando a Show() si se creó con éxito
            var contact = await _apiClient.CreateContactAsync(contactDto);
            if (contact != null)
            {
                Show($"Ok, contact #{contact.Id} created:", contact);
            }
            else
            {
                Console.WriteLine("Error: Could not create contact");
            }
        }
        else
        {
            Console.WriteLine("Error: missing name or phone. Usage: CREATE [name],[phone]");
        }
    }

    private async Task ExecuteListCommandAsync()
    {
        // Usar la API para obtener todos los contactos y mostrarlos en forma de listado
        var contacts = await _apiClient.GetAllAsync();
        if (contacts.Any())
        {
            Console.WriteLine($"ID  NAME                          ");
            Console.WriteLine($"=== ==============================");
            foreach (var contact in contacts)
            {
                Console.WriteLine($"{contact.Id,3} {contact.Name,-30}");
            }
        }
        else
        {
            Console.WriteLine("No contacts found.");
        }
    }

    private void Show(string title, ContactDto contact)
    {
        Console.WriteLine(title);
        Console.WriteLine("  Id: " + contact.Id);
        Console.WriteLine("  Name: " + contact.Name);
        Console.WriteLine("  Phone: " + contact.Phone);
    }
}
using System.Net.Http.Json;

namespace Lab01.ConsoleClient.Api;

public class ContactListApiClient
{
    private readonly string _apiBaseUri;
    private readonly HttpClient _httpClient = new HttpClient();

    public ContactListApiClient(string apiBaseUri)
    {
        _apiBaseUri = apiBaseUri;
    }

    public async Task<IEnumerable<ContactDto>> GetAllAsync()
    {
        try
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<ContactDto>>(_apiBaseUri);
            return result;
        }
        catch { return Enumerable.Empty<ContactDto>(); }
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var response = await _httpClient.DeleteAsync(_apiBaseUri + id);
        return response.IsSuccessStatusCode;
    }

    public async Task<ContactDto> GetByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync(_apiBaseUri + id);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ContactDto>();
        }
        return null;
    }

    public async Task<ContactDto> CreateContactAsync(ContactDto contact)
    {
        var response = await _httpClient.PostAsJsonAsync(_apiBaseUri, contact);
        if(response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ContactDto>();
        }
        return null;
    }
}
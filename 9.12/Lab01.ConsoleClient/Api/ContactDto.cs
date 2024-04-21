using System.Text.Json.Serialization;

namespace Lab01.ConsoleClient.Api;

public class ContactDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("phone")]
    public string Phone { get; set; }
}
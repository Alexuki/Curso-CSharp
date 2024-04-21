public class Friend
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }
}

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
}
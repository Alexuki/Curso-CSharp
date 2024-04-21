public class Friend
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }

    // 7) Añadir propiedad compleja
    public Address Address { get; set; }

}

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
}
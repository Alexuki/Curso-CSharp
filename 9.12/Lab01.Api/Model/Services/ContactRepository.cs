using Lab01.Api.Model.Entities;

namespace Lab01.Api.Model.Services;

public class ContactRepository : IContactRepository
{
    private static Dictionary<int, Contact> _repo = new Dictionary<int, Contact>()
    {
        {1, new Contact() {Id = 1, Name = "John Smith", Phone = "998-12-32-12"}},
        {2, new Contact() {Id = 2, Name = "Rupert Lamas", Phone = "436-45-75-73"}},
        {3, new Contact() {Id = 3, Name = "Peter Norton", Phone = "642-64-16-81"}}
    };
    private static int _nextId = 4;

    public void Add(Contact contact)
    {
        _repo.Add(_nextId, contact);
        contact.Id = _nextId++;
    }

    public bool Remove(int id)
    {
        return _repo.Remove(id);
    }

    public Contact Get(int id)
    {
        Contact result;
        _repo.TryGetValue(id, out result);
        return result;
    }

    public IReadOnlyList<Contact> GetAll()
    {
        return _repo.Values.OrderBy(c => c.Id).ToList();
    }
}
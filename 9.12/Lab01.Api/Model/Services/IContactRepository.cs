using Lab01.Api.Model.Entities;

namespace Lab01.Api.Model.Services;

public interface IContactRepository
{
    void Add(Contact contact);
    bool Remove(int id);
    Contact Get(int id);
    IReadOnlyList<Contact> GetAll();
}
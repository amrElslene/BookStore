using BookStore.Models.domins;

namespace BookStore.Repositories.Abstract
{
    public interface IUserService
    {
        bool Add(User model);
        bool Delete(int id);
        User FindById(int id);
        IEnumerable<User> GetAll();
        bool usernameex(string name,bool pass, string password);
        bool Iadmin(string name,string password);

    }
}

using BookStore.Models.domins;

namespace BookStore.Repositories.Abstract
{
    public interface IBookService
    {
        bool Add(Book model);
        bool Addcart(cartbooks model);
        IEnumerable<cartbooks> GetAllcart();
        bool Deletecart(int id);
        void donecart();

        bool Update(Book model);
        bool Delete(int id);
        Book FindById(int id);
        IEnumerable<Book> GetAll();
        int getlastid();
    }
}

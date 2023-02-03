using BookStore.Models.domins;
using BookStore.Repositories.Abstract;

namespace BookStore.Repositories.Implementation
{
    public class BookService : IBookService
    {
        private readonly DatabaseContext context;
        public BookService(DatabaseContext context)
        {
            this.context = context;
        }
        public bool Add(Book model)
        {
            try
            {
                context.Books.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Addcart(cartbooks model)
        {
            try
            {
                context.cartbooks.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.FindById(id);
                if (data == null)
                    return false;
                context.Books.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Deletecart(int id)
        {
            try
            {
                var data = context.cartbooks.Find(id);
                if (data == null)
                    return false;
                context.cartbooks.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void donecart()
        {
            foreach (var item in context.cartbooks)
            {
                context.cartbooks.Remove(item);
            }
            context.SaveChanges() ;
        }

        public Book FindById(int id)
        {
            return context.Books.Find(id);
        }

        public IEnumerable<Book> GetAll()
        {

            return context.Books.ToList();
        }

        public IEnumerable<cartbooks> GetAllcart()
        {
            return context.cartbooks.ToList();
        }

        public int getlastid()
        {
            return (context.Books.Count()+1);
        }

        public bool Update(Book model)
        {
            try
            {
                context.Books.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
      
    }
}

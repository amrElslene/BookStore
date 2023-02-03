using BookStore.Models.domins;
using BookStore.Repositories.Abstract;

namespace BookStore.Repositories.Implementation
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext context;
        public UserService(DatabaseContext context)
        {
            this.context = context;
        }
        public bool Add(User model)
        {
            try
            {
                context.Users.Add(model);
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
                context.Users.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public User FindById(int id)
        {
            return context.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users.ToList();
        }

        public bool Iadmin(string name, string password)
        {
            admin obj = context.Admin.FirstOrDefault(x => x.adUsername == name);
            if (obj!=null)
            {
                if (obj.adPassword == password) return true;
                else return false;
            }
                else return false;
        }

        public bool usernameex(string name,bool pass,string password)
        {
            User obj = context.Users.FirstOrDefault(x => x.Username == name);
            if (obj == null) return false;
            else if (pass)
            {
                if (obj.Password == password) return true;
                else return false;

            }
            else return true;


        }
    }
}

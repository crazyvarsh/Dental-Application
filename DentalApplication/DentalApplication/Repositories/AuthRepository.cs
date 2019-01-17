using DentalApplication.DAL;
using SampleApplication.Models;
using System.Linq;

namespace SampleApplication.Repositories
{
    public class AuthRepository
    {
        public User RegisterUser(User user)
        {
            using (var context = new DentalAppContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
                return user;
            }
        }

        public User FindUser(string username, string password)
        {
            using (var context = new DentalAppContext())
            {
                return (from u in context.Users
                        where u.UserName == username && u.Password == password
                        select u).FirstOrDefault();
            }
        }
    }
}
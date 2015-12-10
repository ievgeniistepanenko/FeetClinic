using System.Data.Entity;
using BE.BE.Identity;

namespace FeetClinic_UserDAL.Concrete
{
    public class UserDbInitializer : DropCreateDatabaseAlways<UserContext>
    {
        protected override void Seed(UserContext db)
        {
            db.Roles.Add(new Role { Id = 1, Name = "admin" });
            db.Roles.Add(new Role { Id = 2, Name = "user" });
            db.Users.Add(new User
            {
                Email = "alice@gmail.com",
                Password = "123456",
                RoleId = 1
            });
            db.Users.Add(new User
            {
                Email = "tom@gmail.com",
                Password = "123456",
                RoleId = 2
            });
            db.SaveChanges();
            base.Seed(db);
        }
    }
}
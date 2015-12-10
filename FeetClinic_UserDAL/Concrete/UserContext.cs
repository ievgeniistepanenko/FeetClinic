using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BE.BE.Identity;

namespace FeetClinic_UserDAL.Concrete
{
    public class UserContext : DbContext
    {
        public UserContext() : base("DBConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            //Database.SetInitializer<UserContext>(new UserDbInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
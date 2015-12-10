using BE.BE;
using BE.BE.Customer;
using BE.BE.Identity;
using BE.BE.Schedule;
using BE.BE.Treatments;
using FeetClinic_UserDAL.Abstract;

namespace FeetClinic_UserDAL.Concrete
{
    public class UserDalFacade : RepositoryContainer<UserContext>
    {
        public UserDalFacade()
        {
            Context = new UserContext();
        }

        private Repository<UserContext, User> _users;
        private Repository<UserContext, Role> _roles;

        public Repository<UserContext, Role> Roles =>
            _roles ?? (_roles = new Repository<UserContext, Role>(Context)); 
        public Repository<UserContext, User> Users => 
            _users ?? (_users = new Repository<UserContext, User>(Context));


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Managers;

namespace BLL
{
    public class BllFacade
    {
        private AddressManager _addressManager;
        public AddressManager AddressManager => _addressManager ?? (_addressManager = new AddressManager());


        private CustomerProfileManager _customerManager;

        public CustomerProfileManager CustomerManager
            => _customerManager ?? (_customerManager = new CustomerProfileManager());
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Managers;
using DomainModel.BE;

namespace BLL
{
    public class BllFacade
    {
        private TreatmentsManager _treatmentManager;
        public TreatmentsManager TreatmentsManager =>
            _treatmentManager ?? (_treatmentManager = new TreatmentsManager());
            

        private TherapistsManager _therapistManager;
        public TherapistsManager TherapistsManager =>
            _therapistManager ?? (_therapistManager = new TherapistsManager());
            


        private AddressManager _addressManager;
        public AddressManager AddressManager =>
            _addressManager ?? (_addressManager = new AddressManager());

        private TreatmentTypesManager _treatmentTypesManager;
        public TreatmentTypesManager TreatmentTypesManager
            => _treatmentTypesManager ?? (_treatmentTypesManager = new TreatmentTypesManager());

        private CustomerProfileManager _customerManager;
        public CustomerProfileManager CustomerManager
            => _customerManager ?? (_customerManager = new CustomerProfileManager());
    }
}

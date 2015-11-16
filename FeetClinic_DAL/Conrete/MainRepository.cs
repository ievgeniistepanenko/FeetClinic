using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeetClinic_DAL.Abstarct;

namespace FeetClinic_DAL.Conrete
{
    public class MainRepository:RepositoryContainer<DbContext>
    {
        public MainRepository()
        {
        }

    }
}

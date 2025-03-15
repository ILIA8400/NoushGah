using Microsoft.AspNetCore.Identity;
using NoushGah.Model.Entities;
using NoushGah.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.DataAccess.IdentityModel
{
    public class SystemUser : IdentityUser
    {
        public UserGenderEnum Gender { get; set; }

        #region Navigations
        public List<Invoice> Invoices { get; set; }
        public Basket Basket { get; set; } 
        #endregion
    }
}

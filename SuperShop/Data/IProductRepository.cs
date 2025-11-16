using Microsoft.AspNetCore.Mvc.Rendering;
using Supershop.Data.Entities;
using SuperShop.Data;
using SuperShop.Data.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Supershop.Data
{
    public interface IProductRepository : IGenericRepository<Product>
    {

        // This interface extends the generic repository for Product entities
        public IQueryable GetAllWithUsers();


        IEnumerable<SelectListItem> GetComboProducts();

    }
}

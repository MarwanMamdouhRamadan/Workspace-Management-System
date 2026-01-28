using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace_Management_System.Entities;

namespace Workspace.Application.Interfaces
{
    public interface IProductRepo : IGenricRepo<TbProduct>
    {
        Task<IEnumerable<TbProduct>> getAllActivatedProducts();
        Task<IEnumerable<TbProduct>> getAllProducts(long statusId);
        Task<TbProduct> getProductById(long id);
    }
}

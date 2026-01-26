using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Workspace.Application.DTOs.request;
using Workspace_Management_System.Entities;

namespace Workspace.Application.Interfaces
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductDto>> getAll();
        Task<ProductDto> getById(long id);
        Task<bool> addProduct(ProductDto product);
        Task<bool> putProduct(long id, ProductDto dto);
        Task<bool> deleteProduct(long id);
        Task<IEnumerable<object>> getProductsByStatus(long statusId);
        Task<bool> changeProductStatus(PutProductStatus dto);
    }
}

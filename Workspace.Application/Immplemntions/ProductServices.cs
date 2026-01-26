using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Application.DTOs.request;
using Workspace.Application.Interfaces;
using Workspace_Management_System.Entities;

namespace Workspace.Application.Immplemntions
{
    public class ProductServices : IProductServices
    {
        IProductRepo _repo;
        IStatusLookupService _lookupServices;

        public ProductServices(IProductRepo repo, IStatusLookupService lookupServices)
        {
            _repo = repo;
            _lookupServices = lookupServices;
        }

        public async Task<bool> addProduct(ProductDto dto)
        {
            var status = _lookupServices.GetStatusId("Product", "Active");
            if (status == 0) return false;
            var product = new TbProduct
            {
                ProductName = dto.ProductName,
                Price = dto.Price,
                Stock = dto.Stock,
                StatusId = status,
            };
            await _repo.add(product);
            return true;
        }

        public async Task<bool> changeProductStatus(PutProductStatus dto)
        {
            var status = _lookupServices.getStatus(dto.statusId, "Product");
            if (status == null) return false;
            var product =await _repo.getById(dto.productId);
            if (product == null) return false;
            product.StatusId = status.id;
            _repo.update(product);
            return true;
        }

        public async Task<bool> deleteProduct(long id)
        {
            var product = await _repo.getById(id);
            if (product == null) return false;
            var statusId = _lookupServices.GetStatusId("Product", "Closed");
            if(statusId == 0) return false;
            product.StatusId = statusId;
            _repo.update(product);
            return true;
        }

        public async Task<IEnumerable<ProductDto>> getAll()
        {
            var products = await _repo.getAllActivatedProducts();
            return products.Select(x => new ProductDto
            {
                ProductName = x.ProductName,
                Price = x.Price,
                Stock = x.Stock,
            }).ToList();
        }

        public async Task<ProductDto> getById(long id)
        {
            var product = await _repo.getById(id);
            if (product == null) return null;
            return new ProductDto
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Stock = product.Stock,
            };
        }

        public async Task<IEnumerable<object>> getProductsByStatus(long statusId)
        {
           var products = await _repo.getAllProducts(statusId);
            var status = _lookupServices.getStatus(statusId, "Product");
            return products.Select(x => new 
            {
                ProductName = x.ProductName,
                Price = x.Price,
                Stock = x.Stock,
                Status= status.StatusName,
            }).ToList();
        }

        public async Task<bool> putProduct(long id, ProductDto dto)
        {
            var product = await _repo.getById(id);
            if(product == null) return false;
            product.ProductName = dto.ProductName;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            _repo.update(product);
            return true;
        }
    }
}

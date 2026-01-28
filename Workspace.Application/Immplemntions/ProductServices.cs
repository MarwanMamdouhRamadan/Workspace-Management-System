using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Application.DTOs.request;
using Workspace.Application.Interfaces;
using Workspace_Management_System.Entities;
using static Workspace.Application.Common.SystemConstants;

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
            var status = _lookupServices.GetStatusId(StatusTypes.Product, ProductStatus.Active);
            if (status == 0)
                throw new KeyNotFoundException("System Error: 'Active' status configuration for products is missing.");

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
            var status = _lookupServices.getStatus(dto.statusId, StatusTypes.Product);
            if (status == null)
                throw new KeyNotFoundException($"The status ID {dto.statusId} is not valid for products.");

            var product = await _repo.getById(dto.productId);
            if (product == null)
                throw new KeyNotFoundException($"Update failed: Product with ID {dto.productId} not found.");

            product.StatusId = status.id;
            _repo.update(product);
            return true;
        }

        public async Task<bool> deleteProduct(long id)
        {
            var product = await _repo.getProductById(id);
            if (product == null)
                throw new KeyNotFoundException($"Delete failed: Product with ID {id} not found.");

            var statusId = _lookupServices.GetStatusId(StatusTypes.Product, ProductStatus.Closed);
            if (statusId == 0)
                throw new KeyNotFoundException("System Error: 'Closed' status configuration is missing.");

            product.StatusId = statusId;
            _repo.update(product);
            return true;
        }

        public async Task<IEnumerable<ProductDto>> getAll()
        {
            var products = await _repo.getAllActivatedProducts();
            if (products == null || !products.Any())
                throw new KeyNotFoundException("No active products were found in the database.");

            return products.Select(x => new ProductDto
            {
                ProductName = x.ProductName,
                Price = x.Price,
                Stock = x.Stock,
            }).ToList();
        }

        public async Task<ProductDto> getById(long id)
        {
            var product = await _repo.getProductById(id);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} was not found.");

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
            if (products == null || !products.Any())
                throw new KeyNotFoundException($"No products found associated with status ID {statusId}.");

            var status = _lookupServices.getStatus(statusId, StatusTypes.Product);
            if (status == null)
                throw new KeyNotFoundException($"The status ID {statusId} is invalid or not registered for products.");

            return products.Select(x => new
            {
                x.ProductName,
                x.Price,
                x.Stock,
                Status = status.StatusName,
            }).ToList();
        }

        public async Task<bool> putProduct(long id, ProductDto dto)
        {
            var product = await _repo.getProductById(id);
            if (product == null)
                throw new KeyNotFoundException($"Update failed: Product with ID {id} does not exist.");

            product.ProductName = dto.ProductName;
            product.Price = dto.Price;
            product.Stock = dto.Stock;

            _repo.update(product);
            return true;
        }
    }
}
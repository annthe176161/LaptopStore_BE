using LaptopStore.Business.DTOs;
using LaptopStore.Business.Services.Base;
using LaptopStore.Business.Services.Contracts;
using LaptopStore.Data.Contracts.Base;
using LaptopStore.Data.Entities;

namespace LaptopStore.Business.Services
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Thêm sản phẩm mới
        public int Add(ProductDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var product = new Product
            {
                Name = entity.Name,
                BrandID = entity.BrandID,
                Price = entity.Price,
                Description = entity.Description,
                StockQuantity = entity.StockQuantity,
                ImageURL = entity.ImageURL,
                CreatedDate = entity.CreatedDate,
                IsDiscounted = entity.IsDiscounted,
                DiscountPercent = entity.DiscountPercent,
                DiscountID = entity.DiscountID ?? null,
                IsDeleted = entity.IsDeleted
            };

            _unitOfWork.GenericRepository<Product>().Add(product);
            return _unitOfWork.SaveChanges();
        }

        // Cập nhật sản phẩm
        public int Update(ProductDTO entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var product = _unitOfWork.GenericRepository<Product>().GetById(entity.ProductID);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            product.Name = entity.Name;
            product.BrandID = entity.BrandID;
            product.Price = entity.Price;
            product.Description = entity.Description;
            product.StockQuantity = entity.StockQuantity;
            product.ImageURL = entity.ImageURL;
            product.CreatedDate = entity.CreatedDate;
            product.IsDiscounted = entity.IsDiscounted;
            product.DiscountPercent = entity.DiscountPercent;
            product.DiscountID = entity.DiscountID ?? null; 
            product.IsDeleted = entity.IsDeleted;

            _unitOfWork.GenericRepository<Product>().Update(product);
            return _unitOfWork.SaveChanges();
        }

        // Xóa sản phẩm (chỉ đánh dấu là đã xóa)
        public int Delete(int id)
        {
            var product = _unitOfWork.GenericRepository<Product>().GetById(id);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            product.IsDeleted = true;  // Đánh dấu sản phẩm là đã xóa
            _unitOfWork.GenericRepository<Product>().Update(product);
            return _unitOfWork.SaveChanges();
        }

        // Lấy tất cả sản phẩm
        public IEnumerable<ProductDTO> GetAll()
        {
            var products = _unitOfWork.GenericRepository<Product>().GetAll();
            return products.Where(p => !p.IsDeleted) // Lọc sản phẩm chưa bị xóa
                           .Select(p => new ProductDTO
                           {
                               ProductID = p.ProductID,
                               Name = p.Name,
                               BrandID = p.BrandID,
                               Price = p.Price,
                               Description = p.Description,
                               StockQuantity = p.StockQuantity,
                               ImageURL = p.ImageURL,
                               CreatedDate = p.CreatedDate,
                               IsDiscounted = p.IsDiscounted,
                               DiscountPercent = p.DiscountPercent,
                               DiscountID = p.DiscountID ?? null,
                               IsDeleted = p.IsDeleted,
                               
                           });
        }

        // Lấy tất cả sản phẩm bất đồng bộ
        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _unitOfWork.GenericRepository<Product>().GetAllAsync();
            return products.Where(p => !p.IsDeleted)  // Lọc sản phẩm chưa bị xóa
                           .Select(p => new ProductDTO
                           {
                               ProductID = p.ProductID,
                               Name = p.Name,
                               BrandID = p.BrandID,
                               Price = p.Price,
                               Description = p.Description,
                               StockQuantity = p.StockQuantity,
                               ImageURL = p.ImageURL,
                               CreatedDate = p.CreatedDate,
                               IsDiscounted = p.IsDiscounted,
                               DiscountPercent = p.DiscountPercent,
                               DiscountID = p.DiscountID ?? null,
                               IsDeleted = p.IsDeleted,
                             
                           });
        }

        // Lấy sản phẩm theo ID
        public ProductDTO GetById(int id)
        {
            var product = _unitOfWork.GenericRepository<Product>().GetById(id);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            return new ProductDTO
            {
                ProductID = product.ProductID,
                Name = product.Name,
                BrandID = product.BrandID,
                Price = product.Price,
                Description = product.Description,
                StockQuantity = product.StockQuantity,
                ImageURL = product.ImageURL,
                CreatedDate = product.CreatedDate,
                IsDiscounted = product.IsDiscounted,
                DiscountPercent = product.DiscountPercent,
                DiscountID = product.DiscountID ?? null,
                IsDeleted = product.IsDeleted,
              
            };
        }

        // Lấy sản phẩm theo thương hiệu (brandId)
        public async Task<IEnumerable<ProductDTO>> GetProductsByBrandAsync(int brandId)
        {
            var products = await _unitOfWork.GenericRepository<Product>().GetAllAsync();
            return products.Where(p => p.BrandID == brandId && !p.IsDeleted)  // Lọc theo BrandID và trạng thái không bị xóa
                           .Select(p => new ProductDTO
                           {
                               ProductID = p.ProductID,
                               Name = p.Name,
                               BrandID = p.BrandID,
                               Price = p.Price,
                               Description = p.Description,
                               StockQuantity = p.StockQuantity,
                               ImageURL = p.ImageURL,
                               CreatedDate = p.CreatedDate,
                               IsDiscounted = p.IsDiscounted,
                               DiscountPercent = p.DiscountPercent,
                               DiscountID = p.DiscountID ?? null,
                               IsDeleted = p.IsDeleted,
                               
                           });
        }

        // Lấy sản phẩm nổi bật
        public async Task<IEnumerable<ProductDTO>> GetFeaturedProductsAsync()
        {
            var products = await _unitOfWork.GenericRepository<Product>().GetAllAsync();
            return products.Where(p => p.IsDiscounted && !p.IsDeleted)  // Lọc sản phẩm giảm giá và chưa bị xóa
                           .Select(p => new ProductDTO
                           {
                               ProductID = p.ProductID,
                               Name = p.Name,
                               BrandID = p.BrandID,
                               Price = p.Price,
                               Description = p.Description,
                               StockQuantity = p.StockQuantity,
                               ImageURL = p.ImageURL,
                               CreatedDate = p.CreatedDate,
                               IsDiscounted = p.IsDiscounted,
                               DiscountPercent = p.DiscountPercent,
                               DiscountID = p.DiscountID ?? null,
                               IsDeleted = p.IsDeleted,
                               
                           });
        }
    }
}

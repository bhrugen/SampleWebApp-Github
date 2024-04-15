using WebApplication2.Data;

namespace WebApplication2.Models;

/// <summary>
/// Represents a repository for managing products.
/// </summary>
public class ProductRepository
{
	private List<Product> products = new List<Product>
		{
			new Product { Id = 1, Name = "Product 1", Price = 10.99m, Category = "Category 1", Color = "Red" },
			new Product { Id = 2, Name = "Product 2", Price = 19.99m, Category = "Category 2", Color = "Blue" },
			new Product { Id = 3, Name = "Product 3", Price = 5.99m, Category = "Category 1", Color = "Green" },
			new Product { Id = 4, Name = "Product 4", Price = 7.99m, Category = "Category 2", Color = "Yellow" },
			new Product { Id = 5, Name = "Product 5", Price = 14.99m, Category = "Category 1", Color = "Black" },
			new Product { Id = 6, Name = "Product 6", Price = 9.99m, Category = "Category 2", Color = "White" },
			new Product { Id = 7, Name = "Product 7", Price = 12.99m, Category = "Category 1", Color = "Purple" },
			new Product { Id = 8, Name = "Product 8", Price = 6.99m, Category = "Category 2", Color = "Orange" },
			new Product { Id = 9, Name = "Product 9", Price = 8.99m, Category = "Category 1", Color = "Pink" },
			new Product { Id = 10, Name = "Product 10", Price = 11.99m, Category = "Category 2", Color = "Brown" }
		};

	private readonly ApplicationDbContext _context;

	/// <summary>
	/// Initializes a new instance of the <see cref="ProductRepository"/> class.
	/// </summary>
	/// <param name="context">The application database context.</param>
	public ProductRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	/// <summary>
	/// Gets the products from the database ordered by price or category.
	/// </summary>
	/// <param name="orderBy">The order by parameter. Valid values are "price" or "category".</param>
	/// <param name="pageNumber">The page number for pagination. Default value is 1.</param>
	/// <param name="pageSize">The page size for pagination. Default value is 10.</param>
	/// <returns>The list of products.</returns>
	public List<Product> GetProducts(string orderBy, int pageNumber = 1, int pageSize = 10)
	{
		if (orderBy == "price")
		{
			return _context.Product.OrderBy(p => p.Price).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
		}
		else if (orderBy == "category")
		{
			return _context.Product.OrderBy(p => p.Category).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
		}
		else
		{
			return _context.Product.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
		}
	}

	/// <summary>
	/// Gets a product by its ID.
	/// </summary>
	/// <param name="id">The ID of the product.</param>
	/// <returns>The product.</returns>
	public Product GetProductById(int id)
	{
		return _context.Product.FirstOrDefault(p => p.Id == id);
	}

	/// <summary>
	/// Gets the products by category or color.
	/// </summary>
	/// <param name="category">The category parameter.</param>
	/// <param name="color">The color parameter.</param>
	/// <returns>The list of products.</returns>
	public List<Product> GetProductsByCategoryOrColor(string category, string color)
	{
		return _context.Product.Where(p => p.Category == category || p.Color == color).ToList();
	}

	/// <summary>
	/// Gets the products by price range.
	/// </summary>
	/// <param name="minPrice">The minimum price.</param>
	/// <param name="maxPrice">The maximum price.</param>
	/// <returns>The list of products.</returns>
	public List<Product> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
	{
		return _context.Product.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
	}

	/// <summary>
	/// Adds a product to the database.
	/// </summary>
	/// <param name="product">The product to add.</param>
	public void AddProduct(Product product)
	{
		_context.Product.Add(product);
		_context.SaveChanges();
	}

	/// <summary>
	/// Updates a product in the database.
	/// </summary>
	/// <param name="product">The product to update.</param>
	public void UpdateProduct(Product product)
	{
		_context.Product.Update(product);
		_context.SaveChanges();
	}

	/// <summary>
	/// Deletes a product from the database.
	/// </summary>
	/// <param name="product">The product to delete.</param>
	public void DeleteProduct(Product product)
	{
		_context.Product.Remove(product);
		_context.SaveChanges();
	}
}

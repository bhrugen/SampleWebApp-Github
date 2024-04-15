using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
	public class ProductsController : Controller
	{
		private readonly ApplicationDbContext _context;

		//add private readonly ProductRepository _repository;
		private readonly ProductRepository _repository;

		//add ProductRepository repository to the constructor
		public ProductsController(ApplicationDbContext context, ProductRepository repository)
		{
			_context = context;
			_repository = repository;
		}

		//Add GET controller: Products?orderBy=price&pageNumber=1&pageSize=10 which returns a view with a list of products ordered by price
		[HttpGet]
		public IActionResult Index(string orderBy = "price", int pageNumber = 1, int pageSize = 10)
		{
			return View(_repository.GetProducts(orderBy, pageNumber, pageSize));
		}

		

		// GET: Products
		//public async Task<IActionResult> Index()
		//{
		//	//get the products from the repository
		//	return View(_repository.GetProducts("price"));
		//	//return View(await _context.Product.ToListAsync());

		//}

		// GET: Products/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			//get the product from the repository
			var product = _repository.GetProducts("price").FirstOrDefault(p => p.Id == id);
			//var product = await _context.Product
			//	.FirstOrDefaultAsync(m => m.Id == id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// GET: Products/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Products/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Price,Category,Color")] Product product)
		{
			if (ModelState.IsValid)
			{
				_context.Add(product);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(product);
		}

		// GET: Products/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _context.Product.FindAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}

		// POST: Products/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Category,Color")] Product product)
		{
			if (id != product.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(product);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ProductExists(product.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(product);
		}

		// GET: Products/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _context.Product
				.FirstOrDefaultAsync(m => m.Id == id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// POST: Products/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var product = await _context.Product.FindAsync(id);
			if (product != null)
			{
				_context.Product.Remove(product);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ProductExists(int id)
		{
			return _context.Product.Any(e => e.Id == id);
		}

		//create an endpoint GetProducts to retrieve all products
		[HttpGet]
		public IActionResult GetProducts()
		{
			var products = _context.Product.ToListAsync().GetAwaiter().GetResult();
			return Json(new { data =  products });
		}

		//  create endpoint in products controller to delete product by id
		//  endpoint not invoked Failed to load resource: the server responded with a status of 500 () Products/Delete/2:1
		[HttpDelete("Products/DeleteProduct/{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var product = await _context.Product.FindAsync(id);
			if (product == null)
			{
				return NotFound();
			}

			_context.Product.Remove(product);
			await _context.SaveChangesAsync();

			return NoContent();
		}
		//select and wrtie this endpoint is never invoked

		//create an endpoint GetProductsByCategoryOrColor to retrieve products by category or color. The endpoint should accept two parameters: category and color. Specify the route for this endpoint as Products/GetProductsByCategoryOrColor
		[HttpGet("Products/GetProductsByCategoryOrColor")]
		public IActionResult GetProductsByCategoryOrColor(string category, string color)
		{
			var products = _context.Product.Where(p => p.Category == category || p.Color == color).ToList();
			return Json(new { data = products });
		}

		//create an endpoint GetProductsByPriceRange to retrieve products by price range. The endpoint should accept two parameters: minPrice and maxPrice. Specify the route for this endpoint as Products/GetProductsByPriceRange
		[HttpGet("Products/GetProductsByPriceRange")]
		public IActionResult GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
		{
			var products = _context.Product.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
			return Json(new { data = products });

		}

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Service;

namespace WebApplication2.Controllers
{
    public class ProductsController : Controller
    {
        //use   in the controller rather than applicationdbcontext
        private readonly ProductRepository _productRepository;

        public ProductsController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productRepository.GetProducts());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetProduct(id.Value);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Category,Color")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.CreateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetProduct(id.GetValueOrDefault());
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        /// <summary>
        /// Edits a product with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the product to edit.</param>
        /// <param name="product">The updated product object.</param>
        /// <returns>The action result.</returns>
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
                    await _productRepository.UpdateProduct(product);
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

        /// <summary>
        /// Displays the delete confirmation page for a product with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>The action result.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetProduct(id.GetValueOrDefault());
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
      

        private bool ProductExists(int id)
        {
            return _productRepository.GetProduct(id) != null;
        }

        //create an endpoint GetProducts to retrieve all products
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetProducts().Result;
            return Json(new { data = products });
        }

        //  create endpoint in products controller to delete product by id
        [HttpDelete("Products/DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productRepository.DeleteProduct(id);
            return NoContent();
        }

        //create an endpoint GetProductsByCategoryOrColor to retrieve products by category or color. The endpoint should accept two parameters: category and color. Specify the route for this endpoint as Products/GetProductsByCategoryOrColor
        [HttpGet("Products/GetProductsByCategoryOrColor")]
        public IActionResult GetProductsByCategoryOrColor(string category, string color)
        {
            var products = _productRepository.GetProducts().Result;
            if (category != null)
            {
                products = products.Where(p => p.Category == category);
            }
            if (color != null)
            {
                products = products.Where(p => p.Color == color);
            }
            return Json(new { data = products });
        }
    
        

    }
}

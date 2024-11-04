using Microsoft.AspNetCore.Mvc;
using ProductInventoryApp.Models;


namespace ProductInventoryApp.Controllers
{
    
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View(_productService.GetProducts());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.Create(product);
                return RedirectToAction("Index");
            }

            // Lägg till loggning här:
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(product);
        }

        public IActionResult Details(string id)
        {
            var product = _productService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Edit(string id)
        {
            var product = _productService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(string id, Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.Update(id, product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Delete(string id)
        {
            var product = _productService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(string id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
    

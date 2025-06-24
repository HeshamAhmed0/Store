using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services_Absractions;

namespace Presentation
{
    [Controller]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) :ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var product = await serviceManager.ProductServices.GetAllProductAsync();
            if (product == null) return BadRequest();
            else return Ok(product);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var Product =await serviceManager.ProductServices.GetProductAsync(id);
            if (Product == null) return BadRequest();
            return Ok(Product);
        }

    }
}

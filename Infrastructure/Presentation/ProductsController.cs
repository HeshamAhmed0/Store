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
        public async Task<IActionResult> GetAllProducts(int? BrandId, int? TypeID, string? Sort, int PageIndex = 1,int PageSize = 5)
        {
            var product = await serviceManager.ProductServices.GetAllProductAsync( BrandId , TypeID,Sort,PageIndex,PageSize);
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
        [HttpGet("GetAllBrands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var Brands =await serviceManager.ProductServices.GetAllBrandsAsync();
            if (Brands == null) return BadRequest();
            return Ok(Brands);
        }
        [HttpGet("GetAllTypes")]
        public async Task<IActionResult> GetAllTypes()
        {
            var Types =await serviceManager.ProductServices.GetAllTypesAsync();
            if (Types == null) return BadRequest();
            return Ok(Types);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services_Absractions;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) :ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts(ProductSpecificationParameters SpecParams)
        {
            var product = await serviceManager.ProductServices.GetAllProductAsync( SpecParams);
            if (product == null) return BadRequest();
            else return Ok(product);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var Product =await serviceManager.ProductServices.GetProductAsync(id);
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

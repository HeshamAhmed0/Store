using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services_Absractions;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServiceManager serviceManager) :ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBasketAsync(string id)
        {
           var result= serviceManager.BasketService.GetBasketAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBasketAsync(CustomerBasketDto customerBasketDto)
        {
          var result =await  serviceManager.BasketService.UpdateBasketAsync(customerBasketDto);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasketAsync(string id )
        {
            var result = await serviceManager.BasketService.DeleteBasketAsync(id);
            return Ok(result);
        }
    }
}

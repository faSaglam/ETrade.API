using AutoMapper;
using ETrade.API.Data;
using ETrade.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace ETrade.API.Controllers
{
    //[Route("api/categories/{categoryId}/products")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;
        


        public ProductController(IAppRepository appRepository, IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }
        [HttpGet("{id}",Name="GetProduct")]
        public ActionResult GetProduct(int id)
        {
            var product = _appRepository.GetProductById(id);
            return Ok(product);
        }
        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody]Product product)
        {
            _appRepository.Add(product);
            _appRepository.SaveAll();
            return Ok(product);
        }
        [HttpDelete("{id}")]
        
        public ActionResult Delete(int id)
        {
            var product = _appRepository.GetProductById(id);
                if (product == null || product.Id != id) 
            { return BadRequest("Could not found"); }
                
            //var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //if (currentUserId != product.SellerId)
            //{
            //    return Unauthorized();
            //}
            _appRepository.Delete(product);
            _appRepository.SaveAll();
            
            
            return Ok();

        }
        [HttpPut("{id:int}")]


 
        public async Task<ActionResult<Product>> Update(int id, [FromBody]Product product)
        {
            try
            {
                
                if (id != product.Id)
                {
                    return BadRequest();
                }
                var productToUpdate = await _appRepository.GetProductByIdAsync(id);
                if (productToUpdate == null)
                {
                    return NotFound();

                }
                else
                {
                    productToUpdate.Id = product.Id;
                    productToUpdate.UnitPrice = product.UnitPrice;
                    productToUpdate.CategoryId = product.CategoryId;
                    productToUpdate.Stock = product.Stock;
                    productToUpdate.Defination = product.Defination;
                    productToUpdate.PhotoUrl = product.PhotoUrl;
                    productToUpdate.ProductName = product.ProductName;
                    _appRepository.SaveAll();
                    return NoContent();
                }     
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpPatch]
        public async Task<ActionResult<Product>> Patch([FromBody]Product product)
        {

            try
            {

                var productToUpdate = await _appRepository.GetProductByIdAsync(product.Id);

                var newStock = product.Stock - productToUpdate.Stock;
                var postiveStock = newStock > 0 ? newStock : -newStock;
                
                if(productToUpdate == null)
                {
                    return NotFound();
                }
                else
                {
                   
                    productToUpdate.Stock = postiveStock;
                    _appRepository.SaveAll();
                    return NoContent();

                }
            }
            catch(Exception)
            {
                return StatusCode(500);
            }


        }
        [HttpGet]
        [Route("all")]
        public ActionResult GetAllProducts()
        {
            var products = _appRepository.GetAllProducts();
            return Ok(products);
        }

    }
}

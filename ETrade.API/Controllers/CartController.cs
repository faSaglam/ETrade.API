using AutoMapper;
using ETrade.API.Data;
using ETrade.API.Dtos;
using ETrade.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETrade.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;
        public CartController(IAppRepository appRepository,IMapper mapper)
        {
            _appRepository = appRepository; 
            _mapper = mapper;
        }
        [HttpPost]
       
        public ActionResult Add([FromBody] Cart cart)
        {
            _appRepository.Add(cart);
            _appRepository.SaveAll();
            return Ok(cart);
        }
        [HttpGet]
        public ActionResult GetCart()
        {
            var cart = _appRepository.GetCart();
            return Ok(cart);
        }
        [HttpPut]
        public async Task<ActionResult<Cart>> Update(int id, [FromBody] Cart cart)
        {
            try
            {
                
               
                var cartToUpdate = await _appRepository.GetCartItemAsync(id);
                if (cartToUpdate == null)
                {
                    return NotFound();

                }
                else
                {
                    cartToUpdate.Id = cart.Id;
                    cartToUpdate.quantity = cart.quantity;
                    cartToUpdate.isDelivered = cart.isDelivered;
                    cartToUpdate.isOnWay = cart.isOnWay;
                    cartToUpdate.productName = cart.productName;
                    cartToUpdate.price = cart.price;
                    cartToUpdate.photoUrl = cart.photoUrl; 
                    _appRepository.SaveAll();


                    return NoContent();
                }




            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpDelete]
        [Route("empty")]
        public ActionResult Delete(int id)
        {
            var cart = _appRepository.GetCartItem(id);
            if (cart == null || cart.Id != id)
            { return BadRequest("Could not found"); }
            //var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //if (currentUserId != product.SellerId)
            //{
            //    return Unauthorized();
            //}//postman token bearer ile test edilecek
            _appRepository.Delete(cart);
            _appRepository.SaveAll();


            return Ok();

        }
        [HttpGet]
        [Route("customer")]
        public ActionResult GetCartByCustomerId(int customerId)
        {
            var cart = _appRepository.GetCartByCustomerId(customerId);
            //var cartToReturn = _mapper.Map<CustomerCartDto>(cart);
            return Ok(cart);
        }

    }
}

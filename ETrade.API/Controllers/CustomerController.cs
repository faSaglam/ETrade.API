using AutoMapper;
using ETrade.API.Data;
using ETrade.API.Dtos;
using ETrade.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ETrade.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository _customerRepository;
       
        private IConfiguration _config;
        private IAppRepository _appRepository;
        private IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IConfiguration config, IAppRepository appRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _config = config;
            _appRepository = appRepository;
            _mapper = mapper;
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] CustomerToRegisterDto customerToRegisterDto)
        {
     
            if(await _customerRepository.CustomerExist(customerToRegisterDto.UserName,customerToRegisterDto.Email))
            {
                ModelState.AddModelError("Information", "Username or email already exist");

            }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var customerToCreate = new Customer
            {
                UserName = customerToRegisterDto.UserName,
                Name = customerToRegisterDto.Name,
                Email = customerToRegisterDto.Email,
                Address = customerToRegisterDto.Address,
                Payment = customerToRegisterDto.Payment
            };
            var createdCustomer = await _customerRepository.Register(customerToCreate,customerToRegisterDto.Password);
            return StatusCode(201);
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] CustomerToLoginDto customerToLoginDto)
        {
         
            var customer = await _customerRepository.Login(customerToLoginDto.UserName, customerToLoginDto.Password);
            if(customer == null)
            {
                return Unauthorized();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                    new Claim(ClaimTypes.Name, customer.UserName)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(tokenString);
        }
        [HttpGet]
        [Route("cart")]
        public ActionResult GetCart(int customerId)
        {
            var cart = _appRepository.GetCustomerById(customerId);
            var  cartToReturn = _mapper.Map<CustomerCartDto>(cart);
            return Ok(cartToReturn);
        }
    }
}

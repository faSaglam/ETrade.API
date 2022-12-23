using AutoMapper;
using ETrade.API.Data;
using ETrade.API.Dtos;
using ETrade.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ETrade.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private ISellerRepository _sellerRepository;
        private IMapper _mapper;
        private IConfiguration _config;
        private IAppRepository _appRepository;
        public SellerController(ISellerRepository sellerRepository, IMapper mapper, IConfiguration config, IAppRepository appRepository)
        {
            _sellerRepository = sellerRepository;
            _mapper = mapper;
            _config = config;
            _appRepository = appRepository;
        }
        [HttpGet]
        [Route("getseller")]
        public ActionResult GetSeller(int id)
        {
            var seller = _sellerRepository.GetSellerById(id);
            return Ok(seller);
        }
        [HttpGet]
        [Route("stock")]
        public ActionResult GetProductBySellerId(int id)

        {
            var products = _appRepository.GetProductsBySellerId(id);
            return Ok(products);

        }
        [HttpPost]
        [Route("register")]

        public async Task<ActionResult> Register([FromBody] SellerForRegisterDto sellerForRegisterDto)
        {
            if (await _sellerRepository.SellerExist(sellerForRegisterDto.UserName, sellerForRegisterDto.Email))
            {
                ModelState.AddModelError("Information", "Username or email already exist");
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var sellerToCreate = new Seller
            {
                UserName = sellerForRegisterDto.UserName,
                Name = sellerForRegisterDto.Name,
                Email = sellerForRegisterDto.Email,
                Adress = sellerForRegisterDto.Address,
                Payment = sellerForRegisterDto.Payment,


            };
            var createdSeller = await _sellerRepository.Register(sellerToCreate, sellerForRegisterDto.Password);
            return StatusCode(201);
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] SellerForLoginDto sellerForLoginDto)
        {
            var seller = await _sellerRepository.Login(sellerForLoginDto.UserName, sellerForLoginDto.Password);
            if (seller == null)
            {
                return Unauthorized();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,seller.Id.ToString()),
                    new Claim(ClaimTypes.Name, seller.UserName),


                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(tokenString);
        }

    }
}

using AutoMapper;
using ETrade.API.Data;
using ETrade.API.Dtos;
using ETrade.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ETrade.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
         private IAppRepository _appRepository;
        private IMapper _mapper;


        public CategoryController(IAppRepository appRepository,IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }
        [HttpGet]
       
        public ActionResult GetCategories()
        {
            var categories = _appRepository.GetCategories();
            var categoriesToReturn = _mapper.Map<List<CategoryForListDto>>(categories);
            return Ok(categoriesToReturn);
        }
        //product detail burdan çekilebilir.
        [HttpGet]
        [Route("detail")]
        public ActionResult GetCategoryById(int id)
        {
            var category = _appRepository.GetCategoryById(id);
            var categoriesToReturn = _mapper.Map<CategoryForListDto>(category);
            return Ok(category);
        }
        [HttpGet]
        [Route("raw")]
        public ActionResult GetRawCategories()
        {
            var category = _appRepository.GetCategories();
            return Ok(category);
        }
        [HttpGet]
        [Route("products")]
        public ActionResult GetProductsByCategoryId(int categoryId)
        {
            var products = _appRepository.GetProductsByCategoryId(categoryId);
            return Ok(products);
        }
    }
}

using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E2PProductBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;
        IWebHostEnvironment _webHostEnvironment;
        
        public ProductsController(IProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            //var result = _productService.GetAll();
            var result = _productService.GetProducts();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromForm]Product request)
        {

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "images");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fullFileName = Path.Combine(path,request.ProductImageFile.FileName);
            using (var fs = new FileStream(fullFileName, FileMode.Create))
            {
                await request.ProductImageFile.CopyToAsync(fs);
            }

            Product product = new Product
            {
                ProductName = request.ProductName,
                ProductVendor = request.ProductVendor,
                ProductDescription = request.ProductDescription,
                ProductImage = request.ProductImageFile.FileName,
                CategoryId = request.CategoryId,
            };
            ValidationTool.Validate(new ProductValidator(), product);
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm]Product request)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            if (request.ProductImageFile.Length < 0)
            {
                return BadRequest("Fotoğraf ekleyiniz!");
            }
            else
            {
                var fullFileName = Path.Combine(path, request.ProductImageFile.FileName);
                using (var fs = new FileStream(fullFileName, FileMode.Create))
                {
                    await request.ProductImageFile.CopyToAsync(fs);
                }
                ValidationTool.Validate(new ProductValidator(), request);
                Product product = _productService.GetProduct(request.Id);
                Console.WriteLine(product);
                product.ProductName = request.ProductName;
                product.ProductDescription = request.ProductDescription;
                product.CategoryId = request.CategoryId;
                product.ProductVendor  = request.ProductVendor;
                product.ProductImage = request.ProductImageFile.FileName;
                var result = _productService.Update(product);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
                /*
                var result = _productService.UpdateProduct(request);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);*/
            }
        }

        //[HttpGet]
        //[Route("/productdetail/{productId}")]
        //public IActionResult GetProductDetail(int productId)
        //{
        //    var result = _productService.GetProductDetailDto(productId);
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }

        //    return BadRequest(result);
        //}

        [HttpDelete]
        public IActionResult Delete(int productId)
        {
            var result = _productService.DeleteProduct(productId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

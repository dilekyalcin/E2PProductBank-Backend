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
            //Product product = new Product
            //{
            //    ProductName = request.ProductName,
            //    ProductVendor = request.ProductVendor,
            //    ProductDescription = request.ProductDescription,
            //    ProductImage = await SaveImage(request.ProductImageFile),
            //    ProductImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, request.ProductImage),
            //    CategoryId = request.CategoryId,
            //};


            var path = Path.Combine(@"C:\Users\Monster\Desktop\C#\E2PProductBank\Images", request.ProductImageFile.FileName);
            if ((!Directory.Exists(path)))
            {
                Directory.CreateDirectory(path);
            }

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                await request.ProductImageFile.CopyToAsync(fs);
                fs.Close();
            }

            Product product = new Product
            {
                ProductName = request.ProductName,
                ProductVendor = request.ProductVendor,
                ProductDescription = request.ProductDescription,
                ProductImage = request.ProductImage,
                ProductImageSrc = request.ProductImageSrc,
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
        public IActionResult Update(Product product)
        {
            var result = _productService.Update(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("productdetail")]
        public IActionResult GetProductDetail(int productId)
        {
            var result = _productService.GetProductDetailDto(productId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

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

        private string GetFilePath(string productImage)
        {
            return this._webHostEnvironment.WebRootPath + "\\Uploads\\" + productImage;
        }

        private string GetImageByProdut(string productImage)
        {
            string ImageUrl = "";
            string HostUrl = "https://localhost:7182/";
            string Filepath = GetFilePath(productImage);
            string Imagepath = Filepath;
            if (!System.IO.File.Exists(Imagepath))
            {
                ImageUrl = HostUrl + "/uploads/noimage.png";
            }
            else
            {
                ImageUrl = HostUrl + "/uploads/" + productImage;
            }
            return ImageUrl;
        }

    }
}

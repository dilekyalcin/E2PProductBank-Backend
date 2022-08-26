using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E2PProductBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        ILikeService _likeService;  

        public LikesController(ILikeService likeService)
        {
                _likeService = likeService;
        }

        [HttpGet]
        public IActionResult GetLikes()
        {
            var result = _likeService.GetLikes();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("product/{productId}")]
        public IActionResult GetLike(int productId)
        {
            var result = _likeService.GetLikesProduct(productId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        public IActionResult LikeProduct(Like like)
        {
            var result = _likeService.LikeProduct(like);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        public IActionResult UnlikeProduct(Like like)
        {
            var result = _likeService.UnlikeProduct(like);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

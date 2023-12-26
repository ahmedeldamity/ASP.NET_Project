using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Apis.Errors;
using Talabat.Repository.Data;

namespace Talabat.Apis.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreDbContext _dbContext;

        public BuggyController(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest() // -- Not Found
        {
            var product = _dbContext.Products.Find(100);
            if(product == null)
                return NotFound(new ApiResponse(404));
            return Ok(product);
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError() // server error = exception [null reference exception]
        {
            var product = _dbContext.Products.Find(100);
            var productToReturn = product.ToString();
            return Ok(productToReturn);
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest() // bad request -> Client\Front-end Send Some Thing Wrong
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id) // bad request -> validation error, because id is int and i send string 
        {
            return Ok();
        }

        [HttpGet("unouthorize")]
        public ActionResult GetAnouthorizeError(int id) // bad request -> validation error, because id is int and i send string 
        {
            return Unauthorized(new ApiResponse(401));
        }

        // Error Not Fount End Point




        // ** Errors is:
        // --- Not Found Some Thing In Data Base So We Return NotFound()
        // --- Bad Request When FrontEnd/Client Send Some Thing Wrong So We Return BadRequest()
        // --- Unauthorized When Client Want To Enter Some Where Not Allowed For You So We Return Unauthorized()
        // .... This three errors we can handling with object from class we made (ApiResponse) and this class must have two parameters
        // .... 1.Satus Code 2.Message
        // --- Validation Error: Is Type Of Bad Request Errors 
        // .... we made it in this way: 
        // .... In this error we need return to Frontend object from three parameters
        // .... 1.Satus Code 2.Message 3.Errors
        // .... 1 and 2 : inherit it from class ApiResponse
        // .... 3       : we bring this errors from program class
        // --- Server Error 
        // .... We Create Middleware (ExceptionMiddleware) Then Use It In Program Class
        // --- Not Fount End Point
        // .... We Need When This Error Happen: We Send Object From ApiResponse(404)
        // .... To Make It: We Need Some Steps
        // .... 1. Create New Controller (ErrorController) And Make End Point In It 
        // .... 2. Write (app.UseStatusCodePagesWithReExecute("/Errors/{0}");) in program class So When Not Found End Point Error Happen Will Redirect To This End Point
    }
}

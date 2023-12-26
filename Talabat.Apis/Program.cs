using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Apis.Errors;
using Talabat.Apis.Extensions;
using Talabat.Apis.Helpers;
using Talabat.Apis.Middlewares;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.Apis
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region Update Database Problems And Solution
            // To Update Database You Should Do Two Things 
            // 1. Create Object From DbContext
            //--- StoreDbContext storeDbContext = new StoreDbContext();
            // 2. Migrate It
            //--- await storeDbContext.Database.MigrateAsync();
            // But To Create Instanse From DbContext You Should Have Non Parameterized Constructor In This Class
            // And We Don't Have Because We Work Using Dependancy Injection So That Solution Not Working!!
            // And We Try Another Solution Like Ask Clr To Create This Instance Also This Solution Not Working!!
            // Because To Ask Clr You Need Constractor And If We Use Normal Program Constractor Not Working
            // Because Normal Constractor Work If You Need Object From Class 
            // And Function Main Is Static, So We Need To Ask Clr In Static Consractor
            // And If We Used Program Static Constractor (Static Constractor Work Just One Time: Before The First Using Of Class)
            // So Static Constractor Work Before Main
            // And We Configure DbContext Services In Main Function 
            // So The Only Solution Is: Ask Clr Explicitly After Configure DbContext Services
            #endregion

            var builder = WebApplication.CreateBuilder(args);

            #region Add Services

            // Register Api Controller
            builder.Services.AddControllers();

            // Register Required Services For Swagger In Extension Method
            builder.Services.AddSwaggerServices();

            // Store DbContext
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // This Fuction Has All Application Services
            builder.Services.AddApplicationServices();

            #region Validation Error - Bad Request
            // -- Validation Error (Bad Request) 
            // --- First: We need to bring options which have InvalidModelState
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                // --- then we need all data (actionContext) of action has validation error
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    // --- then we bring ModelState: Dictionary key/value pair for each parameter, and value has property Errors Array have all errors
                    // --- and we use where to bring dictionary key/value pair which is value has errors 
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                    // --- then we use SelectMany to make one array of all error  
                    .SelectMany(P => P.Value.Errors)
                    // --- then we use Select to bring from errors just ErrorMessages
                    .Select(E => E.ErrorMessage)
                    .ToArray();
                    // --- then we insert this errors to the class we made
                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    // then return it :)
                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });
            #endregion

            #endregion

            var app = builder.Build();

            #region Update Database
            // We Said To Update Database You Should Do Two Things (1. Create Instance From DbContext 2. Migrate It)
            
            // To Ask Clr To Create Instance Explicitly From Any Class
            //    1 ->  Create Scope (Life Time Per Request)
            using var scope = app.Services.CreateScope();
            //    2 ->  Bring Service Provider Of This Scope
            var services = scope.ServiceProvider;

            // --> Bring Object Of StoreDbContext For Update Database
            var _storeDbContext = services.GetRequiredService<StoreDbContext>();
            // --> Bring Object Of ILoggerFactory For Good Show Error In Console    
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                // Migrate It In Try Catch To Avoid Throw Exception While Update Database
                await _storeDbContext.Database.MigrateAsync();

                // Seeding Data
                await StoreDbContextSeed.SeedAsync(_storeDbContext);
            }
            catch (Exception ex)     
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occured during apply the migration!");
            }
            #endregion

            #region Configure Kestral Middlewares   

            // -- Server Error Middleware (we catch it in class ExceptionMiddleware)
            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                // -- Add Swagger Middelwares In Extension Method
                app.AddSwaggerMiddlewares();
            }

            // -- Error Not Found End Point: Here When This Error Thrown: It Redirect To This End Point in (Controller: Errors)
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            // -- To Redirect Any Http Request To Https
            app.UseHttpsRedirection();

            // -- To this application can resolve on any static file like (html, wwwroot, etc..)
            app.UseStaticFiles();

            /// -- In MVC We Used This Way For Routing
            ///app.UseRouting(); // -> we use this middleware to match request to an endpoint
            ///app.UseEndpoints  // -> we use this middleware to excute the matched endpoint
            ///(endpoints =>  
            ///{
            ///    endpoints.MapControllerRoute(
            ///        name: "default",
            ///        pattern: "{controller}/{action}"
            ///        );
            ///});
            /// -- But We Use MapController Instead Of It Because We Create Routing On Controller Itself
            app.MapControllers(); // -> we use this middleware to talk program that: your routing depend on route written on the controller

            #endregion

            app.Run();
        }
    }
}
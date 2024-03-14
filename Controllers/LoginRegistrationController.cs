using Microsoft.AspNetCore.Mvc;
using MyWorkDetailsProject.Models;

namespace MyWorkDetailsProject.Controllers
{
    
    public class LoginRegistrationController : Controller
    {
        private readonly ApplicationDBCOntext _applicationDBCOntext;
        public LoginRegistrationController(ApplicationDBCOntext applicationDBCOntext) 
        {
            _applicationDBCOntext = applicationDBCOntext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            HttpContext.Session.SetString("Name", "Durga Prasad");
            ViewBag.title = "Login";
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login login)
        {
            
            if(ModelState.IsValid)
            {
                var userid=login.UserID;
                var password=login.Password;
                var result= _applicationDBCOntext.logins.SingleOrDefault(ml=>ml.UserID== userid && ml.Password == password);
                if(result==null)
                {
                    return Content($"{userid} is not found in DB");
                }
                else
                {
                    
                    TempData["UserID"] = userid;
                    HttpContext.Session.SetString("UserID", userid);
                    Response.Cookies.Append("UserName", userid);
                    
                    //ViewBag.UserID = userid;
                    //ViewData["UserID"] = userid;
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                    //return Content($"{userid} Successfully Logged In");
                }
                
            }
            else
            {
                return Content("Error");
            }

           
        }
        [HttpGet]
        public IActionResult Register()
        {
            ViewData["Title"] = "Register";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            if(ModelState.IsValid)
            {
                var userid=register.UserID;
                var fname = register.FName;
                var lname=register.LName;
                var gender=register.Gender;
                var email=register.Email;
                var phone=register.Phone;
                var city=register.City;
                var country=register.Country;
                var password=register.Password;
                var confirmpassword = register.ConfirmPassword;

               await  _applicationDBCOntext.registers.AddAsync(register);
                await _applicationDBCOntext.SaveChangesAsync();
               await _applicationDBCOntext.logins.AddAsync(new Models.Login
                {
                    UserID= userid,
                    Password= password
                });
               await  _applicationDBCOntext.SaveChangesAsync();
               // ViewBag.UserID = userid;
                return RedirectToAction("Login");
                
            }
            else
            {
                return View();
            }
          
        }
    }
}

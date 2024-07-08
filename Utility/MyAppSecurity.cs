//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using PaymentApp.Data;

//namespace PaymentApp.Utility
//{
//    public class MyAppSecurity : IAuthorizationFilter
//    {

//        private readonly IHttpContextAccessor _contextAccessor;

//        public MyAppSecurity(IHttpContextAccessor contextAccessor)
//        {
//            _contextAccessor = contextAccessor;
//        }

//        public void OnAuthorization(AuthorizationFilterContext context)
//        {
//            if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
//            {
//                context.Result = new UnauthorizedResult();
//            }   
//        }
//    }
//}

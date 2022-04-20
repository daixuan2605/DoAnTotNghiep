using System.Web.Mvc;

namespace MineFruits.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //GET: Admin/ManageBrand
            context.MapRoute(
                "ManageBrand",
                "Admin/{controller}/{action}",
                new { controller = "ManageBrand", action = "Index" }
            );

            //GET: Admin/ManageCategory
            context.MapRoute(
                "ManageCategory",
                "Admin/{controller}/{action}",
                new { controller = "ManageCategory", action = "Index" }
            );
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtolHub.Web.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual IActionResult Index()
        {
            //TODO:: ADD Swagger to pipe line and configure service
            //TODO:: необходимо добавить подключение сваггера
            return Redirect("~/swagger");
        }
    }
}

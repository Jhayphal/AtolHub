using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FieldService.Web.Controllers
{
    [ApiController]
    [Area("Api")]
    [Route("[area]/v1/[controller]/[action]")]
    public abstract partial class BaseApiController : Controller
    {

    }
}

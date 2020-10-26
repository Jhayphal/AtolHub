using Microsoft.AspNetCore.Mvc;

namespace AtolHub.Framework.Mvc
{
    public class NullJsonResult : JsonResult
    {
        public NullJsonResult() : base(null)
        {
        }
    }
}

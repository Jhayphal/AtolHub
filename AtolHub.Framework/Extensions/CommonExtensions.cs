using AtolHub.Core;
using AtolHub.Core.Infrastructure;
using AtolHub.Framework.Kendoui;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AtolHub.Framework.Extensions
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class CommonExtensions
    {
        public static IEnumerable<T> PagedForCommand<T>(this IEnumerable<T> current, DataSourceRequest command)
        {
            return current.Skip((command.Page - 1) * command.PageSize).Take(command.PageSize);
        }
        public static IEnumerable<T> PagedForCommand<T>(this IEnumerable<T> current, int pageIndex, int pageSize)
        {
            return current.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    }
}

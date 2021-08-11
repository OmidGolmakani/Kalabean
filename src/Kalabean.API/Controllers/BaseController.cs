using CurrencyExchange.Filter;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kalabean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthorizationFilter]
    public class BaseController : ControllerBase
    {

    }
}

using Microsoft.AspNetCore.Mvc;

namespace Adform.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public abstract class AdformControllerBase : ControllerBase
    {
    }
}

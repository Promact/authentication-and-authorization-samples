using Auth0.Demo.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Auth0.Demo.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class DemoController : AbpControllerBase
{
    protected DemoController()
    {
        LocalizationResource = typeof(DemoResource);
    }
}

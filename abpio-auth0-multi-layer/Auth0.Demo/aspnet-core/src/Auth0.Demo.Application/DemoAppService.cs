using System;
using System.Collections.Generic;
using System.Text;
using Auth0.Demo.Localization;
using Volo.Abp.Application.Services;

namespace Auth0.Demo;

/* Inherit your application services from this class.
 */
public abstract class DemoAppService : ApplicationService
{
    protected DemoAppService()
    {
        LocalizationResource = typeof(DemoResource);
    }
}

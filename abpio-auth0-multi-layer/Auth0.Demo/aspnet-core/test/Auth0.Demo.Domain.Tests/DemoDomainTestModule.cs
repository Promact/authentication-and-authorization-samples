using Auth0.Demo.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Auth0.Demo;

[DependsOn(
    typeof(DemoEntityFrameworkCoreTestModule)
    )]
public class DemoDomainTestModule : AbpModule
{

}

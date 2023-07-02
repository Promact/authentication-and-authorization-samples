using Volo.Abp.Modularity;

namespace Auth0.Demo;

[DependsOn(
    typeof(DemoApplicationModule),
    typeof(DemoDomainTestModule)
    )]
public class DemoApplicationTestModule : AbpModule
{

}

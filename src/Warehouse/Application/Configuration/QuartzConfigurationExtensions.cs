using Application.Background;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Application.Configuration
{
    public static class QuartzConfigurationExtensions
    {
        public static void AddQuartzConfiguration(this IServiceCollection services, ConfigurationManager config)
        {
            var productSourceSection = config.GetSection(AppConstants.Configuration.ProductsSourceSection);
            services.Configure<ProductsSourceConfig>(productSourceSection);

            var productsSourceConfig = productSourceSection.Get<ProductsSourceConfig>();
            services.AddQuartz(options =>
            {
                var jobKey = JobKey.Create(nameof(ProductsSyncService));

                options
                    .AddJob<ProductsSyncService>(jobKey)
                    .AddTrigger(trigger => trigger.ForJob(jobKey)
                    .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(productsSourceConfig.RefreshTime)
                    .RepeatForever()));
            });

            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        }
    }
}

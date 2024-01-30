using Application.Common.Constants;
using Application.Configuration;
using Application.Extensions;
using Application.PipelineBehaviors;
using Application.Services.Background;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Application.Extensions;

public static class DependencyRegistrationExtention
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyRegistrationExtention).Assembly);

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyRegistrationExtention).Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddQuartz(configuration);
    }

    private static void AddQuartz(this IServiceCollection services, IConfiguration configuration)
    {
        var schedulerSection = configuration.GetSection(AppConstants.Configuration.SchedulerSection);
        var schedulerConfig = schedulerSection.Get<SchedulerConfig>();
        services.AddQuartz(options =>
        {
            var jobKey = JobKey.Create(nameof(ProductsSyncService));

            options
                .AddJob<ProductsSyncService>(jobKey)
                .AddTrigger(trigger => trigger.ForJob(jobKey)
                .WithSimpleSchedule(schedule => schedule.WithIntervalInMinutes(schedulerConfig.RecurringJobTimeInMinutes)
                .RepeatForever()));
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
    }
}

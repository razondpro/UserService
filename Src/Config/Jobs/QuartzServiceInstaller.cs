namespace UserService.Config.Jobs
{

    using Quartz;
    using UserService.Shared.Infrastructure.Jobs.OutboxMessageProcessing;

    public class QuartzServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {

            services.AddQuartz(q =>
            {
                var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
                q.AddJob<ProcessOutboxMessagesJob>(opts => opts.WithIdentity(jobKey))
                    .AddTrigger(opts => opts.ForJob(jobKey)
                        .WithSimpleSchedule(builder =>
                            builder.WithIntervalInSeconds(10)
                                .RepeatForever()));
            });

            services.AddQuartzHostedService();
        }
    }
}
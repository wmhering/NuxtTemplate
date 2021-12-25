using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using NuxtTemplate.DAL.Dto;

namespace NuxtTemplate.DAL
{
    public class NuxtTemplateContext : DbContext
    {
        ILogger _Logger;
        IAppContextAccessor _ContextAccessor;

        public NuxtTemplateContext(ILogger<NuxtTemplateContext> logger, DbContextOptions<NuxtTemplateContext> options, IAppContextAccessor contextAccessor) : base(options)
        {
            _Logger = logger;
            _ContextAccessor = contextAccessor;
        }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<WorkLogEntry> WorkLogEntries { get; set; }
    }
}

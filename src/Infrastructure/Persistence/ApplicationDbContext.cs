using ElarabyCA.Application.Common.Interfaces;
using ElarabyCA.Domain.Common;
using ElarabyCA.Domain.Entities;
using ElarabyCA.Infrastructure.Identity;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Infrastructure.Persistence
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private IDbContextTransaction _currentTransaction;

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService,
            IDateTime dateTime) : base(options, operationalStoreOptions)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        [NotMapped]
        public DbSet<TodoList> TodoLists { get; set; }
        [NotMapped]
        public DbSet<TodoItem> TodoItems { get; set; }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<InventoryTransaction> InventoryTransaction { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderTicket> OrderTicket { get; set; }
        public virtual DbSet<SparePart> SparePart { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<ValueGroup> ValueGroup { get; set; }
        public virtual DbSet<TicketCategory> TicketCategory { get; set; }
        public virtual DbSet<SMSMessage> SMSMessage { get; set; }
        public virtual DbSet<OrderSMSMessage> OrderSMSMessage { get; set; }
        public virtual DbSet<FinancialTransaction> FinancialTransaction { get; set; }

        //public virtual DbSet<OrderDetailsView> OrderDetailsView { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await base.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);

                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Ignore<TodoItem>()
                   .Ignore<TodoList>();
            //.Ignore<Store>()
            //.Ignore<SparePart>()
            //.Ignore<Inventory>()
            //.Ignore<InventoryTransaction>()
            //.Ignore<ValueGroup>();

            //builder.Entity<OrderDetailsView>(config =>
            //{
            //    config.HasNoKey();
            //    config.ToView("OrderDetailsView");
            //});
        }
    }
}

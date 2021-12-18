using ElarabyCA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace ElarabyCA.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; set; }
        DbSet<TodoItem> TodoItems { get; set; }

        DbSet<Employee> Employee { get; set; }
        DbSet<Inventory> Inventory { get; set; }
        DbSet<InventoryTransaction> InventoryTransaction { get; set; }
        DbSet<Order> Order { get; set; }
        DbSet<OrderTicket> OrderTicket { get; set; }
        DbSet<SparePart> SparePart { get; set; }
        DbSet<Store> Store { get; set; }
        DbSet<ValueGroup> ValueGroup { get; set; }

        DbSet<SMSMessage> SMSMessage { get; set; }
        DbSet<OrderSMSMessage> OrderSMSMessage { get; set; }

        //DbSet<OrderDetailsView> OrderDetailsView { get; set; }
         DbSet<TicketCategory> TicketCategory { get; set; }

        DbSet<FinancialTransaction> FinancialTransaction { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

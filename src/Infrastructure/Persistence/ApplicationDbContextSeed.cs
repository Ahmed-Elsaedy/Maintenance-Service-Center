using ElarabyCA.Domain.Entities;
using ElarabyCA.Domain.Enums;
using ElarabyCA.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ElarabyCA.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            var adminRoleExist = await roleManager.RoleExistsAsync("Administrator");
            if (!adminRoleExist)
                await roleManager.CreateAsync(new IdentityRole() { Name = "Administrator", NormalizedName = "ADMINISTRATOR" });

            var sMSManagerExist = await roleManager.RoleExistsAsync("SMSManager");
            if (!sMSManagerExist)
                await roleManager.CreateAsync(new IdentityRole() { Name = "SMSManager", NormalizedName = "SMSMANAGER" });

            var warehouseManagerExist = await roleManager.RoleExistsAsync("WarehouseManager");
            if (!warehouseManagerExist)
                await roleManager.CreateAsync(new IdentityRole() { Name = "WarehouseManager", NormalizedName = "WAREHOUSEMANAGER" });

            var ordersManagerExist = await roleManager.RoleExistsAsync("OrdersManager");
            if (!ordersManagerExist)
                await roleManager.CreateAsync(new IdentityRole() { Name = "OrdersManager", NormalizedName = "ORDERSMANAGER" });

            var dashboardManagerExist = await roleManager.RoleExistsAsync("DashboardManager");
            if (!dashboardManagerExist)
                await roleManager.CreateAsync(new IdentityRole() { Name = "DashboardManager", NormalizedName = "DASHBOARDMANAGER" });

            var financialManagerExist = await roleManager.RoleExistsAsync("FinancialManager");
            if (!financialManagerExist)
                await roleManager.CreateAsync(new IdentityRole() { Name = "FinancialManager", NormalizedName = "FINANCIALMANAGER" });


            var defaultUser = await userManager.FindByNameAsync("ahmed-elsaedy@gemography.com");
            if (defaultUser == null)
            {
                defaultUser = new ApplicationUser { UserName = "ahmed-elsaedy@gemography.com", Email = "ahmed-elsaedy@gemography.com" };
                await userManager.CreateAsync(defaultUser, "P@ssw0rd");
            }

            var isDefaultUserAdmin = await userManager.IsInRoleAsync(defaultUser, "Administrator");
            if (!isDefaultUserAdmin)
                await userManager.AddToRoleAsync(defaultUser, "Administrator");
        }

        public static async Task SeedValueGroups(ApplicationDbContext context)
        {
            var transactionTypeNames = Enum.GetNames(typeof(TransactionType));
            var transactionTypeObjs = await context.ValueGroup
                                                .Where(x => x.Group == nameof(TransactionType))
                                                .ToListAsync();

            foreach (var typeName in transactionTypeNames)
            {
                if (!transactionTypeObjs.Any(x => x.Value == typeName))
                {
                    context.ValueGroup.Add(new ValueGroup()
                    {
                        Group = nameof(TransactionType),
                        IsDeleted = false,
                        Value = typeName,
                        Created = DateTime.Now
                    });
                }
            }


            var finanicalTransactionTypeNames = Enum.GetNames(typeof(FinancialTransactionType));
            var finanicalTransactionTypeObjs = await context.ValueGroup
                                                .Where(x => x.Group == nameof(FinancialTransactionType))
                                                .ToListAsync();

            foreach (var typeName in finanicalTransactionTypeNames)
            {
                if (!finanicalTransactionTypeObjs.Any(x => x.Value == typeName))
                {
                    context.ValueGroup.Add(new ValueGroup()
                    {
                        Group = nameof(FinancialTransactionType),
                        IsDeleted = false,
                        Value = typeName,
                        Created = DateTime.Now
                    });
                }
            }

            await context.SaveChangesAsync();

        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {

            // Create a demo order
            Order order = new Order()
            {
                Customer = "Ahmed Elsaedy",
                Address = "Alexandria, Egypt",
                City = "Alexandria",
                Complaint = "Device is not working",
                DateAssigned = System.DateTime.Now,
                Model = "Samsung TV 49in",
                PrimaryPhone = "+201064041138",
                SecondaryPhone = "+201064041138",
                Product = "TV",
                Region = "Alexandria"
            };

            context.Order.Add(order);

            TicketCategory ticketCategory = new TicketCategory()
            {
                Title = "Spare Part is required",
            };

            context.TicketCategory.Add(ticketCategory);

            Employee employee = new Employee()
            {
                PrimaryPhone = "+201064041138",
                UserName = "Ahmed Elsaedy",
                FullName = "Ahmed Elsaedy",
                DisplayName = "Ahmed Elsaedy",
                IsTechnician = true
            };

            context.Employee.Add(employee);

            await context.SaveChangesAsync();


            OrderTicket orderTicket = new OrderTicket()
            {
                CategoryNavigation = ticketCategory,
                Date = System.DateTime.Now,
                IsActive = true,
                Order1 = order,
                Report = "An adaptor is required",
                UserName = "Ahmed Elsaedy"
            };

            order.ActiveTicketNavigation = orderTicket;
            context.OrderTicket.Add(orderTicket);

            //order.OrderTicket.Add(orderTicket);




            await context.SaveChangesAsync();
        }
    }
}

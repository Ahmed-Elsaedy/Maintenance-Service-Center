using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElarabyCA.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Expiration = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    OID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    DisplayName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PrimaryPhone = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    SecondaryPhone = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Province = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Town = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Street = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Birthdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdentityNumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    DateEnrolled = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ChangePasswordOnFirstLogon = table.Column<bool>(type: "INTEGER", nullable: true),
                    StoredPassword = table.Column<string>(type: "TEXT", nullable: true),
                    OptimisticLockField = table.Column<int>(type: "INTEGER", nullable: true),
                    GCRecord = table.Column<int>(type: "INTEGER", nullable: true),
                    ObjectType = table.Column<int>(type: "INTEGER", nullable: true),
                    IsTechnician = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("OID", x => x.OID);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Expiration = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Data = table.Column<string>(type: "TEXT", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "SMSMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSMessage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketCategory",
                columns: table => new
                {
                    OID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    BackColor = table.Column<int>(type: "INTEGER", nullable: true),
                    FontColor = table.Column<int>(type: "INTEGER", nullable: true),
                    FontStyle = table.Column<int>(type: "INTEGER", nullable: true),
                    SupportApplyInBatches = table.Column<bool>(type: "INTEGER", nullable: true),
                    AllowDuplicates = table.Column<bool>(type: "INTEGER", nullable: true),
                    OptimisticLockField = table.Column<int>(type: "INTEGER", nullable: true),
                    GCRecord = table.Column<int>(type: "INTEGER", nullable: true),
                    Reportable = table.Column<bool>(type: "INTEGER", nullable: true),
                    Priority = table.Column<int>(type: "INTEGER", nullable: true),
                    Activatable = table.Column<bool>(type: "INTEGER", nullable: true),
                    IncludeInOrderNotes = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketCategory", x => x.OID);
                });

            migrationBuilder.CreateTable(
                name: "ValueGroup",
                columns: table => new
                {
                    ValueGroupId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    Group = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    LastModifiedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(type: "TEXT", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueGroup", x => x.ValueGroupId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Administrator = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    LastModifiedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.StoreId);
                    table.ForeignKey(
                        name: "FK_Store_Employee",
                        column: x => x.Administrator,
                        principalTable: "Employee",
                        principalColumn: "OID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinancialTransaction",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Remarks = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    LastModifiedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(type: "TEXT", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialTransaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_FinancialTransaction_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "OID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialTransaction_ValueObject",
                        column: x => x.Type,
                        principalTable: "ValueGroup",
                        principalColumn: "ValueGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SparePart",
                columns: table => new
                {
                    SparePartId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Barcode = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    LastModifiedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SparePart", x => x.SparePartId);
                    table.ForeignKey(
                        name: "FK_SparePart_ValueObject",
                        column: x => x.Category,
                        principalTable: "ValueGroup",
                        principalColumn: "ValueGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StoreId = table.Column<int>(type: "INTEGER", nullable: false),
                    SparePartId = table.Column<int>(type: "INTEGER", nullable: false),
                    OpeningBalance = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentBalance = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    LastModifiedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(type: "TEXT", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_Inventory_SparePart",
                        column: x => x.SparePartId,
                        principalTable: "SparePart",
                        principalColumn: "SparePartId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventory_Store",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTransaction",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InventoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: true),
                    Amount = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ReferenceType = table.Column<int>(type: "INTEGER", nullable: true),
                    ReferenceId = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    LastModifiedBy = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(type: "TEXT", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTransaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_InventoryTransaction_Inventory",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransaction_ValueObject",
                        column: x => x.Type,
                        principalTable: "ValueGroup",
                        principalColumn: "ValueGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderTicket",
                columns: table => new
                {
                    OID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Report = table.Column<string>(type: "TEXT", nullable: true),
                    Order = table.Column<int>(type: "INTEGER", nullable: true),
                    Employee = table.Column<int>(type: "INTEGER", nullable: true),
                    Category = table.Column<int>(type: "INTEGER", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: true),
                    OptimisticLockField = table.Column<int>(type: "INTEGER", nullable: true),
                    GCRecord = table.Column<int>(type: "INTEGER", nullable: true),
                    CauseUnCompletion = table.Column<bool>(type: "INTEGER", nullable: true),
                    Notable = table.Column<bool>(type: "INTEGER", nullable: true),
                    IncludeInOrderNotes = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTicket", x => x.OID);
                    table.ForeignKey(
                        name: "FK_OrderTicket_Category",
                        column: x => x.Category,
                        principalTable: "TicketCategory",
                        principalColumn: "OID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderTicket_Employee",
                        column: x => x.Employee,
                        principalTable: "Employee",
                        principalColumn: "OID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ORDERID = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Customer = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Region = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Town = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Street = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    PrimaryPhone = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    SecondaryPhone = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Product = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Model = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Complaint = table.Column<string>(type: "TEXT", nullable: true),
                    DateAssigned = table.Column<DateTime>(type: "datetime", nullable: true),
                    InterfaceStatus = table.Column<int>(type: "INTEGER", nullable: true),
                    DateLastDelayed = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReasonLastDelayed = table.Column<string>(type: "TEXT", nullable: true),
                    IsSynchronized = table.Column<bool>(type: "INTEGER", nullable: true),
                    OptimisticLockField = table.Column<int>(type: "INTEGER", nullable: true),
                    GCRecord = table.Column<int>(type: "INTEGER", nullable: true),
                    ReportsOverview = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    IsArchived = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsArgent = table.Column<bool>(type: "INTEGER", nullable: true),
                    ActiveTicket = table.Column<int>(type: "INTEGER", nullable: true),
                    OverviewNotes = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Tag = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Priority = table.Column<int>(type: "INTEGER", nullable: true),
                    SiteIndex = table.Column<int>(type: "INTEGER", nullable: true),
                    RowBytesCount = table.Column<int>(type: "INTEGER", nullable: true),
                    WorkOrderId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CaseNumber = table.Column<string>(type: "TEXT", nullable: true),
                    SAPNumber = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OID);
                    table.ForeignKey(
                        name: "FK_Order_ActiveTicket",
                        column: x => x.ActiveTicket,
                        principalTable: "OrderTicket",
                        principalColumn: "OID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderSMSMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    SMSMessageId = table.Column<int>(type: "INTEGER", nullable: false),
                    LastSent = table.Column<DateTime>(type: "TEXT", nullable: true),
                    SendCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Report = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSMSMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderSMSMessage_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderSMSMessage_SMSMessage_SMSMessageId",
                        column: x => x.SMSMessageId,
                        principalTable: "SMSMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "iGCRecord_Employee",
                table: "Employee",
                column: "GCRecord");

            migrationBuilder.CreateIndex(
                name: "iObjectType_Employee",
                table: "Employee",
                column: "ObjectType");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransaction_EmployeeId",
                table: "FinancialTransaction",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransaction_Type",
                table: "FinancialTransaction",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_SparePartId",
                table: "Inventory",
                column: "SparePartId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_StoreId",
                table: "Inventory",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_InventoryId",
                table: "InventoryTransaction",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_Type",
                table: "InventoryTransaction",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "iActiveTicket_Order",
                table: "Order",
                column: "ActiveTicket");

            migrationBuilder.CreateIndex(
                name: "iGCRecord_Order",
                table: "Order",
                column: "GCRecord");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSMSMessage_OrderId",
                table: "OrderSMSMessage",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSMSMessage_SMSMessageId",
                table: "OrderSMSMessage",
                column: "SMSMessageId");

            migrationBuilder.CreateIndex(
                name: "iCategory_OrderTicket",
                table: "OrderTicket",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "iEmployee_OrderTicket",
                table: "OrderTicket",
                column: "Employee");

            migrationBuilder.CreateIndex(
                name: "iGCRecord_OrderTicket",
                table: "OrderTicket",
                column: "GCRecord");

            migrationBuilder.CreateIndex(
                name: "iOrder_OrderTicket",
                table: "OrderTicket",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_SparePart_Category",
                table: "SparePart",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Store_Administrator",
                table: "Store",
                column: "Administrator");

            migrationBuilder.CreateIndex(
                name: "iGCRecord_TicketCategory",
                table: "TicketCategory",
                column: "GCRecord");

            migrationBuilder.CreateIndex(
                name: "iTitle_TicketCategory",
                table: "TicketCategory",
                column: "Title",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTicket_Order",
                table: "OrderTicket",
                column: "Order",
                principalTable: "Order",
                principalColumn: "OID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTicket_Employee",
                table: "OrderTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_ActiveTicket",
                table: "Order");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "FinancialTransaction");

            migrationBuilder.DropTable(
                name: "InventoryTransaction");

            migrationBuilder.DropTable(
                name: "OrderSMSMessage");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "SMSMessage");

            migrationBuilder.DropTable(
                name: "SparePart");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "ValueGroup");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "OrderTicket");

            migrationBuilder.DropTable(
                name: "TicketCategory");

            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}

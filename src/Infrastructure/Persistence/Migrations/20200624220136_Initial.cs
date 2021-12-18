using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElarabyCA.Infrastructure.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            return;

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    OID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(maxLength: 100, nullable: true),
                    DisplayName = table.Column<string>(maxLength: 100, nullable: true),
                    PrimaryPhone = table.Column<string>(maxLength: 100, nullable: true),
                    SecondaryPhone = table.Column<string>(maxLength: 100, nullable: true),
                    Province = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    Town = table.Column<string>(maxLength: 100, nullable: true),
                    Street = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Birthdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdentityNumber = table.Column<string>(maxLength: 100, nullable: true),
                    DateEnrolled = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    UserName = table.Column<string>(maxLength: 100, nullable: true),
                    ChangePasswordOnFirstLogon = table.Column<bool>(nullable: true),
                    StoredPassword = table.Column<string>(nullable: true),
                    OptimisticLockField = table.Column<int>(nullable: true),
                    GCRecord = table.Column<int>(nullable: true),
                    ObjectType = table.Column<int>(nullable: true),
                    IsTechnician = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.OID);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(maxLength: 200, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "TicketCategory",
                columns: table => new
                {
                    OID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BackColor = table.Column<int>(nullable: true),
                    FontColor = table.Column<int>(nullable: true),
                    FontStyle = table.Column<int>(nullable: true),
                    SupportApplyInBatches = table.Column<bool>(nullable: true),
                    AllowDuplicates = table.Column<bool>(nullable: true),
                    OptimisticLockField = table.Column<int>(nullable: true),
                    GCRecord = table.Column<int>(nullable: true),
                    Reportable = table.Column<bool>(nullable: true),
                    Priority = table.Column<int>(nullable: true),
                    Activatable = table.Column<bool>(nullable: true),
                    IncludeInOrderNotes = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketCategory", x => x.OID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
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
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
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
                name: "OrderTicket",
                columns: table => new
                {
                    OID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 100, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Report = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: true),
                    Employee = table.Column<int>(nullable: true),
                    Category = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    OptimisticLockField = table.Column<int>(nullable: true),
                    GCRecord = table.Column<int>(nullable: true),
                    CauseUnCompletion = table.Column<bool>(nullable: true),
                    Notable = table.Column<bool>(nullable: true),
                    IncludeInOrderNotes = table.Column<bool>(nullable: true)
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
                    OID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ORDERID = table.Column<string>(maxLength: 100, nullable: true),
                    Customer = table.Column<string>(maxLength: 100, nullable: true),
                    Region = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    Town = table.Column<string>(maxLength: 100, nullable: true),
                    Street = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PrimaryPhone = table.Column<string>(maxLength: 100, nullable: true),
                    SecondaryPhone = table.Column<string>(maxLength: 100, nullable: true),
                    Product = table.Column<string>(maxLength: 100, nullable: true),
                    Model = table.Column<string>(maxLength: 100, nullable: true),
                    Complaint = table.Column<string>(nullable: true),
                    DateAssigned = table.Column<DateTime>(type: "datetime", nullable: true),
                    InterfaceStatus = table.Column<int>(nullable: true),
                    DateLastDelayed = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReasonLastDelayed = table.Column<string>(nullable: true),
                    IsSynchronized = table.Column<bool>(nullable: true),
                    OptimisticLockField = table.Column<int>(nullable: true),
                    GCRecord = table.Column<int>(nullable: true),
                    ReportsOverview = table.Column<string>(maxLength: 100, nullable: true),
                    IsArchived = table.Column<bool>(nullable: true),
                    IsArgent = table.Column<bool>(nullable: true),
                    ActiveTicket = table.Column<int>(nullable: true),
                    OverviewNotes = table.Column<string>(maxLength: 100, nullable: true),
                    Tag = table.Column<string>(maxLength: 100, nullable: true),
                    Priority = table.Column<int>(nullable: true),
                    SiteIndex = table.Column<int>(nullable: true),
                    RowBytesCount = table.Column<int>(nullable: true),
                    WorkOrderId = table.Column<Guid>(nullable: true),
                    CaseNumber = table.Column<string>(nullable: true),
                    SAPNumber = table.Column<int>(nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "iActiveTicket_Order",
                table: "Order",
                column: "ActiveTicket");

            migrationBuilder.CreateIndex(
                name: "iGCRecord_Order",
                table: "Order",
                column: "GCRecord");

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
                name: "iGCRecord_TicketCategory",
                table: "TicketCategory",
                column: "GCRecord");

            migrationBuilder.CreateIndex(
                name: "iTitle_TicketCategory",
                table: "TicketCategory",
                column: "Title",
                unique: true,
                filter: "[Title] IS NOT NULL");

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
            return;

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
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "OrderTicket");

            migrationBuilder.DropTable(
                name: "TicketCategory");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}

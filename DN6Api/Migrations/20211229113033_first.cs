using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DN6Api.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentID = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Description = table.Column<string>(type: "ntext", nullable: true),
                    Picture = table.Column<byte[]>(type: "image", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyID);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    RegionID = table.Column<int>(type: "int", nullable: false),
                    RegionDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.RegionID);
                });

            migrationBuilder.CreateTable(
                name: "Shippers",
                columns: table => new
                {
                    ShipperID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippers", x => x.ShipperID);
                });

            migrationBuilder.CreateTable(
                name: "SpareParts",
                columns: table => new
                {
                    SpareID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpareName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Units = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CashCost = table.Column<decimal>(type: "money", nullable: true),
                    ChecqueCost = table.Column<decimal>(type: "money", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpareParts", x => x.SpareID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ContactTitle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    City = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    HomePage = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "CompanyUsers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompanyUsers_Companies",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TaxOffice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK_Customers_Companies",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TitleOfCourtesy = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    HireDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    City = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Region = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Photo = table.Column<byte[]>(type: "image", nullable: true),
                    Notes = table.Column<string>(type: "ntext", nullable: true),
                    ReportsTo = table.Column<int>(type: "int", nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employees_Companies",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                    table.ForeignKey(
                        name: "FK_Employees_Employees",
                        column: x => x.ReportsTo,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "Territories",
                columns: table => new
                {
                    TerritoryID = table.Column<int>(type: "int", nullable: false),
                    TerritoryDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Territories", x => x.TerritoryID);
                    table.ForeignKey(
                        name: "FK_Territories_Region",
                        column: x => x.RegionID,
                        principalTable: "Region",
                        principalColumn: "RegionID");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    QuantityPerUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CashCost = table.Column<decimal>(type: "money", nullable: true),
                    ChecqueCost = table.Column<decimal>(type: "money", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: true, defaultValueSql: "((0))"),
                    UnitsInStock = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    UnitsOnOrder = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    ReorderLevel = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    Discontinued = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    Photo = table.Column<byte[]>(type: "image", nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Units = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_Categories",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK_Products_Suppliers",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: true),
                    OrderName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    RequiredDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ShippedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ShipVia = table.Column<int>(type: "int", nullable: true),
                    Total = table.Column<decimal>(type: "money", nullable: true, defaultValueSql: "((0))"),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TaxTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ShipFees = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssemblyFees = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipAddress = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    ShipCity = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ShipRegion = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ShipPostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ShipCountry = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    GrandTotal = table.Column<decimal>(type: "money", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    OrderStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    OrderNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Companies",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID");
                    table.ForeignKey(
                        name: "FK_Orders_Customers",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Employees",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID");
                    table.ForeignKey(
                        name: "FK_Orders_Shippers",
                        column: x => x.ShipVia,
                        principalTable: "Shippers",
                        principalColumn: "ShipperID");
                });

            migrationBuilder.CreateTable(
                name: "ProductParts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpareID = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Prepared = table.Column<bool>(type: "bit", nullable: true),
                    Sent = table.Column<bool>(type: "bit", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CashCost = table.Column<decimal>(type: "money", nullable: true),
                    ChecqueCost = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductParts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductParts_Products",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_ProductParts_SpareParts",
                        column: x => x.SpareID,
                        principalTable: "SpareParts",
                        principalColumn: "SpareID");
                });

            migrationBuilder.CreateTable(
                name: "Order Details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    Quantity = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((1))"),
                    Discount = table.Column<float>(type: "real", nullable: true, defaultValueSql: "((0))"),
                    DiscountPercent = table.Column<float>(type: "real", nullable: true),
                    Total = table.Column<decimal>(type: "money", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Details_Orders",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID");
                    table.ForeignKey(
                        name: "FK_Order_Details_Products",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateIndex(
                name: "CategoryName",
                table: "Categories",
                column: "CategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUsers_CompanyID",
                table: "CompanyUsers",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "City",
                table: "Customers",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "CompanyName",
                table: "Customers",
                column: "CompanyName");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CompanyID",
                table: "Customers",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "PostalCode",
                table: "Customers",
                column: "PostalCode");

            migrationBuilder.CreateIndex(
                name: "Region",
                table: "Customers",
                column: "Region");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyID",
                table: "Employees",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ReportsTo",
                table: "Employees",
                column: "ReportsTo");

            migrationBuilder.CreateIndex(
                name: "LastName",
                table: "Employees",
                column: "LastName");

            migrationBuilder.CreateIndex(
                name: "PostalCode1",
                table: "Employees",
                column: "PostalCode");

            migrationBuilder.CreateIndex(
                name: "IX_Order Details_OrderID",
                table: "Order Details",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Order Details_ProductID",
                table: "Order Details",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CompanyID",
                table: "Orders",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeID",
                table: "Orders",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipVia",
                table: "Orders",
                column: "ShipVia");

            migrationBuilder.CreateIndex(
                name: "IX_ProductParts_ProductID",
                table: "ProductParts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductParts_SpareID",
                table: "ProductParts",
                column: "SpareID");

            migrationBuilder.CreateIndex(
                name: "CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "ProductName",
                table: "Products",
                column: "ProductName");

            migrationBuilder.CreateIndex(
                name: "SuppliersProducts",
                table: "Products",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "CompanyName1",
                table: "Suppliers",
                column: "CompanyName");

            migrationBuilder.CreateIndex(
                name: "PostalCode2",
                table: "Suppliers",
                column: "PostalCode");

            migrationBuilder.CreateIndex(
                name: "IX_Territories_RegionID",
                table: "Territories",
                column: "RegionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyUsers");

            migrationBuilder.DropTable(
                name: "Order Details");

            migrationBuilder.DropTable(
                name: "ProductParts");

            migrationBuilder.DropTable(
                name: "Territories");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SpareParts");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Shippers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}

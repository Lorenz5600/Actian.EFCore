------------------------------------------------------------------------------------------------------------------------
-- Constraints and indexes for Employees
------------------------------------------------------------------------------------------------------------------------

ALTER TABLE "Employees" ADD CONSTRAINT "PK_Employees" PRIMARY KEY (
    "EmployeeID"
);

ALTER TABLE "Employees" ADD CONSTRAINT "FK_Employees_Employees" FOREIGN KEY
(
    "ReportsTo"
) REFERENCES "Employees" (
    "EmployeeID"
);

ALTER TABLE "Employees" ADD CONSTRAINT "CK_Birthdate" CHECK ("BirthDate" < CURRENT_DATE);

CREATE INDEX "Employees-LastName" ON "Employees" ("LastName");

CREATE INDEX "Employees-PostalCode" ON "Employees" ("PostalCode");

\nocontinue\p\g

-- DECLARE @isFullTextSearchingEnabled bit;

-- SELECT @isFullTextSearchingEnabled = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled');

-- IF(@isFullTextSearchingEnabled = 1)
-- BEGIN
--     CREATE FULLTEXT CATALOG Northwing_FTC AS DEFAULT;

--     CREATE FULLTEXT INDEX ON Employees (Title, City)
--        KEY INDEX PK_Employees;
-- END
-- \nocontinue\p\g


------------------------------------------------------------------------------------------------------------------------
-- Constraints and indexes for Categories
------------------------------------------------------------------------------------------------------------------------

ALTER TABLE "Categories" ADD CONSTRAINT "PK_Categories" PRIMARY KEY
(
    "CategoryID"
);

CREATE INDEX "Categories-CategoryName" ON "Categories" ("CategoryName");

\nocontinue\p\g


------------------------------------------------------------------------------------------------------------------------
-- Constraints and indexes for Customers
------------------------------------------------------------------------------------------------------------------------

ALTER TABLE "Customers" ADD CONSTRAINT "PK_Customers" PRIMARY KEY
(
    "CustomerID"
);

CREATE INDEX "Customers-City" ON "Customers" ("City");

CREATE INDEX "Customers-CompanyName" ON "Customers" ("CompanyName");

CREATE INDEX "Customers-PostalCode" ON "Customers" ("PostalCode");

CREATE INDEX "Customers-Region" ON "Customers" ("Region");

\nocontinue\p\g

------------------------------------------------------------------------------------------------------------------------
-- Constraints and indexes for Shippers
------------------------------------------------------------------------------------------------------------------------

ALTER TABLE "Shippers" ADD CONSTRAINT "PK_Shippers" PRIMARY KEY
(
    "ShipperID"
);

\nocontinue\p\g

------------------------------------------------------------------------------------------------------------------------
-- Constraints and indexes for Suppliers
------------------------------------------------------------------------------------------------------------------------

ALTER TABLE "Suppliers" ADD CONSTRAINT "PK_Suppliers" PRIMARY KEY
(
    "SupplierID"
);

CREATE INDEX "Suppliers-CompanyName" ON "Suppliers" ("CompanyName");

CREATE INDEX "Suppliers-PostalCode" ON "Suppliers" ("PostalCode");

\nocontinue\p\g


------------------------------------------------------------------------------------------------------------------------
-- Constraints and indexes for Orders
------------------------------------------------------------------------------------------------------------------------

ALTER TABLE "Orders" ADD CONSTRAINT "PK_Orders" PRIMARY KEY
(
    "OrderID"
);

ALTER TABLE "Orders" ADD CONSTRAINT "FK_Orders_Customers" FOREIGN KEY
(
    "CustomerID"
) REFERENCES "Customers" (
    "CustomerID"
);

ALTER TABLE "Orders" ADD CONSTRAINT "FK_Orders_Employees" FOREIGN KEY
(
    "EmployeeID"
) REFERENCES "Employees" (
    "EmployeeID"
);

ALTER TABLE "Orders" ADD CONSTRAINT "FK_Orders_Shippers" FOREIGN KEY
(
    "ShipVia"
) REFERENCES "Shippers" (
    "ShipperID"
);


CREATE INDEX "Orders-CustomerID" ON "Orders" ("CustomerID");

CREATE INDEX "Orders-CustomersOrders" ON "Orders" ("CustomerID");

CREATE INDEX "Orders-EmployeeID" ON "Orders" ("EmployeeID");

CREATE INDEX "Orders-EmployeesOrders" ON "Orders" ("EmployeeID");

CREATE INDEX "Orders-OrderDate" ON "Orders" ("OrderDate");

CREATE INDEX "Orders-ShippedDate" ON "Orders" ("ShippedDate");

CREATE INDEX "Orders-ShippersOrders" ON "Orders" ("ShipVia");

CREATE INDEX "Orders-ShipPostalCode" ON "Orders" ("ShipPostalCode");


\nocontinue\p\g

------------------------------------------------------------------------------------------------------------------------
-- Constraints and indexes for Products
------------------------------------------------------------------------------------------------------------------------

ALTER TABLE "Products" ADD CONSTRAINT "PK_Products" PRIMARY KEY
(
    "ProductID"
);

ALTER TABLE "Products" ADD CONSTRAINT "FK_Products_Categories" FOREIGN KEY
(
    "CategoryID"
) REFERENCES "Categories" (
    "CategoryID"
);

ALTER TABLE "Products" ADD CONSTRAINT "FK_Products_Suppliers" FOREIGN KEY
(
    "SupplierID"
) REFERENCES "Suppliers" (
    "SupplierID"
);

ALTER TABLE "Products" ADD CONSTRAINT "CK_Products_UnitPrice" CHECK ("UnitPrice" >= 0);

ALTER TABLE "Products" ADD CONSTRAINT "CK_ReorderLevel" CHECK ("ReorderLevel" >= 0);

ALTER TABLE "Products" ADD CONSTRAINT "CK_UnitsInStock" CHECK ("UnitsInStock" >= 0);

ALTER TABLE "Products" ADD CONSTRAINT "CK_UnitsOnOrder" CHECK ("UnitsOnOrder" >= 0);


CREATE INDEX "Products-CategoriesProducts" ON "Products" ("CategoryID");

CREATE INDEX "Products-CategoryID" ON "Products" ("CategoryID");

CREATE INDEX "Products-ProductName" ON "Products" ("ProductName");

CREATE INDEX "Products-SupplierID" ON "Products" ("SupplierID");

CREATE INDEX "Products-SuppliersProducts" ON "Products" ("SupplierID");

\nocontinue\p\g

------------------------------------------------------------------------------------------------------------------------
-- Constraints and indexes for Order Details
------------------------------------------------------------------------------------------------------------------------

ALTER TABLE "Order Details" ADD CONSTRAINT "PK_Order_Details" PRIMARY KEY
(
    "OrderID",
    "ProductID"
);

ALTER TABLE "Order Details" ADD CONSTRAINT "FK_Order_Details_Orders" FOREIGN KEY
(
    "OrderID"
) REFERENCES "Orders" (
    "OrderID"
);

ALTER TABLE "Order Details" ADD CONSTRAINT "FK_Order_Details_Products" FOREIGN KEY
(
    "ProductID"
) REFERENCES "Products" (
    "ProductID"
);

ALTER TABLE "Order Details" ADD CONSTRAINT "CK_Discount" CHECK ("Discount" >= 0 and ("Discount" <= 1));

ALTER TABLE "Order Details" ADD CONSTRAINT "CK_Quantity" CHECK ("Quantity" > 0);

ALTER TABLE "Order Details" ADD CONSTRAINT "CK_UnitPrice" CHECK ("UnitPrice" >= 0);


CREATE INDEX "Order Details-OrderID" ON "Order Details" ("OrderID");

CREATE INDEX "Order Details-OrdersOrder_Details" ON "Order Details" ("OrderID");

CREATE INDEX "Order Details-ProductID" ON "Order Details" ("ProductID");

CREATE INDEX "Order Details-ProductsOrder_Details" ON "Order Details" ("ProductID");

\nocontinue\p\g


------------------------------------------------------------------------------------------------------------------------
-- Constraints and indexes for CustomerDemographics
------------------------------------------------------------------------------------------------------------------------

ALTER TABLE "CustomerDemographics" ADD CONSTRAINT "PK_CustomerDemographics" PRIMARY KEY
(
    "CustomerTypeID"
);

\nocontinue\p\g


------------------------------------------------------------------------------------------------------------------------
-- Constraints and indexes for CustomerCustomerDemo
------------------------------------------------------------------------------------------------------------------------

ALTER TABLE "CustomerCustomerDemo" ADD CONSTRAINT "PK_CustomerCustomerDemo" PRIMARY KEY
(
    "CustomerID",
    "CustomerTypeID"
);

ALTER TABLE "CustomerCustomerDemo" ADD CONSTRAINT "FK_CustomerCustomerDemo" FOREIGN KEY
(
    "CustomerTypeID"
) REFERENCES "CustomerDemographics" (
    "CustomerTypeID"
);

ALTER TABLE "CustomerCustomerDemo" ADD CONSTRAINT "FK_CustomerCustomerDemo_Customers" FOREIGN KEY
(
    "CustomerID"
) REFERENCES "Customers" (
    "CustomerID"
);

\nocontinue\p\g


------------------------------------------------------------------------------------------------------------------------
-- Constraints and indexes for Region
------------------------------------------------------------------------------------------------------------------------

ALTER TABLE "Region" ADD CONSTRAINT "PK_Region" PRIMARY KEY
(
    "RegionID"
);

\nocontinue\p\g


------------------------------------------------------------------------------------------------------------------------
-- Constraints and indexes for Territories
------------------------------------------------------------------------------------------------------------------------

ALTER TABLE "Territories" ADD CONSTRAINT "PK_Territories" PRIMARY KEY
(
    "TerritoryID"
);

\nocontinue\p\g


------------------------------------------------------------------------------------------------------------------------
-- Constraints and indexes for Territories
------------------------------------------------------------------------------------------------------------------------

ALTER TABLE "Territories" ADD CONSTRAINT "FK_Territories_Region" FOREIGN KEY
(
    "RegionID"
) REFERENCES "Region" (
    "RegionID"
);

\nocontinue\p\g


------------------------------------------------------------------------------------------------------------------------
-- Constraints and indexes for EmployeeTerritories
------------------------------------------------------------------------------------------------------------------------

ALTER TABLE "EmployeeTerritories" ADD CONSTRAINT "PK_EmployeeTerritories" PRIMARY KEY
(
    "EmployeeID",
    "TerritoryID"
);

ALTER TABLE "EmployeeTerritories" ADD CONSTRAINT "FK_EmployeeTerritories_Employees" FOREIGN KEY
(
    "EmployeeID"
) REFERENCES "Employees" (
    "EmployeeID"
);

ALTER TABLE "EmployeeTerritories" ADD CONSTRAINT "FK_EmployeeTerritories_Territories" FOREIGN KEY
(
    "TerritoryID"
) REFERENCES "Territories" (
    "TerritoryID"
);

\nocontinue\p\g

using System;
using Actian.EFCore.TestUtilities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query;

public class NorthwindQueryActianFixture<TModelCustomizer> : NorthwindQueryRelationalFixture<TModelCustomizer>
    where TModelCustomizer : IModelCustomizer, new()
{
    protected override ITestStoreFactory TestStoreFactory
        => ActianNorthwindTestStoreFactory.Instance;

    protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
    {
        base.OnModelCreating(modelBuilder, context);

        modelBuilder.Entity<Customer>(
            b =>
            {
                b.Property(c => c.CustomerID).HasColumnType("nchar(5)");
                b.Property(cm => cm.CompanyName).HasMaxLength(40);
                b.Property(cm => cm.ContactName).HasMaxLength(30);
                b.Property(cm => cm.ContactTitle).HasColumnType("national character varying(30)");
            });

        modelBuilder.Entity<Employee>(
            b =>
            {
                b.Property(c => c.EmployeeID).HasColumnType("int");
                b.Property(c => c.ReportsTo).HasColumnType("int");
            });

        modelBuilder.Entity<Order>(
            b =>
            {
                b.Property(o => o.EmployeeID).HasColumnType("int");
                b.Property(o => o.OrderDate).HasColumnType("datetime");
            });

        modelBuilder.Entity<OrderDetail>()
            .Property(od => od.UnitPrice)
            .HasColumnType("money");

        modelBuilder.Entity<Product>(
            b =>
            {
                b.Property(p => p.UnitPrice).HasColumnType("money");
                b.Property(p => p.UnitsInStock).HasColumnType("smallint");
                b.Property(cm => cm.ProductName).HasMaxLength(40);
            });

        modelBuilder.Entity<MostExpensiveProduct>()
            .Property(p => p.UnitPrice)
            .HasColumnType("money");
    }

    protected override Type ContextType
        => typeof(NorthwindActianContext);
}

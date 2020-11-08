using FluentAssertions;
using Ingres.Client;
using Xunit;

namespace Actian.EFCore.Tests
{
    public class ConnectionStrings
    {
        private const string ConnectionString = "Server=actian-client-test;Port=II7;Database=efcore_test;User ID=efcore_test;Password=efcore_test";

        [Fact]
        public void IngresConnectionStringBuilder_preserves_password()
        {
            var builder = new IngresConnectionStringBuilder(ConnectionString);
            builder.ToString().Should().Be(ConnectionString);
        }

        [Fact]
        public void IngresConnection_preserves_password()
        {
            var connection = new IngresConnection(ConnectionString);
            connection.ConnectionString.Should().Be(ConnectionString);
        }

        [Fact]
        public void Cloned_IngresConnection_preserves_password()
        {
            var connection = new IngresConnection(ConnectionString).Clone() as IngresConnection;
            connection.ConnectionString.Should().Be(ConnectionString);
        }
    }
}

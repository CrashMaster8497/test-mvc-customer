using CustomerDataLayer.BusinessEntities;
using System.Data;
using System.Data.SqlClient;

namespace CustomerDataLayer.Repositories
{
    public class AddressRepository : BaseRepository
    {
        public override string TableName { get; } = "Address";
        public override BaseRepository[] UsedByTables { get; } = Array.Empty<BaseRepository>();

        public override string KeyColumn { get; } = "AddressId";
        public override string[] NonKeyColumns { get; } =
        {
            "CustomerId",
            "AddressLine",
            "AddressLine2",
            "AddressType",
            "City",
            "PostalCode",
            "State",
            "Country"
        };

        public override SqlParameter GetKeyParameter(int key)
        {
            return new SqlParameter("@AddressId", SqlDbType.Int) { Value = key };
        }
        public override SqlParameter GetKeyParameter(Entity entity)
        {
            return new SqlParameter("@AddressId", SqlDbType.Int) { Value = entity.Id };
        }
        public override SqlParameter[] GetNonKeyParameters(Entity entity)
        {
            var address = (Address)entity;
            return new[]
            {
                new SqlParameter("@CustomerId", SqlDbType.Int) { Value = address.CustomerId },
                new SqlParameter("@AddressLine", SqlDbType.NVarChar, 100) { Value = address.AddressLine },
                new SqlParameter("@AddressLine2", SqlDbType.NVarChar, 100) { Value = (object?)address.AddressLine2 ?? DBNull.Value },
                new SqlParameter("@AddressType", SqlDbType.VarChar, 8) { Value = address.AddressType },
                new SqlParameter("@City", SqlDbType.VarChar, 50) { Value = address.City },
                new SqlParameter("@PostalCode", SqlDbType.VarChar, 6) { Value = address.PostalCode },
                new SqlParameter("@State", SqlDbType.VarChar, 20) { Value = address.State },
                new SqlParameter("@Country", SqlDbType.VarChar, 50) { Value = address.Country }
            };
        }

        public override Entity GetEntity(SqlDataReader reader)
        {
            Enum.TryParse((string)reader["AddressType"], out AddressType addressType);
            return new Address
            {
                Id = (int)reader["AddressId"],
                CustomerId = (int)reader["CustomerId"],
                AddressLine = (string)reader["AddressLine"],
                AddressLine2 = reader["AddressLine2"] == DBNull.Value ? null : (string)reader["AddressLine2"],
                AddressType = addressType,
                City = (string)reader["City"],
                PostalCode = (string)reader["PostalCode"],
                State = (string)reader["State"],
                Country = (string)reader["Country"]
            };
        }

        public List<Address> ReadByCustomerId(int customerId)
        {
            using var connection = GetConnection();

            var command = new SqlCommand(
                $"SELECT * FROM [{TableName}] " +
                "WHERE CustomerId = @CustomerId",
                connection);
            command.Parameters.Add(
                new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customerId });

            using var reader = command.ExecuteReader();

            var addresses = new List<Address>();
            while (reader.Read())
            {
                addresses.Add((Address)GetEntity(reader));
            }

            return addresses;
        }

        public int DeleteByCustomerId(int customerId)
        {
            using var connection = GetConnection();

            var command = new SqlCommand(
                $"DELETE FROM [{TableName}] " +
                "WHERE CustomerId = @CustomerId",
                connection);
            command.Parameters.Add(
                new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customerId });

            return command.ExecuteNonQuery();
        }
    }
}

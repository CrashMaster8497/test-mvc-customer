using System.Data;
using System.Data.SqlClient;
using CustomerDataLayer.BusinessEntities;

namespace CustomerDataLayer.Repositories
{
    public class CustomerRepository : BaseRepository
    {
        public override string TableName { get; } = "Customer";
        public override BaseRepository[] UsedByTables { get; } = new BaseRepository[]
        {
            new AddressRepository(),
            new NoteRepository()
        };

        public override string KeyColumn { get; } = "CustomerId";
        public override string[] NonKeyColumns { get; } =
        {
            "FirstName",
            "LastName",
            "PhoneNumber",
            "Email",
            "TotalPurchasesAmount"
        };

        public override SqlParameter GetKeyParameter(int key)
        {
            return new SqlParameter("@CustomerId", SqlDbType.Int) { Value = key };
        }
        public override SqlParameter GetKeyParameter(Entity entity)
        {
            var customer = (Customer)entity;
            return new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customer.Id };
        }
        public override SqlParameter[] GetNonKeyParameters(Entity entity)
        {
            var customer = (Customer)entity;
            return new SqlParameter[]
            {
                new("@FirstName", SqlDbType.NVarChar, 50) { Value = (object?)customer.FirstName ?? DBNull.Value },
                new("@LastName", SqlDbType.NVarChar, 50) { Value = customer.LastName },
                new("@PhoneNumber", SqlDbType.VarChar, 12) { Value = (object?)customer.PhoneNumber ?? DBNull.Value },
                new("@Email", SqlDbType.NVarChar, 100) { Value = (object?) customer.Email ?? DBNull.Value },
                new("@TotalPurchasesAmount", SqlDbType.Money) { Value = (object?) customer.TotalPurchasesAmount ?? DBNull.Value }
            };
        }

        public override Entity GetEntity(SqlDataReader reader)
        {
            return new Customer()
            {
                Id = (int)reader["CustomerId"],
                FirstName = reader["FirstName"] == DBNull.Value ? null : (string?)reader["FirstName"],
                LastName = (string)reader["LastName"],
                PhoneNumber = reader["PhoneNumber"] == DBNull.Value ? null : (string?)reader["PhoneNumber"],
                Email = reader["Email"] == DBNull.Value ? null : (string?)reader["Email"],
                TotalPurchasesAmount = reader["TotalPurchasesAmount"] == DBNull.Value ? null : (decimal?)reader["TotalPurchasesAmount"]
            };
        }
    }
}

using CustomerDataLayer.BusinessEntities;
using System.Data;
using System.Data.SqlClient;

namespace CustomerDataLayer.Repositories
{
    public class NoteRepository : BaseRepository
    {
        public override string TableName { get; } = "Note";
        public override BaseRepository[] UsedByTables { get; } = Array.Empty<BaseRepository>();

        public override string KeyColumn { get; } = "NoteId";

        public override string[] NonKeyColumns { get; } =
        {
            "CustomerId",
            "Text"
        };

        public override SqlParameter GetKeyParameter(int key)
        {
            return new SqlParameter("@NoteId", SqlDbType.Int) { Value = key };
        }
        public override SqlParameter GetKeyParameter(Entity entity)
        {
            return new SqlParameter("@NoteId", SqlDbType.Int) { Value = entity.Id };
        }
        public override SqlParameter[] GetNonKeyParameters(Entity entity)
        {
            var note = (Note)entity;
            return new[]
            {
                new SqlParameter("@CustomerId", SqlDbType.Int) { Value = note.CustomerId },
                new SqlParameter("@Text", SqlDbType.NVarChar, 100) { Value = note.Text }
            };
        }

        public override Entity GetEntity(SqlDataReader reader)
        {
            return new Note
            {
                Id = (int)reader["NoteId"],
                CustomerId = (int)reader["CustomerId"],
                Text = (string)reader["Text"]
            };
        }

        public List<Note> ReadByCustomerId(int customerId)
        {
            using var connection = GetConnection();

            var command = new SqlCommand(
                $"SELECT * FROM [{TableName}] " +
                "WHERE CustomerId = @CustomerId",
                connection);
            command.Parameters.Add(
                new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customerId });

            using var reader = command.ExecuteReader();

            var addresses = new List<Note>();
            while (reader.Read())
            {
                addresses.Add((Note)GetEntity(reader));
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

using CustomerDataLayer.BusinessEntities;
using System.Data.SqlClient;

namespace CustomerDataLayer.Repositories
{
    public abstract class BaseRepository
    {
        public abstract string TableName { get; }
        public abstract BaseRepository[] UsedByTables { get; }

        public abstract string KeyColumn { get; }
        public abstract string[] NonKeyColumns { get; }

        public abstract SqlParameter GetKeyParameter(int key);
        public abstract SqlParameter GetKeyParameter(Entity entity);
        public abstract SqlParameter[] GetNonKeyParameters(Entity entity);

        public abstract Entity GetEntity(SqlDataReader reader);

        public static SqlConnection GetConnection()
        {
            var connection = new SqlConnection("server=localhost;DataBase=CustomerLib_Tests;Trusted_Connection=True");
            connection.Open();

            return connection;
        }

        public virtual int? Create(Entity entity)
        {
            using var connection = GetConnection();

            var command = new SqlCommand(
                $"INSERT INTO [{TableName}] ({string.Join(',', NonKeyColumns)}) " +
                $"OUTPUT INSERTED.{KeyColumn} " +
                $"VALUES ({string.Join(',', NonKeyColumns.Select(column => $"@{column}"))})",
                connection);
            command.Parameters.AddRange(GetNonKeyParameters(entity));

            using var reader = command.ExecuteReader();

            return reader.Read() ? (int)reader[KeyColumn] : null;
        }

        public virtual Entity? Read(int id)
        {
            using var connection = GetConnection();

            var command = new SqlCommand(
                $"SELECT * FROM [{TableName}] " +
                $"WHERE {KeyColumn} = @{KeyColumn}",
                connection);
            command.Parameters.Add(GetKeyParameter(id));

            using var reader = command.ExecuteReader();

            return reader.Read() ? GetEntity(reader) : null;
        }

        public virtual bool Update(Entity entity)
        {
            using var connection = GetConnection();

            var command = new SqlCommand(
                $"UPDATE [{TableName}] " +
                $"SET {string.Join(',', NonKeyColumns.Select(column => $"{column} = @{column}"))} " +
                $"WHERE {KeyColumn} = @{KeyColumn}",
                connection);
            command.Parameters.Add(GetKeyParameter(entity));
            command.Parameters.AddRange(GetNonKeyParameters(entity));

            return command.ExecuteNonQuery() > 0;
        }

        public virtual bool Delete(int id)
        {
            using var connection = GetConnection();

            var command = new SqlCommand(
                $"DELETE FROM [{TableName}] " +
                $"WHERE {KeyColumn} = @{KeyColumn}",
                connection);
            command.Parameters.Add(GetKeyParameter(id));

            return command.ExecuteNonQuery() > 0;
        }

        public virtual List<Entity> ReadAll()
        {
            using var connection = GetConnection();

            var command = new SqlCommand(
                $"SELECT * FROM [{TableName}]",
                connection);

            using var reader = command.ExecuteReader();

            var entities = new List<Entity>();
            while (reader.Read())
            {
                entities.Add(GetEntity(reader));
            }

            return entities;
        }

        public virtual int DeleteAll()
        {
            using var connection = GetConnection();

            foreach (var repository in UsedByTables)
            {
                repository.DeleteAll();
            }

            var command = new SqlCommand(
                $"DELETE FROM [{TableName}]",
                connection);

            return command.ExecuteNonQuery();
        }

        public virtual int Count()
        {
            using var connection = GetConnection();

            var command = new SqlCommand(
                $"SELECT COUNT(*) FROM [{TableName}]",
                connection);

            return (int)command.ExecuteScalar();
        }

        public virtual List<Entity> Read(int offset, int count)
        {
            if (count <= 0)
            {
                return new List<Entity>();
            }

            using var connection = GetConnection();

            var command = new SqlCommand(
                $"SELECT * FROM [{TableName}] " +
                $"ORDER BY {KeyColumn} " +
                $"OFFSET {offset} ROWS " +
                $"FETCH NEXT {count} ROWS ONLY",
                connection);

            using var reader = command.ExecuteReader();

            var entities = new List<Entity>();
            while (reader.Read())
            {
                entities.Add(GetEntity(reader));
            }

            return entities;
        }
    }
}

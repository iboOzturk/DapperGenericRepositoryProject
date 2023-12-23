
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using static Dapper.SqlMapper;

namespace DapperGenericRepositoryProject.API.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        IDbConnection _connection;

        readonly string connectionString = "Data Source=database-name;Initial Catalog=table-name;Integrated Security=True;TrustServerCertificate=True;";

        public GenericRepository()
        {
            _connection = new SqlConnection(connectionString);
        }

        public bool Add(T entity)
        {
            int rowsEffected = 0;
            var parameters = new DynamicParameters();
            var properties = entity.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (property == properties[0])
                    continue;

                var value = property.GetValue(entity, null);
                parameters.Add($"@{property.Name}", value);
            }
            var tableName = typeof(T).Name + 's';
            var columns = string.Join(", ", parameters.ParameterNames);
            var values = string.Join(", ", parameters.ParameterNames.Select(p => $"@{p}"));

            var query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
            rowsEffected = _connection.Execute(query, parameters);
            return rowsEffected > 0 ? true : false;


        }

        public bool Delete(int id)
        {
            int rowsEffected = 0;

            var tableName = typeof(T).Name + 's';

            PropertyInfo[] properties = typeof(T).GetProperties();
            string keyColumn = properties[0].Name;

            string query = $"DELETE FROM {tableName} Where {keyColumn} = @Id";

            rowsEffected = _connection.Execute(query, new { Id = id });


            return rowsEffected > 0 ? true : false;

        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> result = null;

            var tableName = typeof(T).Name + 's';
            string query = $"SELECT * FROM {tableName}";

            result = _connection.Query<T>(query);


            return result;

        }

        public T GetById(int id)
        {
            IEnumerable<T> result = null;


            var tableName = typeof(T).Name + 's';
            PropertyInfo[] properties = typeof(T).GetProperties();

            string keyColumn = properties[0].Name;
            string query = $"SELECT * FROM {tableName} WHERE {keyColumn} = '{id}'";

            result = _connection.Query<T>(query);


            return result.FirstOrDefault();

        }

        public bool Update(T entity)
        {
            int rowsEffected = 0;

            var parameters = new DynamicParameters();

            var properties = entity.GetType().GetProperties();
            string keyColumn = properties[0].Name;
            var tableName = typeof(T).Name + 's';

            foreach (var property in properties)
            {
                var value = property.GetValue(entity, null);
                parameters.Add($"@{property.Name}", value);
            }

            var setClauses = string.Join(", ", properties.Skip(1).Select(p => $"{p.Name} = @{p.Name}"));
            var query = $"UPDATE {tableName} SET {setClauses} WHERE {keyColumn} = @{keyColumn}";
            rowsEffected = _connection.Execute(query, parameters);

            return rowsEffected > 0 ? true : false;

        }

    }
}


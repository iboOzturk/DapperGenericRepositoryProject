using Dapper;
using DapperGenericRepositoryProject.API.Interfaces;
using DapperGenericRepositoryProject.API.Models;
using System.Data;
using System.Linq;
using System.Reflection;
using static Dapper.SqlMapper;

namespace DapperGenericRepositoryProject.API.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly Context _context;

        public GenericRepository(Context context)
        {
            _context = context;
        }

        private string GetTableName()
        {
            var typeName = typeof(T).Name;
            return typeName.EndsWith("y") ? typeName[..^1] + "ies" : typeName + "s";
        }

        private string GetKeyColumn()
        {
            return typeof(T).GetProperties().First().Name;
        }

        private DynamicParameters GetParameters(T entity, bool includeFirstProperty = false)
        {
            var parameters = new DynamicParameters();
            var properties = entity.GetType().GetProperties();

            foreach (var property in properties.Skip(includeFirstProperty ? 0 : 1))
            {
                var value = property.GetValue(entity, null);
                parameters.Add($"@{property.Name}", value);
            }

            return parameters;
        }

        public bool Add(T entity)
        {
            var parameters = GetParameters(entity);

            var tableName = GetTableName();
            var columns = string.Join(", ", parameters.ParameterNames);
            var values = string.Join(", ", parameters.ParameterNames.Select(p => $"@{p}"));

            var query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";

            using (var connection = _context.CreateConnection())
            {
                return connection.Execute(query, parameters) > 0;
            }
        }

        public bool Delete(int id)
        {
            var tableName = GetTableName();
            var keyColumn = GetKeyColumn();

            var query = $"DELETE FROM {tableName} WHERE {keyColumn} = @Id";

            using (var connection = _context.CreateConnection())
            {
                return connection.Execute(query, new { Id = id }) > 0;
            }
        }

        public IEnumerable<T> GetAll()
        {
            var tableName = GetTableName();
            var query = $"SELECT * FROM {tableName}";

            using (var connection = _context.CreateConnection())
            {
                return connection.Query<T>(query);
            }
        }

        public T GetById(int id)
        {
            var tableName = GetTableName();
            var keyColumn = GetKeyColumn();

            var query = $"SELECT * FROM {tableName} WHERE {keyColumn} = @Id";

            using (var connection = _context.CreateConnection())
            {
                return connection.QueryFirstOrDefault<T>(query, new { Id = id });
            }
        }

        public bool Update(T entity)
        {
            var parameters = GetParameters(entity,true);

            var keyColumn = GetKeyColumn();
            var tableName = GetTableName();

            var setClauses = string.Join(", ", parameters.ParameterNames.Skip(1).Select(p => $"{p} = @{p}"));

            var query = $"UPDATE {tableName} SET {setClauses} WHERE {keyColumn} = @{keyColumn}";

            using (var connection = _context.CreateConnection())
            {
                return connection.Execute(query, parameters) > 0;
            }
        }
    }
}

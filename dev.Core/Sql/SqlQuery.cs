﻿using Dapper;
using DapperExtensions;
using DapperExtensions.Sql;
using dev.Core.Logger;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace dev.Core.Sql
{
    public class SqlQuery : IQuery
    {
        const string CONNECTION_STRING = "Data Source=ACE-DESKTOP\\MSSQLSERVER03;Integrated Security=False;User ID=AcePilot10;Password=Airplane10;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database=CommandHandler";

        private ILog _log;
        private ISqlGenerator _sql;

        public SqlQuery(ILog log)
        {
            _log = log;
            _sql = new SqlGeneratorImpl(new DapperExtensionsConfiguration());
        }
        public List<T> Get<T>(string sql, object parameters)
        {
            var result = new List<T>();

            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                _log.LogTrace<SqlQuery>($"SELECT: {sql}. Parameters: {JsonConvert.SerializeObject(parameters)}");

                connection.Open();

                result = connection.Query<T>(sql, parameters).ToList();

                connection.Close();
            }

            return result.ToList();
        }

        public T GetSingle<T>(string sql, object parameters)
        {
            return Get<T>(sql, parameters).FirstOrDefault();
        }

        public void Insert<T>(T obj) where T : class
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                connection.Insert(obj);
                connection.Close();
            }
        }

        public void Update<T>(T obj) where T : class
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                connection.Update(obj);
                connection.Close();
            }
        }

        public void Save<T>(T obj, object parameters) where T : class
        {
            var properties = string.Join(" AND ", parameters.GetType().GetProperties().Select(x => $"{x.Name} = @{x.Name}"));
            var exist = Get<T>($"select * from [{ _sql.Configuration.GetMap<T>().TableName }] where {properties}", parameters);
            if (exist.Count == 0)
                Insert(obj);
            else
                Update(obj);
        }

        public bool Exist<T>(string sql, object parameters) where T : class
        {
            var exist = Get<T>(sql, parameters);
            if (exist.Count == 0)
                return false;

            return true;
        }

        public void Delete<T>(T obj) where T : class
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                connection.Delete(obj);
                connection.Close();
            }
        }

        public void Execute(string sql, object parameters)
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                _log.LogTrace<SqlQuery>($"EXECUTE: {sql}. Parameters: {JsonConvert.SerializeObject(parameters)}");

                connection.Open();
                connection.Execute(sql, parameters);
                connection.Close();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Delivery._3_Domínio.Shared
{
    public delegate T ConverterDelegate<T>(IDataReader reader);
    public static class Db
    {
        private static readonly string connectionString = @"Data Source = C:\Users\vinic\source\repos\Delivery\Delivery.sqlite\banco\DbDelivery.db";

        public static int Insert(string sql, Dictionary<string, object> parameters)
        {
            SqliteConnection connectionLite = new SqliteConnection(connectionString);

            SqliteCommand commandlite = new SqliteCommand(sql.AppendSelectIdentityLite(), connectionLite);

            commandlite.SetParametersLite(parameters);

            connectionLite.Open();

            int id = Convert.ToInt32(commandlite.ExecuteScalar());

            connectionLite.Close();

            return id;
        }

        public static void Update(string sql, Dictionary<string, object> parameters = null)
        {
            SqliteConnection connectionLite = new SqliteConnection(connectionString);

            SqliteCommand commandlite = new SqliteCommand(sql, connectionLite);

            commandlite.SetParametersLite(parameters);

            connectionLite.Open();

            commandlite.ExecuteNonQuery();

            connectionLite.Close();
        }

        public static void Delete(string sql, Dictionary<string, object> parameters)
        {

            Update(sql, parameters);
        }

        public static List<T> GetAll<T>(string sql, ConverterDelegate<T> convert, Dictionary<string, object> parameters = null)
        {


            SqliteConnection connectionLite = new SqliteConnection(connectionString);

            SqliteCommand commandLite = new SqliteCommand(sql, connectionLite);

            commandLite.SetParametersLite(parameters);

            connectionLite.Open();

            var list = new List<T>();

            using (var reader = commandLite.ExecuteReader())

                while (reader.Read())
                {
                    var obj = convert(reader);
                    list.Add(obj);
                }

            connectionLite.Close();
            return list;
        }
        public static T Get<T>(string sql, ConverterDelegate<T> convert, Dictionary<string, object> parameters)
        {

            SqliteConnection connectionLite = new SqliteConnection(connectionString);

            SqliteCommand commandLite = new SqliteCommand(sql, connectionLite);

            commandLite.SetParametersLite(parameters);

            connectionLite.Open();

            T t = default;

            using (var reader = commandLite.ExecuteReader())

                if (reader.Read())
                    t = convert(reader);

            connectionLite.Close();
            return t;
        }

        public static bool Exists(string sql, Dictionary<string, object> parameters)
        {

            SqliteConnection connectionLite = new SqliteConnection(connectionString);

            SqliteCommand commandLite = new SqliteCommand(sql, connectionLite);

            commandLite.SetParametersLite(parameters);

            connectionLite.Open();

            int numberRows = Convert.ToInt32(commandLite.ExecuteScalar());

            connectionLite.Close();

            return numberRows > 0;
        }
        private static void SetParametersLite(this SqliteCommand command, Dictionary<string, object> parameters)
        {
            if (parameters == null || parameters.Count == 0)
                return;

            foreach (var parameter in parameters)
            {
                string name = parameter.Key;

                object value = parameter.Value.IsNullOrEmpty() ? DBNull.Value : parameter.Value;

                SqliteParameter dbParameter = new SqliteParameter(name, value);

                command.Parameters.Add(dbParameter);
            }
        }
        public static string AppendSelectIdentityLite(this string sql)
        {
            return sql + ";SELECT last_insert_rowid()";
        }

        public static bool IsNullOrEmpty(this object value)
        {
            return (value is string && string.IsNullOrEmpty((string)value)) ||
                    value == null;
        }
    }
}

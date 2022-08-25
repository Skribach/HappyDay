using Dapper;
using HappyDay.Domain;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HappyDay.Repositories
{
    public class BirthdayRepository : IBirthdayRepository
    {
        //TODO: Move to appsettings.json
        public readonly string _connectionString;
        public readonly string _birthdayTable = "[HappyDay].[dbo].[Birthdays]";

        public BirthdayRepository(string connectionString = "Server=localhost;Database=HappyDay;Trusted_Connection=True;MultipleActiveResultSets=true")
        {
            _connectionString = connectionString;
        }

        public Birthday Add(Birthday birthday)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var guid = Guid.NewGuid().ToString();
                birthday.Id = guid;
                var sqlQuery = $"INSERT INTO {_birthdayTable} VALUES('{guid}', " + 
                    $"'{birthday.UserId}', " +
                    $"'{birthday.FirstName}', " +
                    $"'{birthday.LastName}', " +
                    $"'{birthday.BiDay.ToString("yyyy-MM-dd HH:mm:ss")}')";

                db.Query(sqlQuery);
            }
            return birthday;
        }

        public void DeleteById(string id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"DELETE FROM {_birthdayTable} " +
                    $"WHERE Id = '{id}'";

                db.Query(sqlQuery);
            }
        }

        public Birthday GetById(string id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"SELECT * FROM {_birthdayTable} WHERE Id = '{id}'";

                return db.Query<Birthday>(sqlQuery).FirstOrDefault();
            }
        }

        public IEnumerable<Birthday> GetByUserId(string userId)
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"SELECT * FROM {_birthdayTable} WHERE UserId = '{userId}'";

                return db.Query<Birthday>(sqlQuery).ToList();
            }
        }

        public void Update(Birthday birthday)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"UPDATE {_birthdayTable} SET " +
                    $"FirstName = '{birthday.FirstName}', " +
                    $"LastName = '{birthday.LastName}', " +
                    $"BiDay = '{birthday.BiDay.ToString("yyyy-MM-dd HH:mm:ss")}' " +
                    $"WHERE Id = '{birthday.Id}'";

                db.Query(sqlQuery);
            }
        }
    }
}

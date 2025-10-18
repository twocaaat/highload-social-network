using System.Text;
using Dapper;
using HighloadSocialNetwork.WebServer.DataAccess.Interfaces;
using HighloadSocialNetwork.WebServer.DataAccess.Models;

namespace HighloadSocialNetwork.WebServer.DataAccess.Repositories;

public class FakeRepository(IDatabase database) : IFakeRepository
{
    public Task<int> GetCountOfUsers() => database.GetOrDefault<int>("SELECT count(1) FROM users");

    public Task InsertUsers(List<UserInDb> users)
    {
        var sql = GetBulkUsersInsertQuery(users);
        var parameters = new DynamicParameters();

        for (var i = 0; i < users.Count; i++)
        {
            parameters.Add($"id{i}", users[i].Id);
            parameters.Add($"firstName{i}", users[i].FirstName);
            parameters.Add($"secondName{i}", users[i].SecondName);
            parameters.Add($"birthdate{i}", users[i].Birthdate);
            parameters.Add($"biography{i}", users[i].Biography);
            parameters.Add($"city{i}", users[i].City);
        }

        return database.Execute(sql, parameters);
    }

    private static string GetBulkUsersInsertQuery(List<UserInDb> users)
    {
        var sb = new StringBuilder();
        sb.Append("INSERT INTO users (id, first_name, second_name, birthdate, biography, city) VALUES ");

        for (var i = 0; i < users.Count; i++)
        {
            sb.Append($"(@id{i}, @firstName{i}, @secondName{i}, @birthdate{i}, @biography{i}, @city{i}),");
        }

        sb.Length--;
        return sb.ToString();
    }
}
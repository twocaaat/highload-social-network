using Bogus;
using HighloadSocialNetwork.WebServer.DataAccess.Models;

namespace HighloadSocialNetwork.WebServer.Fakers;

public sealed class UserInDbFaker : Faker<UserInDb>
{
     public UserInDbFaker()
     {
         Locale = "en";

         RuleFor(x => x.Id, f => f.Random.Guid());
         RuleFor(x => x.FirstName, f => f.Name.FirstName());
         RuleFor(x => x.SecondName, f => f.Name.LastName());
         RuleFor(x => x.Birthdate, f => f.Date.Past(40, DateTime.Today.AddYears(-18)));

         RuleFor(x => x.Biography, f => f.Lorem.Paragraphs(1, 2));
         RuleFor(x => x.City, f => f.Address.City());
     }
}
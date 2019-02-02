using System;
using Newtonsoft.Json;
using SQLite;

namespace Taskify.Model
{
    [JsonObject]
    public class User
    {
        [JsonProperty ("id")]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }


        [JsonProperty("email_verified")]
        public bool EmailVerified { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }


    }
}

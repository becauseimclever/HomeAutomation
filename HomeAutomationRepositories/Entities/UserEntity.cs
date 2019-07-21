using HomeAutomationRepositories.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomeAutomationRepositories.Entities
{
    public class UserEntity
    {
        public UserEntity() { }
        public UserEntity(User user)
        {
            this.FirstName = user.FirstName;
            this.Id = ObjectId.Parse(user.Id);
            this.LastName = user.LastName;
            this.Password = user.Password;
            this.Token = user.Token;
            this.Username = user.UserName;
        }
        [BsonId]
        public ObjectId Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Token { get; set; }

    }
}

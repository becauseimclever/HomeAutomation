using HomeAutomationRepositories.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomeAutomationRepositories.Entities
{
    public class UserEntity
    {
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
        public static User ConvertToModel(UserEntity userEntity)
        {
            return new User()
            {
                Id = userEntity.Id.ToString(),
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                UserName = userEntity.Username,
                Password = userEntity.Password,
                Token = userEntity.Token
            };
        }
    }
}

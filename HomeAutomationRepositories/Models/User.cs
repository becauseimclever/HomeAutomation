using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using HomeAutomationRepositories.Entities;
using MongoDB.Bson;

namespace HomeAutomationRepositories.Models
{
    public class User
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Token { get; set; }
        public static UserEntity ConvertToEntity(User user)
        {
            return new UserEntity()
            {
                Id = ObjectId.Parse(user.Id),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Password = user.Password,
                Token = user.Token
            };
        }
    }
}

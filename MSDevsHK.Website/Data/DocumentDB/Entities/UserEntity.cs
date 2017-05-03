using AutoMapper;
using MSDevsHK.Website.Data.Models;

namespace MSDevsHK.Website.Data.DocumentDB.Entities
{
    class UserEntity : Entity
    {
        public string Username { get; set; }
        public MeetupUserIdentity MeetupIdentity { get; set; }

        public User ToUser()
        {
            return Mapper.Map<User>(this);
        }

        public static UserEntity FromUser(User user)
        {
            return Mapper.Map<UserEntity>(user);
        }
    }
}
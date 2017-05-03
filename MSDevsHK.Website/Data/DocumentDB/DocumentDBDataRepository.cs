using AutoMapper;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using MSDevsHK.Website.Data;
using MSDevsHK.Website.Data.DocumentDB.Entities;
using MSDevsHK.Website.Data.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MSDevsHK.Website.Data.DocumentDB
{
    class DocumentDBDataRepositoryOptions
    {
        public string Endpoint { get; set; }
        public string AuthKey { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }

    class DocumentDBDataRepository : IDataRepository
    {
        static DocumentDBDataRepository()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserEntity>();
                cfg.CreateMap<UserEntity, User>();
            });
        }

        public DocumentDBDataRepository(IOptions<DocumentDBDataRepositoryOptions> optionsAccessor)
        {
            _documentClient = new DocumentClient(new Uri(optionsAccessor.Value.Endpoint), optionsAccessor.Value.AuthKey);
            _databaseName = optionsAccessor.Value.DatabaseName;
            _collectionName = optionsAccessor.Value.CollectionName;

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public Task CreateUser(User user)
        {
            var userEntity = UserEntity.FromUser(user);
            userEntity.Type = Entity.TypeUser;

            return _documentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName), userEntity);
        }

        public User GetUserByMeetupId(string meetupId)
        {
            var user = _documentClient.CreateDocumentQuery<UserEntity>(UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName),
                $"SELECT * FROM user WHERE user.type = '{Entity.TypeUser}' AND user.meetupIdentity.meetupId = '{meetupId}'")
                .SingleOrDefault();
            
            return user != null ? user.ToUser() : null;
        }

        private readonly DocumentClient _documentClient;
        private readonly string _collectionName;
        private readonly string _databaseName;
    }
}
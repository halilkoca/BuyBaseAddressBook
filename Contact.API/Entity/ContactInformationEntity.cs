﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace Contact.API.Entity
{
    public class ContactInformationEntity
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [BsonRepresentation(BsonType.String)]
        public InformationType Type { get; set; }
        public string Value { get; set; }
    }

    public enum InformationType
    {
        PhoneNumber = 1,
        EmailAddress,
        Location
    }
}
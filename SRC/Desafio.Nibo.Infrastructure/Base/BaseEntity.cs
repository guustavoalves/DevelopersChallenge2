using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Desafio.Nibo.Infrastructure.Base
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public virtual ObjectId Id { get; set; }
    }
}

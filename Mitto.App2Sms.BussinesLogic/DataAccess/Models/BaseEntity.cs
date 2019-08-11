using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System;

namespace Mitto.App2Sms.BussinesLogic.DataAccess.Models
{
    public class BaseEntity
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public BaseEntity()
        {
            Created = DateTime.UtcNow;
        }
    }
}

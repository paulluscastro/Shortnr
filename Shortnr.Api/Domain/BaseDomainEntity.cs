using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shortnr.Api.Domain
{
    public abstract class BaseDomainEntity : IBaseDomainEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; protected set; }
        public DateTime Created { get; protected set; }
        public DateTime LastUpdated { get; protected set; }
        [NotMapped]
        public List<string> Errors { get; protected set; } = new List<string>();
        protected void AddError(string error) => Errors.Add(error);
        public abstract bool Validate();
    }
}

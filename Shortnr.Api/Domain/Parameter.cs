using Microsoft.AspNetCore.Identity;
using Shortnr.Commons.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortnr.Api.Domain
{
    public class Parameter : BaseDomainEntity
    {
        public string Key { get; protected set; }
        public string Value { get; protected set; }
        protected Parameter() { }
        public Parameter(string key) => Key = key;
        public Parameter(string key, string value)
        {
            Key = key;
            Value = value;
        }
        public void NewValue(string value) => Value = value;
        public override bool Validate()
        {
            Errors.Clear();
            if (string.IsNullOrEmpty(Key))
                AddError(ParameterErrors.KeyNotInformed);
            return Errors.Count == 0;
        }
    }
}

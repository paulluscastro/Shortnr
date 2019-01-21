using Shortnr.Api.Domain;
using Shortnr.Api.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Shortnr.Api.Data;
using Shortnr.Api.Repositories.Interfaces;

namespace Shortnr.Api.Repositories
{
    public class ParameterRepository : IParameterRepository
    {
        ApiDataContext _context;
        public ParameterRepository(ApiDataContext context) => _context = context;
        public Parameter Get(string key)
        {
            Parameter parameter = null;
            try
            {
                parameter = _context.Parameters.Where(p => p.Key == key).FirstOrDefault();
                if (parameter == null)
                    parameter = new Parameter(key);
                _context.Parameters.Add(parameter);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting parameter '{key}'. Message: {ex.Message}");
                throw;
            }
            return parameter;
        }
        public Parameter GenerateNextValue(string key)
        {
            Parameter parameter = null;
            try
            {
                parameter = _context.Parameters.Where(p => p.Key == key).FirstOrDefault();
                if (parameter == null)
                {
                    parameter = new Parameter(key);
                    _context.Parameters.Add(parameter);
                    _context.SaveChanges();
                }
                parameter.NewValue(ShortenedUrlGenerator.GenerateNext(parameter.Value));
                _context.Parameters.Update(parameter);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error generating next value. Message: {ex.Message}");
            }
            return parameter;
        }
    }
}

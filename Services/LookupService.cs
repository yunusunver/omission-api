using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using omission.api.Context;
using omission.api.Exceptions;
using omission.api.Models;
using omission.api.Models.DTO;
using omission.api.Utility;

namespace omission.api.Services
{
    public class LookupService
    {

        private OmissionContext _context;

        public LookupService(OmissionContext context)
        {
            _context = context;
        }

        public List<Lookup> GetCodes(string type)
        {

            return _context.Lookups.Where(x=>x.Type==type).ToList();   
            
        }

        public Lookup GetById(int id){
            return _context.Lookups.FirstOrDefault(x=>x.Id==id);
        }

        
    }
}
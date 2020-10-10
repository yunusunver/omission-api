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

        public List<Lookup> GetCodes(string type,int page=1,int limit=5)
        {
            var skipData = (page-1)*limit;
            if(limit!=-1) return _context.Lookups.Where(x => x.Type == type && !x.isDeleted).Skip(skipData).Take(limit).ToList();
            else if(limit==-1) return _context.Lookups.Where(x => x.Type == type && !x.isDeleted).ToList();
            return null;
        }

        public Lookup GetById(int id)
        {
            return _context.Lookups.FirstOrDefault(x => x.Id == id);
        }


        public void Update(LookupUpdateDTO lookupDTO)
        {
            
            var lookup = _context.Lookups.FirstOrDefault(x=>x.Id==lookupDTO.Id);
            if(lookup==null) 
                throw new ServiceException(ExceptionMessages.LOOKUP_NOT_FOUND);
            
            lookup.UpdatedDate = DateTime.Now;
            lookup.Name = lookupDTO.Name;
            lookup.Type = lookupDTO.Type;
            lookup.OrderId = lookupDTO.OrderId;
            
            _context.Update(lookup);
            _context.SaveChanges();
        }   

        public void Add(LookupCreateDTO lookupDTO)
        {
            Lookup lookup = new Lookup {
                Name = lookupDTO.Name,
                Type = lookupDTO.Type,
                OrderId = lookupDTO.OrderId,
                CreatedDate = DateTime.Now,
                UpdatedDate = null

            };
            _context.Lookups.Add(lookup);
            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var lookup = _context.Lookups.FirstOrDefault(x=>x.Id==id);
            if(lookup == null) 
                throw new ServiceException(ExceptionMessages.LOOKUP_NOT_FOUND);
            
            lookup.isDeleted = true;
            _context.Lookups.Update(lookup);
            _context.SaveChanges();
        }

        public int Count(){
            var result = _context.Lookups.Where(x=>!x.isDeleted).ToList();
            return result.Count;
        }


        #region Validations 
        public void PostValidation(LookupCreateDTO lookupDTO)
        {
            if (string.IsNullOrEmpty(lookupDTO.Name))
                throw new ServiceException(ExceptionMessages.LOOKUP_NAME_CANNOT_BE_BLANK);
            else if (string.IsNullOrEmpty(lookupDTO.Type))
                throw new ServiceException(ExceptionMessages.TYPE_CANNOT_BE_BLANK);
        }

        public void PutValidation(LookupUpdateDTO lookupDTO)
        {
            if (lookupDTO.Id <= 0)
                throw new ServiceException(ExceptionMessages.LOOKUP_ID_NOT_AVAILABLE);
            else if (string.IsNullOrEmpty(lookupDTO.Name))
                throw new ServiceException(ExceptionMessages.LOOKUP_NAME_CANNOT_BE_BLANK);
            else if (string.IsNullOrEmpty(lookupDTO.Type))
                throw new ServiceException(ExceptionMessages.TYPE_CANNOT_BE_BLANK);

        }
        #endregion
    }
}
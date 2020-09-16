using System;
using System.Collections.Generic;
using System.Linq;
using omission.api.Context;
using omission.api.Controllers;
using omission.api.Exceptions;
using omission.api.Models;
using omission.api.Models.DTO;
using omission.api.Utility;

namespace omission.api.Services
{

    public class HashtagService
    {
        private OmissionContext _context;

        public HashtagService(OmissionContext context)
        {
            _context = context;
        }

        public Hashtag GetById(int id)
        {
            return _context.Hashtags.FirstOrDefault(x => x.Id == id);

        }

        public List<Hashtag> Gets()
        {
            return _context.Hashtags.ToList();
        }

        public void CreateValidation(HashtagCreateDTO hashtagCreateDTO)
        {
            if (string.IsNullOrEmpty(hashtagCreateDTO.Name))
                throw new ServiceException(ExceptionMessages.HASHTAG_NAME_CANNOT_BE_BLANK);

        }

        public void Add(HashtagCreateDTO hashtagCreateDTO)
        {
            Hashtag hashtag = new Hashtag
            {
                Name = hashtagCreateDTO.Name,
                CreatedDate = DateTime.Now,

            };
            _context.Hashtags.Add(hashtag);
            _context.SaveChanges();
        }

        public void UpdateValidation(HashtagUpdateDTO hashtagUpdateDTO)
        {
            if (hashtagUpdateDTO.Id <= 0)
                throw new ServiceException(ExceptionMessages.HASHTAG_ID_NOT_AVAILABLE);
            else if (string.IsNullOrEmpty(hashtagUpdateDTO.Name))
                throw new ServiceException(ExceptionMessages.HASHTAG_NAME_CANNOT_BE_BLANK);
        }

        public void Update(HashtagUpdateDTO hashtagUpdateDTO)
        {
            var hashtag = _context.Hashtags.FirstOrDefault(x => x.Id == hashtagUpdateDTO.Id);
            if (hashtag == null)
                throw new ServiceException(ExceptionMessages.HASHTAG_NOT_FOUND);
            hashtag.Name = hashtagUpdateDTO.Name;
            _context.Hashtags.Update(hashtag);
            _context.SaveChanges();

        }

        public void Remove(int id)
        {
            var hashtag = _context.Hashtags.FirstOrDefault(x => x.Id == id);
            if (hashtag == null)
                throw new ServiceException(ExceptionMessages.HASHTAG_NOT_FOUND);
            hashtag.isDeleted = true;
            _context.Hashtags.Update(hashtag);
            _context.SaveChanges();
        }
    }
}
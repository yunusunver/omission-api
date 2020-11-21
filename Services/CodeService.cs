using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using omission.api.Context;
using omission.api.Exceptions;
using omission.api.Models;
using omission.api.Models.DTO;
using omission.api.Models.ViewModels;
using omission.api.Utility;

namespace omission.api.Services
{
    public class CodeService
    {

        private UserService _userService;
        private LookupService _lookUpService;
        private HashtagService _hashtagService;
        private OmissionContext _context;

        public CodeService(UserService userService, OmissionContext context, LookupService lookupService,HashtagService hashtagService)
        {
            _userService = userService;
            _lookUpService = lookupService;
            _hashtagService = hashtagService;
            _context = context;

        }

        public void CodeValidation(CodeDTO codeDTO)
        {
            if (string.IsNullOrEmpty(codeDTO.Title))
                throw new ServiceException(ExceptionMessages.CODENAME_CANNOT_BE_BLANK);

            if (codeDTO.LookupId <= 0)
                throw new ServiceException(ExceptionMessages.CODELANGUAGE_CANNOT_BE_BLANK);

            if (string.IsNullOrEmpty(codeDTO.Code))
                throw new ServiceException(ExceptionMessages.CODEBODY_CANNOT_BE_BLANK);

        }

        public void AddCode(CodeDTO codeDTO)
        {
            var currentUser = _userService.GetLoggedInUser();
            Code code = new Code
            {
                Body = codeDTO.Code,
                LookupId = codeDTO.LookupId,
                Description = codeDTO.Description,
                Title = codeDTO.Title,
                UpdatedDate = null,
                UpdatedBy = 0,
                CreatedBy = currentUser.Id,
                Hashtags = codeDTO.Hashtags

            };
            _context.Codes.Add(code);
            _context.SaveChanges();

        }

        public List<CodeListVM> GetCodes(CodeListDTO codeListDTO)
        {
            var currentUser = _userService.GetLoggedInUser();
            
            Expression<Func<Code, bool>> where = x => x.isDeleted == false && x.CreatedBy == currentUser.Id && (
                 string.IsNullOrEmpty(codeListDTO.Search) || x.Title.ToLower().Contains(codeListDTO.Search.ToLower())
             );
        
            var programmingLanguages = _lookUpService.GetCodes(type:"programmingLanguage",limit:-1);
            var skipData = (codeListDTO.Page - 1) * codeListDTO.Limit;
            var codes = _context.Codes.Where(where).Skip(skipData).Take(codeListDTO.Limit).ToList();
            // var userUsedHashTags = codes.Select(x=>x.Hashtags).Distinct();
            var result = (from code in codes
                          join programminLanguage in programmingLanguages
                          on code.LookupId equals programminLanguage.Id


                          select new CodeListVM()
                          {
                              Id = code.Id,
                              CreatedDate = code.CreatedDate,
                              UpdatedDate = code.UpdatedDate,
                              Description = code.Description,
                              Title = code.Title,
                              Code = code.Body,
                              ProgrammingLanguage = programminLanguage.Name,
                              HashTags = this.getHashTagsByName(code.Hashtags)
                          }

            ).ToList();

            return result;

        }

        public List<string> getHashTagsByName(int[] hashTags){
            
            List<string> hashTagNames = new List<string>();
            if(hashTags.Length<=0 ||  hashTags==null) return hashTagNames;
            foreach (var item in hashTags)
            {
                var hashtag = _hashtagService.GetById(item);
                hashTagNames.Add(hashtag.Name);
            }

            return hashTagNames;

        }

        public void DeleteCode(int id)
        {
            if (id <= 0)
                throw new ServiceException(ExceptionMessages.CODEID_NOT_AVAILABLE);
            var findedCode = _context.Codes.FirstOrDefault(x => x.Id == id);
            if (findedCode == null)
                throw new ServiceException(ExceptionMessages.CODE_NOT_FOUND);
            findedCode.isDeleted  = true;   
            _context.SaveChanges();
        }

        public void UpdateCode(CodeUpdateDTO codeUpdateDTO)
        {
            var currentUser = _userService.GetLoggedInUser();
            if (codeUpdateDTO.Id <= 0)
                throw new ServiceException(ExceptionMessages.CODEID_NOT_AVAILABLE);
            
            var findedCode = _context.Codes.FirstOrDefault(x => x.Id == codeUpdateDTO.Id);
            if (findedCode == null)
                throw new ServiceException(ExceptionMessages.CODE_NOT_FOUND);
            
            findedCode.Title = codeUpdateDTO.CodeDTO.Title;
            findedCode.Description = codeUpdateDTO.CodeDTO.Description;
            findedCode.Body = codeUpdateDTO.CodeDTO.Code;
            findedCode.Hashtags = codeUpdateDTO.CodeDTO.Hashtags;
            findedCode.LookupId = codeUpdateDTO.CodeDTO.LookupId;
            findedCode.UpdatedBy = currentUser.Id;
            findedCode.UpdatedDate = DateTime.Now;
            
            _context.Codes.Update(findedCode);
            _context.SaveChanges();
        }

        public Code GetCodeById(int id){
            var currentUser = _userService.GetLoggedInUser();
            if (id <= 0)
                throw new ServiceException(ExceptionMessages.CODEID_NOT_AVAILABLE);
            
            var findedCode = _context.Codes.FirstOrDefault(x => x.Id == id);
            if (findedCode == null)
                throw new ServiceException(ExceptionMessages.CODE_NOT_FOUND);
            return findedCode;
        }
    }
}
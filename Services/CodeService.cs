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
        private OmissionContext _context;

        public CodeService(UserService userService, OmissionContext context, LookupService lookupService)
        {
            _userService = userService;
            _lookUpService = lookupService;
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
                // Hashtags = 

            };
            _context.Codes.Add(code);
            _context.SaveChanges();

        }

        public List<CodeListVM> GetCodes(CodeListDTO codeListDTO)
        {
            var currentUser = _userService.GetLoggedInUser();

            Expression<Func<Code, bool>> where = x => x.isDeleted == false && x.CreatedBy == currentUser.Id && (
                 string.IsNullOrEmpty(codeListDTO.Search) || x.Title.Contains(codeListDTO.Search)
             );
            var lookups = _lookUpService.GetCodes("programmingLanguage");
            var skipData = (codeListDTO.Page - 1) * codeListDTO.Limit;
            var codes = _context.Codes.Where(where).Skip(skipData).Take(codeListDTO.Limit).ToList();

            var result = (from code in codes
                          join lookupData in lookups
                          on code.LookupId equals lookupData.Id
                          select new CodeListVM()
                          {
                              CreatedDate = code.CreatedDate,
                              Description = code.Description,
                              Title = code.Title,
                              Code = code.Body,
                              ProgrammingLanguage = lookupData.Name
                          }

            ).ToList();

            return result;

        }
    }
}
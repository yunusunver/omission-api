using System;
using omission.api.Exceptions;
using omission.api.Models;
using omission.api.Models.DTO;
using omission.api.Utility;

namespace omission.api.Services
{
    public class CodeService
    {
        public void CodeValidation(CodeDTO codeDTO)
        {
            if (string.IsNullOrEmpty(codeDTO.Title))
                throw new ServiceException(ExceptionMessages.CODENAME_CANNOT_BE_BLANK);
            
            if (codeDTO.LookupId<=0)
                throw new ServiceException(ExceptionMessages.CODELANGUAGE_CANNOT_BE_BLANK);
           
            if (string.IsNullOrEmpty(codeDTO.Code))
                throw new ServiceException(ExceptionMessages.CODEBODY_CANNOT_BE_BLANK);
            
        }

        public void AddCode(CodeDTO codeDTO)
        {
            Code code = new Code {
                Body = codeDTO.Code,
                LookupId = codeDTO.LookupId,
                Description = codeDTO.Description,
                Title = codeDTO.Title,
                UpdatedDate = null,
                UpdatedBy = 0
            };
        }
    }
}
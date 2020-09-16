using System;
using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using omission.api.Context;
using omission.api.Exceptions;
using omission.api.Services;
using omission.api.Utility;

namespace omission.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LookupController : ControllerBase
    {

        private OmissionContext _context;
        private LookupService _lookupService;

        public LookupController(OmissionContext context, LookupService lookupService)
        {
            _context = context;
            _lookupService = lookupService;
        }

        [HttpGet("type")]
        public IActionResult Get(string type)
        {
            try
            {
                if (string.IsNullOrEmpty(type))
                    throw new ServiceException(ExceptionMessages.TYPE_CANNOT_BE_BLANK);
                var result = _lookupService.GetCodes(type);
                return Ok(result);
            }

            catch (AuthenticationException)
            {
                return Forbid();
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }

        }

        


    }
}
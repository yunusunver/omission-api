using System;
using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using omission.api.Context;
using omission.api.Exceptions;
using omission.api.Models.DTO;
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

        [HttpGet]
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

        [HttpPut]
        public IActionResult Put(LookupUpdateDTO lookupDTO)
        {
            try
            {
                _lookupService.PutValidation(lookupDTO);
                _lookupService.Update(lookupDTO);
                return Ok();
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

        [HttpPost]
        public IActionResult Post(LookupCreateDTO lookupCreateDTO)
        {
            try
            {
                _lookupService.PostValidation(lookupCreateDTO);
                _lookupService.Add(lookupCreateDTO);
                return Ok();
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


        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ServiceException(ExceptionMessages.LOOKUP_ID_NOT_AVAILABLE);
                _lookupService.Remove(id);
                return Ok();
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
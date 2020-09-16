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
    public class HashtagController : ControllerBase
    {
        private OmissionContext _context;

        private HashtagService _hashtagService;

        public HashtagController(OmissionContext context, HashtagService hashtagService)
        {
            _context = context;
            _hashtagService = hashtagService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _hashtagService.Gets();
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

        [HttpPost]
        public IActionResult Post([FromBody] HashtagCreateDTO hashtagCreateDTO)
        {
            try
            {
                _hashtagService.CreateValidation(hashtagCreateDTO);
                _hashtagService.Add(hashtagCreateDTO);
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

        [HttpPut]
        public IActionResult Put([FromBody] HashtagUpdateDTO hashtagUpdateDTO)
        {
            try
            {
                _hashtagService.UpdateValidation(hashtagUpdateDTO);
                _hashtagService.Update(hashtagUpdateDTO);
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
                    throw new ServiceException(ExceptionMessages.HASHTAG_ID_NOT_AVAILABLE);
                _hashtagService.Remove(id);
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
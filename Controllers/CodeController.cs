using System;
using System.Security.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using omission.api.Exceptions;
using omission.api.Models;
using omission.api.Models.DTO;
using omission.api.Services;

namespace omission.api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CodeController : ControllerBase
    {
        private CodeService _codeService;

        public CodeController(CodeService codeService)
        {
            _codeService = codeService;
        }


        [HttpPost]
        public IActionResult AddCode([FromBody] CodeDTO codeDTO)
        {
            try
            {
                _codeService.CodeValidation(codeDTO);
                _codeService.AddCode(codeDTO);
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


        [HttpGet]
        public IActionResult GetCodes([FromQuery] CodeListDTO codeListDTO)
        {
            try
            {
                var result = _codeService.GetCodes(codeListDTO);
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

        [HttpGet("{id}")]
        public IActionResult GetCodeById(int id){
            try
            {
                var result = _codeService.GetCodeById(id);
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
        public IActionResult UpdateCode([FromBody] CodeUpdateDTO codeUpdateDTO)
        {
            try
            {
                _codeService.UpdateCode(codeUpdateDTO);
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

        [HttpDelete("{id}")]
        public IActionResult DeleteCode(int id)
        {
            try
            {
                _codeService.DeleteCode(id);
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
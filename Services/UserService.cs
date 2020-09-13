using System;
using omission.api.Context;
using omission.api.Exceptions;
using omission.api.Models;
using omission.api.Utility;
using omission.api.Utility.Crypto;
using omission.api.Utility.Mail;
using System.Linq;
using omission.api.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace omission.api.Services
{
    public class UserService
    {
        private OmissionContext _context;
        private IConfiguration _configuration;

        private readonly ClaimsPrincipal _principal;


        public UserService(OmissionContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _principal = httpContextAccessor.HttpContext.User;
        }

        public void RegisterValidation(RegisterDTO registerDTO)
        {
            if (string.IsNullOrEmpty(registerDTO.Name))
                throw new ServiceException(ExceptionMessages.NAME_CANNOT_BE_BLANK);
            if (string.IsNullOrEmpty(registerDTO.Surname))
                throw new ServiceException(ExceptionMessages.SURNAME_CANNOT_BE_BLANK);
            if (string.IsNullOrEmpty(registerDTO.Email))
                throw new ServiceException(ExceptionMessages.EMAIL_CANNOT_BE_BLANK);
            if (string.IsNullOrEmpty(registerDTO.Password))
                throw new ServiceException(ExceptionMessages.PASSWORD_CANNOT_BE_BLANK);
            if (string.IsNullOrEmpty(registerDTO.RePassword))
                throw new ServiceException(ExceptionMessages.REPASSWORD_CANNOT_BE_BLANK);
            if (registerDTO.Password != registerDTO.RePassword)
                throw new ServiceException(ExceptionMessages.PASSWORDS_NOT_MATCHES);

        }





       
        public User GetLoggedInUser()
        {
            User loginUserVM = new User();
            var principal = _principal.Claims.ToDictionary(x => x.Type, x => x.Value);
            loginUserVM.Email = principal["Email"];
            loginUserVM.Name = principal["Name"];
            loginUserVM.Surname = principal["Surname"];
            loginUserVM.Id = Convert.ToInt32(principal["Id"]);
            return loginUserVM;
        }

        public void Register(RegisterDTO registerDTO)
        {
            User user = new User
            {
                Name = registerDTO.Name,
                Surname = registerDTO.Surname,
                Email = registerDTO.Email,
                Password = CryptoPassword.GetSha(registerDTO.Password),
                UpdatedDate = null,
                ConfirmationKey = CryptoPassword.GetSha(registerDTO.Email),
                IsActive = false
            };
            _context.Users.Add(user);
            _context.SaveChanges();


            string bodyHtml = $"<html> <head> <body>  Üyeliğiniz aktif olması için <a href=http://localhost:5000/api/User/userActivate?confirmationKey={user.ConfirmationKey}&mail={user.Email}> bu </a> linke tıklayınız  </body>  </head>  </html> ";
            string subject = "USER-ACTIVATION";
            this.sendEmail(bodyHtml, subject, user.Email);
        }

        public void sendEmail(string bodyHtml, string subject, string email)
        {

            MailHelper.Send(bodyHtml, subject, email);

        }

        public string Login(LoginDTO loginDTO)
        {
            var cryptoPassword = CryptoPassword.GetSha(loginDTO.Password);
            var user = _context.Users.FirstOrDefault(x => x.Email == loginDTO.Email && x.Password == cryptoPassword);
            if (user == null)
                throw new ServiceException(ExceptionMessages.USER_NOT_FOUND);
            if(!user.IsActive)
                throw new ServiceException(ExceptionMessages.USER_NOT_ACTIVE);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = this._configuration.GetValue<string>("Token_Key");
            var tokenKeyByte = Encoding.ASCII.GetBytes(tokenKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim("Name",user.Name),
                    new Claim("Surname",user.Surname),
                    new Claim("Email",user.Email),
                    new Claim("Id", Convert.ToString(user.Id) ),

                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKeyByte), SecurityAlgorithms.HmacSha512)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            return token;

        }

        public void LoginValidation(LoginDTO loginDTO)
        {
            if (string.IsNullOrEmpty(loginDTO.Email))
                throw new ServiceException(ExceptionMessages.EMAIL_CANNOT_BE_BLANK);

            if (string.IsNullOrEmpty(loginDTO.Password))
                throw new ServiceException(ExceptionMessages.PASSWORD_CANNOT_BE_BLANK);
        }

        public bool UserActivate(string confirmationKey, string mail)
        {
            var user = _context.Users.FirstOrDefault(x => x.ConfirmationKey == confirmationKey && x.Email == mail);

            if (user != null)
            {
                user.IsActive = true;
                var result = _context.Users.Update(user);
                var result2 = _context.SaveChanges();
                return true;
            }

            throw new ServiceException("Onay kodu geçerli değil");


        }
    }
}
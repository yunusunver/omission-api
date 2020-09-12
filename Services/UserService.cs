using System;
using omission.api.Context;
using omission.api.Exceptions;
using omission.api.Models;
using omission.api.Utility;
using omission.api.Utility.Crypto;
using omission.api.Utility.Mail;
using System.Linq;

namespace omission.api.Services
{
    public class UserService
    {
        private OmissionContext _context;

        public UserService(OmissionContext context)
        {
            _context = context;
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
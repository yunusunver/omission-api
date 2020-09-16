namespace omission.api.Utility
{
    public static class ExceptionMessages
    {

        #region USER MESSAGES
        public const string NAME_CANNOT_BE_BLANK = "İsim boş bırakılamaz";
        public const string SURNAME_CANNOT_BE_BLANK = "Soyisim boş bırakılamaz";
        public const string EMAIL_CANNOT_BE_BLANK = "Email boş bırakılamaz";
        public const string PASSWORD_CANNOT_BE_BLANK = "Şifre boş bırakılamaz";
        public const string REPASSWORD_CANNOT_BE_BLANK = "Şifre tekrar boş bırakılamaz";
        public const string PASSWORDS_NOT_MATCHES = "Şifre alanları eşleşmiyor.";

        public const string USER_NOT_FOUND = "Böyle bir kullanıcı bulunamadı";
        public const string USER_NOT_ACTIVE = "Bu kullanıcı aktif değil.";


        #endregion



        #region CODE MESSAGES

        public const string CODENAME_CANNOT_BE_BLANK = "Kod tanımlayıcısı boş bırakılamaz";

        public const string CODELANGUAGE_CANNOT_BE_BLANK = "Programlama dili boş bırakılamaz";

        public const string CODEBODY_CANNOT_BE_BLANK = "Kod içeriği boş bırakılamaz";
        public static string CODE_NOT_FOUND = "Kod bulunamadı";

        public static string CODEID_NOT_AVAILABLE = "Kod Idsi 0 dan büyük olmalıdır.";

        #endregion

        #region LOOKUP MESSAGES
        public static string TYPE_CANNOT_BE_BLANK = "Type alanı boş bırakılamaz";

        public static string LOOKUP_ID_NOT_AVAILABLE = "Kod Idsi 0 dan büyük olmalıdır.";
        public static string LOOKUP_NOT_FOUND = "Böyle bir kayıt bulunamadı";

        public static string LOOKUP_NAME_CANNOT_BE_BLANK = "Ad alanı boş bırakılamaz";

        #endregion

        #region  HASHTAG MESSAGES 
        public static string HASHTAG_ID_NOT_AVAILABLE = "Kod Idsi 0 dan büyük olmalıdır.";
        public static string HASHTAG_NOT_FOUND = "Böyle bir kayıt bulunamadı";

        public static string HASHTAG_NAME_CANNOT_BE_BLANK = "Ad alanı boş bırakılamaz";

        #endregion
    }

}
namespace SchoolManagementSystem.Helpers
{


    public static class PersianNumberHelper
    {
        public static string ToPersianNumber(this int number)
        {
            string englishNumber = number.ToString();

            string[] englishDigits =
            {
                "0","1","2","3","4","5","6","7","8","9"
            };

            string[] persianDigits =
            {
                "۰","۱","۲","۳","۴","۵","۶","۷","۸","۹"
            };

            for (int i = 0; i < englishDigits.Length; i++)
            {
                englishNumber = englishNumber.Replace(
                    englishDigits[i],
                    persianDigits[i]);
            }

            return englishNumber;
        }

        public static string ToPersianNumber(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            string[] englishDigits =
            {
        "0","1","2","3","4","5","6","7","8","9"
    };

            string[] persianDigits =
            {
        "۰","۱","۲","۳","۴","۵","۶","۷","۸","۹"
    };

            for (int i = 0; i < englishDigits.Length; i++)
            {
                text = text.Replace(englishDigits[i], persianDigits[i]);
            }

            return text;
        }
    
}
}



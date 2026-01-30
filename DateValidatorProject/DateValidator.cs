using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateValidatorProject
{
    public class DateValidator
    {
        public string DateToValidate { get; set; }

        public DateValidator(string date)
        {
            DateToValidate = date;
        }

        public bool ValidateStringDate()
        {
            return CheckNumberOfDashes()
                && CheckAllNumerics()
                && CheckNumberBetween(1, 31, 0)
                && CheckNumberBetween(1, 12, 1)
                && CheckNumberBetween(2000, DateTime.Now.Year, 2);
        }

        public bool CheckNumberOfDashes()
        {
            if (string.IsNullOrEmpty(DateToValidate))
                throw new ArgumentException("Date cannot be null or empty string.");

            var splitted = DateToValidate.Split('-');

            if (splitted.Length != 3)
                return false;

            return true;
        }

        public bool CheckAllNumerics()
        {
            if (!CheckNumberOfDashes())
                return false;

            var splitted = DateToValidate.Split("-");

            return int.TryParse(splitted[0], out _)
                && int.TryParse(splitted[1], out _)
                && int.TryParse(splitted[2], out _);
        }

        public bool CheckNumberBetween(int min, int max, int position)
        {
            if (!CheckAllNumerics())
                return false;

            var splitted = DateToValidate.Split("-");

            return Convert.ToInt32(splitted[position]) >= min
                && Convert.ToInt32(splitted[position]) <= max;
        }
    }
}

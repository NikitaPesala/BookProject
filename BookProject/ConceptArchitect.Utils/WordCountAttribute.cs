using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.Utils
{
    public class WordCountAttribute : ValidationAttribute
    {

        int min, max;

        public WordCountAttribute(int min=1, int max=0)
        {
            this.min = min;
            this.max = max;
            if (this.max > 0)
                ErrorMessage = $"Word Count should be {min} - {max}";
            else
                ErrorMessage = $"Word count should be at least {min}";
        }


        /// <summary>
        /// We have to validate a value and return true (valid) or false (invalid)
        /// </summary>
        /// <param name="value">Is the value of property being validated.</param>
        /// <returns>if valid true else false</returns>
        public override bool IsValid(object? value)
        {
            if (!(value is string))
                return true; //I don't care. return true if it is out of syllabus.

            var words = value.ToString().Split(" ").Length;

            if (words < min)
                return false;
            if (max > 0 && words > max)
                return false;

            return true;

        }
    }
}

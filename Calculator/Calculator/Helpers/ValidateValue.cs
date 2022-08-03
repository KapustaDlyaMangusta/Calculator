using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Helpers
{
    public static class ValidateValue
    {
        public static bool Validate(string value)
        {
            try
            {
                Convert.ToDecimal(value);

            }
            catch
            {
                return false;
            }
            return true;
           
        }
    }
}

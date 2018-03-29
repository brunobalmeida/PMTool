using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace PmToolClassLibrary
{
    public class Validations : ValidationAttribute
    {
        /// <summary>
        /// Method used to Capitalize a string 
        /// </summary>
        /// <param name="tempString">string parameter passed to be capitalized</param>
        /// <returns>Returns a capitalized string</returns>
        public static string Capitalize(string tempString)
        {
            if (tempString == null || tempString == "")
            {
                return "";
            }

            tempString = tempString.ToLower().Trim();

            string[] stringArray = tempString.Split();
            string finalString = "";
            for (int i = 0; i < stringArray.Length; i++)
            {
                string aux2 = stringArray[i];
                finalString += char.ToUpper(aux2[0]) + aux2.Substring(1) + " ";
            }

            finalString = finalString.Trim();

            return finalString;

        }

        /// <summary>
        ///Boolean method to validate postal code following the Canadian Postal Code pattern
        /// </summary>
        /// <param name="postalCode">Parameter passed by reference to be evaluated and formatted</param>
        /// <returns>Returns a bool validating or not the postal code passed</returns>
        public static bool PostalCodeValidation(ref string postalCode)
        {
            if (postalCode == null || postalCode.ToString() == "")
            {
                return true;
            }

            if (postalCode.Length > 7)
            {
                return false;
            }

            string tempString;
            Regex pattern = new Regex(@"[ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVXY | abceghjklmnprstvwxyz] ?\d[ABCEGHJKLMNPRSTVXY]\d$", RegexOptions.IgnoreCase);

            if (pattern.IsMatch(postalCode.ToString()))
            {
                tempString = postalCode;
                postalCode = postalCode.ToUpper();
                if (tempString.ToCharArray().Length == 6)
                {
                    postalCode = postalCode.Insert(3, " ");
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Boolean method to validate postal code following the US Postal Code pattern
        /// </summary>
        /// <param name="postalCode">Parameter passed by reference to be evaluated and formatted</param>
        /// <returns>Returns a bool validating or not the postal code passed</returns>
        public static bool ZipCodeValidation(ref string postalCode)
        {
            if (postalCode == null || postalCode == "")
            {
                return true;
            }

            string tempString = postalCode;
            string auxString = "";
            bool digit;
            int count = 0;

            for (int i = 0; i < tempString.Length; i++)
            {
                digit = char.IsDigit(tempString[i]);
                if (digit)
                {
                    auxString += tempString[i];
                    count++;
                }
            }
            if (count != 5 && count != 9)
            {
                return false;
            }
            else if (count == 5)
            {
                postalCode = auxString;
                return true;
            }
            else if (count == 9)
            {
                postalCode = auxString.Insert(5, "-");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Phone validation method to validate and format a phone number 
        /// </summary>
        /// <param name="phoneNumber">String parameter passed by reference</param>
        /// <returns>Returns a bool validating the length of a phone number</returns>
        public static bool PhoneValidation(ref string phoneNumber)
        {
            if (phoneNumber == null || phoneNumber == "")
            {
                return true;
            }

            string tempString = phoneNumber;
            string auxString = "";
            bool digit;
            int count = 0;

            for (int i = 0; i < tempString.Length; i++)
            {
                digit = char.IsDigit(tempString[i]);
                if (digit)
                {
                    auxString += tempString[i];
                    count++;
                }
            }

            if (count == 10)
            {
                auxString = auxString.Insert(3, "-");
                auxString = auxString.Insert(7, "-");
                phoneNumber = auxString;
                return true;
            }

            return false;
        }

        public static bool EmailValidation(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }





    }
}

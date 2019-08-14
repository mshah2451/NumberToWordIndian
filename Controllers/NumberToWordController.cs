using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace interviewAssingement.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class NumberToWordController : ControllerBase {
        [HttpGet ("{number}")]
        public ActionResult<string> Get (int number) {
            return words (number);
        }
        private string words (int? numbers, Boolean paisaconversion = false) {
            var pointindex = numbers.ToString ().IndexOf (".");
            var paisaamt = 0;
            if (pointindex > 0)
                paisaamt = Convert.ToInt32 (numbers.ToString ().Substring (pointindex + 1, 2));

            int number = Convert.ToInt32 (numbers);

            if (number == 0)
                return "Zero";
            if (number == -2147483648)
                return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";
            int[] num = new int[4];
            int first = 0;
            int unit, h, tens;
            System.Text.StringBuilder stringbuilder = new System.Text.StringBuilder ();
            if (number < 0) {
                stringbuilder.Append ("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs
            for (int i = 3; i > 0; i--) {
                if (num[i] != 0) {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--) {
                if (num[i] == 0) continue;
                unit = num[i] % 10; // ones
                tens = num[i] / 10;
                h = num[i] / 100; // hundreds
                tens = tens - 10 * h; // tens
                if (h > 0) stringbuilder.Append (words0[h] + "Hundred ");
                if (unit > 0 || tens > 0) {
                    if (h > 0 && i != 0) stringbuilder.Append ("and ");
                    if (tens == 0)
                        stringbuilder.Append (words0[unit]);
                    else if (tens == 1)
                        stringbuilder.Append (words1[unit]);
                    else
                        stringbuilder.Append (words2[tens - 2] + words0[unit]);
                }
                if (i != 0) stringbuilder.Append (words3[i - 1]);
            }

            if (paisaamt == 0 && paisaconversion == false) {
                stringbuilder.Append ("ruppes only");
            } else if (paisaamt > 0) {
                var paisatext = words (paisaamt, true);
                stringbuilder.AppendFormat ("rupees {0} paise only", paisatext);
            }
            return stringbuilder.ToString ().TrimEnd ();
        }
    }
}
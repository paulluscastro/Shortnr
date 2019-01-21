using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortnr.Api.Utils
{
    public static class ShortenedUrlGenerator
    {
        /*
         * chars values
         * private static readonly int val0 = '0'; // 48
         * private static readonly int val9 = '9'; // 57
         * private static readonly int valA = 'A'; // 65
         * private static readonly int valZ = 'Z'; // 90
         * private static readonly int vala = 'a'; // 97
         * private static readonly int valz = 'z'; // 122
         */

        public static string GenerateNext(string current)
        {
            if (string.IsNullOrEmpty(current)) return "0";
            int[] numbers = current.Select(x => (int)x).ToArray();
            int last = numbers.Last();
            switch (last)
            {
                case '9':
                    numbers[numbers.Length - 1] = 'A';
                    break;
                case 'Z':
                    numbers[numbers.Length - 1] = 'a';
                    break;
                default:
                    int position = numbers.Length - 1;
                    numbers[position]++;
                    while (position > 0 && numbers[position] > 'z')
                    {
                        numbers[position] = '0';
                        numbers[position - 1]++;
                        position--;
                    }
                    if (numbers[0] > 'z')
                    {
                        numbers[0] = '0';
                        return '0' + string.Join("", numbers.Select(n => (char)n).ToArray());
                    }
                    break;
            }
            return string.Join("", numbers.Select(n => (char)n).ToArray());
        }
    }
}

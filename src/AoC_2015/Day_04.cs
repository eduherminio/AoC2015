using AoCHelper;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AoC_2015
{
    public class Day_04 : BaseDay
    {
        private readonly string _input = "iwrupvqb";

        /// <summary>
        /// Not thread safe, avoid sharing it if a multi-thread solution is ever implemented
        /// </summary>
        private static readonly MD5 Md5 = MD5.Create();

        public override string Solve_1()
        {
            string hash;
            for (int i = 0; ; ++i)
            {
                hash = CreateMD5(_input + i.ToString(), 5);

                if (hash.StartsWith("00000"))
                {
                    return i.ToString();
                }
            }
        }

        public override string Solve_2()
        {
            string hash;
            for (int i = 0; ; ++i)
            {
                hash = CreateMD5(_input + i.ToString(), 6);

                if (hash.StartsWith("000000"))
                {
                    return i.ToString();
                }
            }
        }

        private static string CreateMD5(string input, int numberOfCharsToCheck)
        {
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = Md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));

                // 'Dirty' optimization
                if (sb.Length >= numberOfCharsToCheck)
                {
                    return sb.ToString();
                }
            }
            return sb.ToString();
        }
    }
}

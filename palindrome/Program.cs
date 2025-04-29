using System;

namespace PalindromeChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            var palindrome1 = new PalindromeService("!@$");
            Console.WriteLine(palindrome1.IsPalindrome("a@b!!b$a")); // True

            var palindrome2 = new PalindromeService("#?");
            Console.WriteLine(palindrome2.IsPalindrome("?Aa#c"));    // False
        }
    }
}

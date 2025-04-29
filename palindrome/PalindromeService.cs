using System;
using System.Collections.Generic;

namespace PalindromeChecker
{
    // Encapsulates palindrome checking logic
    public class PalindromeService
    {
        private readonly HashSet<char> _trashSymbols;

        public PalindromeService(string trashSymbols)
        {
            if (trashSymbols == null) throw new ArgumentNullException(nameof(trashSymbols));
            _trashSymbols = new HashSet<char>(trashSymbols);
        }

        // Checks if the input string is a palindrome, ignoring trash symbols and case
        public bool IsPalindrome(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            int left = 0;
            int right = input.Length - 1;

            while (left < right)
            {
                while (left < right && _trashSymbols.Contains(input[left]))
                    left++;

                while (left < right && _trashSymbols.Contains(input[right]))
                    right--;

                if (left < right)
                {
                    if (char.ToLowerInvariant(input[left]) != char.ToLowerInvariant(input[right]))
                        return false;

                    left++;
                    right--;
                }
            }

            return true;
        }
    }
}

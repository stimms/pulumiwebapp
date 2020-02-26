using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace pulumiapp.Pages
{

    public class Prime
    {
        public int Number { get; set; }
        public bool IsMersenne { get; set; }
        public bool IsFibonacciPrime { get; set; }
    }
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Prime> Primes;
        public void OnGet(int countTo = 500)
        {

            var numbers = new bool[countTo];
            for (int i = 2; i < Math.Ceiling(Math.Sqrt(countTo)); i++)
            {
                var j = 2 * i;
                while (j < countTo)
                {
                    numbers[j] = true;
                    j += i;
                }
            }
            Primes = CleanPrimes(numbers).Select(x => new Prime
            {
                Number = x,
                IsFibonacciPrime = IsFibonacciPrime(x),
                IsMersenne = IsMersennePrime(x)
            });
            if (Primes.Count() < 30)
                throw new Exception("Too few answers!");

        }

        private bool IsMersennePrime(int number)
        {
            return ((number + 1) & number) == 0;
        }

        private bool IsFibonacciPrime(int number)
        {
            double fib = 0;
            int i = 0;
            bool isFib = false;
            while (fib <= number)
            {
                fib = 1 / Math.Sqrt(5) * (Math.Pow(((1 + Math.Sqrt(5)) / 2), i) - Math.Pow(((1 - Math.Sqrt(5)) / 2), i));
                if ((int)fib == number)
                    isFib = true;
                i++;
            }
            return isFib;
        }

        private IEnumerable<int> CleanPrimes(bool[] numbers)
        {
            return numbers.Select((x, i) => new { index = i, value = x }).Where(x => !x.value).Select(y => y.index).Where(x => x > 2);
        }
    }
}

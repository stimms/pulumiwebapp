using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Xunit;

namespace pulumiapp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IEnumerable<int> Primes;
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
            Primes = numbers.Select((x, i) => new { index = i, value = x }).Where(x => !x.value).Select(y => y.index).Where(x => x > 2);
            try
            {
                Assert.True(Primes.Count() > 30);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Too few primes returned");
                throw;
            }
        }
    }
}

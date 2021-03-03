using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace async_examples
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            // Call the first task and wait.  This task takes about 15.64 seconds to run
            var numbers = await SumArtificialNumbersAsync();
            // Then call the second task and wait.  This task takes about 4.08 seconds to run
            var words = await ConcatenateArtificialStringsAsync();

            Console.WriteLine("This is using async/await, but calling them one at a time (synchronous)");
            Console.WriteLine("Time elapsed for both completed:  " + stopwatch.Elapsed);
            Console.WriteLine("Numbers:  " + numbers);
            Console.WriteLine("Words: " + words);

            Console.WriteLine("--------------------------------------------------------------");

            var stopwatch2 = new Stopwatch();
            stopwatch2.Start();

            var numbersTwo = SumArtificialNumbersAsync();
            var wordsTwo = ConcatenateArtificialStringsAsync();

            // Both of the above tasks will complete in about 15.63 seconds, the length of time that it takes the longest task.
            await Task.WhenAll(numbersTwo, wordsTwo);

            Console.WriteLine("This is using async/await too, but calling them all at the same time (asynchronous)");
            Console.WriteLine("Time elapsed for both completed:  " + stopwatch2.Elapsed);
            Console.WriteLine("Numbers:  " + numbersTwo.Result);
            Console.WriteLine("Words: " + wordsTwo.Result);


            Console.Read();
        }

        private static async Task<int> SumArtificialNumbersAsync()
        {
            var sum = 0;
            foreach (var counter in Enumerable.Range(0, 100))
            {
                sum += counter;

                // ! artificial delay
                await Task.Delay(150);
            }

            return sum;
        }

        private static async Task<string> ConcatenateArtificialStringsAsync()
        {
            var word = string.Empty;
            foreach (var counter in Enumerable.Range(65, 26))
            {
                word = string.Concat(word, (char) counter);

                // ! artificial delay
                await Task.Delay(150);
            }

            return word;
        }
    }
}

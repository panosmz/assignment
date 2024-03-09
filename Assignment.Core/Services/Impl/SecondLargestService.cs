using Assignment.Core.Helpers;

namespace Assignment.Core.Services.Impl
{
    public class SecondLargestService : ISecondLargestService
    {
        public DataOrError<int?> FindSecondLargest(IEnumerable<int> numbers)
        {
            if (numbers == null || !numbers.Any())
                return new DataOrError<int?>("Array is empty.");

            var numbersDistinct = numbers.Distinct();

            if (numbersDistinct.Count() == 1)
                return new DataOrError<int?>("Array contains the same number.");

            int? secondLargest = numbersDistinct.OrderByDescending(n => n).Skip(1).FirstOrDefault();

            if (secondLargest == null)
                return new DataOrError<int?>("There is no second largest element in the array.");

            return new DataOrError<int?>(secondLargest);
        }
    }
}

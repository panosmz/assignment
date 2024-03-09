using Assignment.Core.Helpers;

namespace Assignment.Core.Services
{
    public interface ISecondLargestService
    {
        DataOrError<int?> FindSecondLargest(IEnumerable<int> numbers);
    }
}

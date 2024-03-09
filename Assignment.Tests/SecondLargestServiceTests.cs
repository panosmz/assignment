using Assignment.Core.Services.Impl;

namespace Assignment.Tests
{
    public class SecondLargestServiceTests
    {
        [Fact]
        public void FindSecondLargest_EmptyArray_ReturnsError()
        {
            // Arrange
            var service = new SecondLargestService();
            var emptyArray = new List<int>();

            // Act
            var result = service.FindSecondLargest(emptyArray);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal("Array is empty.", result.Error);
        }

        [Fact]
        public void FindSecondLargest_NoSecondLargest_ReturnsError()
        {
            // Arrange
            var service = new SecondLargestService();
            var numbers = new List<int> { 5, 5, 5, 5 }; // Same numbers

            // Act
            var result = service.FindSecondLargest(numbers);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal("Array contains the same number.", result.Error);
        }

        [Fact]
        public void FindSecondLargest_ReturnsSecondLargest()
        {
            // Arrange
            var service = new SecondLargestService();
            var numbers = new List<int> { 1, 3, 2, 4, 5 };

            // Act
            var result = service.FindSecondLargest(numbers);

            // Assert
            Assert.True(result.IsData);
            Assert.Equal(4, result.Data);
        }

        [Fact]
        public void FindSecondLargest_DuplicateLargest_ReturnsSecondLargest()
        {
            // Arrange
            var service = new SecondLargestService();
            var numbers = new List<int> { 1, 3, 2, 4, 5, 5 }; // Duplicate largest

            // Act
            var result = service.FindSecondLargest(numbers);

            // Assert
            Assert.True(result.IsData);
            Assert.Equal(4, result.Data);
        }
    }
}
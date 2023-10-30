using FluentAssertions;

namespace TranslationManagement.Api
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var a = 2;
            var b = 5;

            // Act
            var result = a + b;

            // Assert
            result.Should().Be(7);
        }
    }
}

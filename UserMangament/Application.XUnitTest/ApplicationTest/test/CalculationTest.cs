using Xunit;

namespace Application.XUnitTest.ApplicationTest.test
{
    public class CalculationTest
    {
        [Fact]
        public void FiboNotIncludZero()
        {
            var cal = new Calculation();
            Assert.All(cal.FibonNamber, n => Assert.NotEqual(1, n));
        }

        [Fact]
        public void FiboInclud13()
        {
            var cal = new Calculation();
            Assert.Contains(13, cal.FibonNamber);
        }

        [Fact]
        public void FiboIncludDoesNotInclud5()
        {
            var cal = new Calculation();
            Assert.DoesNotContain(5, cal.FibonNamber);
        }

        [Fact]
        public void CheckCalculation()
        {
            var expectedCollection = new List<int> { 1, 1, 2, 3, 5, 8, 13 };

            var cal = new Calculation();
            Assert.Equal(expectedCollection, cal.FibonNamber);

        }



    }


}

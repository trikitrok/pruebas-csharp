using NUnit.Framework;

namespace Portfolio.Test
{
    public class PortfolioTest
    {
        [Test]
        public void fix_me()
        {
            var app = new Portfolio("../../../portfolio.csv");

            app.ComputePortfolioValue();

            Assert.That("fixme", Is.EqualTo("fixme"));
        }

    }
}
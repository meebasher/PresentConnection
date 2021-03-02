using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Supplier.Console.Services;
using Supplier.REST;
using System;

namespace Invoices.Tests
{
    [TestClass]
    public class VatRequesterTests
    {
        private IVATRequester _vATRequester;
        private IRequsetRestAgent _requestAgent;

        [TestInitialize]
        public void Setup()
        {
            _requestAgent = Substitute.For<IRequsetRestAgent>();
            _vATRequester = new VATRequester(_requestAgent);
        }

        [TestMethod]
        public void Shuold_Throw_Error_When_Countyr_Code_Is_Not_ToLetters()
        {
            var InvalidCountryCode = "L";

            Assert.ThrowsException<ArgumentException>(() => _vATRequester.GetEUCountryVAT(InvalidCountryCode));
        }
    }
}

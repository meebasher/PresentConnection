using Invoices.Model.Entities.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Supplier.Console.Helpers;
using Supplier.Console.Services;
using System.Collections.Generic;
using System.Linq;

namespace Invoices.Tests
{
    [TestClass]
    public class EUCountryCheckerTests
    {
        private IEUCountryChecker _eUCountryChecker;
        private IEUCountryRequester _euCountryRequester;
        private IEnumerable<Country> _euCountries;

        [TestInitialize]
        public void Setup()
        {
            _euCountries = new List<Country> { new Country { CountryName = "Lithuania", Alpha2Code = "lt" } };
            _euCountryRequester = Substitute.For<IEUCountryRequester>();
            _euCountryRequester.EUCountries.Returns(_euCountries);
            _eUCountryChecker = new EUCountryChecker(_euCountryRequester);

        }

        [TestMethod]
        public void Shuold_Return_False_When_Not_Eu_Country()
        {
            var euCountry = "America";

            var result = _eUCountryChecker.IsEUCoutry(euCountry);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Shuold_Return_True_When_Is_Eu_Country()
        {
            var euCountry = _euCountries.First().CountryName;

            var result = _eUCountryChecker.IsEUCoutry(euCountry);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Should_Return_Alpha2Code_From_EU_Country_Name_When_Valid_Country()
        {
            var euCountry = _euCountries.First();

            var result = _eUCountryChecker.GetEUCoutryAlpha2Code(euCountry.CountryName);

            Assert.AreEqual(result, euCountry.Alpha2Code);
        }

        [TestMethod]
        public void Should_Return_Empty_String_When_Not_Valid_Country_Name()
        {
            var invalidCountry = "InvalidName";
            var expectedResult = string.Empty;

            var result = _eUCountryChecker.GetEUCoutryAlpha2Code(invalidCountry);

            Assert.AreEqual(result, expectedResult);
        }
    }
}

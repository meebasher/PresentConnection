using Invoices.Model.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Supplier.Console.Helpers;
using Supplier.Console.Services;

namespace Invoices.Tests
{
    [TestClass]
    public class InvoiceGeneratorTests
    {
        private IPerson _supplier;
        private IPerson _customer;
        private IInvoiceGenerator _invoiceGenerator;
        private IVATRequester _vATReuester;
        private IEUCountryRequester _eUCountryRequester;
        private IEUCountryChecker _eUCountryChecker;

        //     var leaglSupplier = SupplierFactory.GetLegalSupplier("Mario LTD", "Lithuania", false);
        //    var legalPayerCustomer = CustomerFactory.GetLegalCustomer("Gando", "Poland", true);
        //var legalNonPayerCustomer = CustomerFactory.GetLegalCustomer("Monolodia", "Italy", false);
        //var privatePayerCustomer = CustomerFactory.GetLegalCustomer("Indorum", "Germany", true);
        //var privatelNonPayerCustomer = CustomerFactory.GetLegalCustomer("La Portum", "France", false);
        [TestInitialize]
        public void Setup()
        {
            _supplier = Substitute.For<IPerson>();
            _supplier.IsVatPayer.Returns(true);
            _supplier.Country.Returns("Lithuania");

            _customer = Substitute.For<IPerson>();

            _eUCountryRequester = Substitute.For<IEUCountryRequester>();
            _eUCountryChecker = Substitute.For<IEUCountryChecker>();
            _vATReuester = Substitute.For<IVATRequester>();
            _invoiceGenerator = new InvoiceGenerator(_vATReuester, _eUCountryChecker);

        }

        [TestMethod]
        public void Sould_Return_Zero_If_Supplier_Is_Not_VAT_Payer()
        {
            var expectedResult = 0;
            _supplier.IsVatPayer.Returns(false);

            var result = _invoiceGenerator.GenerateInvoiceVAT(_supplier, _customer);

            Assert.AreEqual(result, expectedResult);
        }

        [TestMethod]
        public void Sould_Return_Countiry_VAT_If_Supplier_Is_VAT_Payer_And_Customer_Not_VAT_Payer_And_Customer_Not_In_EU_Region_And_Supplier_In_Different_Country()
        {
            var expectedResult = 21;
            _customer.IsVatPayer.Returns(false);
            _eUCountryChecker.IsEUCoutry(_customer.Country).Returns(true);
            _eUCountryChecker.GetEUCoutryAlpha2Code(_customer.Country).Returns("lt");
            _vATReuester.GetEUCountryVAT(_eUCountryChecker.GetEUCoutryAlpha2Code(_customer.Country)).Returns(21);
            _invoiceGenerator = new InvoiceGenerator(_vATReuester, _eUCountryChecker);

            var result = _invoiceGenerator.GenerateInvoiceVAT(_supplier, _customer);

            Assert.AreEqual(result, expectedResult);
        }

        [TestMethod]
        public void Sould_Return_Zero_VAT_If_Supplier_Is_VAT_Payer_And_Customer_Is_VAT_Payer_And_Customer_Not_In_EU_Region_And_Supplier_In_Different_Country()
        {
            var expectedResult = 0;
            _customer.IsVatPayer.Returns(true);

            var result = _invoiceGenerator.GenerateInvoiceVAT(_supplier, _customer);

            Assert.AreEqual(result, expectedResult);
        }

        [TestMethod]
        public void Sould_Return_Country_VAT_If_Supplier_And_Cutomer_In_The_Same_Country()
        {
            var expectedResult = 21;
            _customer.Country.Returns("Lithuania");
            _eUCountryChecker.IsEUCoutry(_customer.Country).Returns(true);
            _eUCountryChecker.GetEUCoutryAlpha2Code(_customer.Country).Returns("lt");
            _vATReuester.GetEUCountryVAT(_eUCountryChecker.GetEUCoutryAlpha2Code(_customer.Country)).Returns(21);
            _invoiceGenerator = new InvoiceGenerator(_vATReuester, _eUCountryChecker);

            var result = _invoiceGenerator.GenerateInvoiceVAT(_supplier, _customer);

            Assert.AreEqual(result, expectedResult);
        }

    }
}

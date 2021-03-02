using Invoices.Model.Entities;
using Supplier.Console.Services;
using System;

namespace Supplier.Console.Helpers
{
    public class InvoiceGenerator : IInvoiceGenerator
    {
        private readonly IVATRequester _vATRequester;
        private readonly IEUCountryChecker _eUCountryChecker;
        public InvoiceGenerator(IVATRequester vATRequester, IEUCountryChecker eUCountryChecker)
        {
            _vATRequester = vATRequester ?? throw new ArgumentNullException(nameof(vATRequester));
            _eUCountryChecker = eUCountryChecker ?? throw new ArgumentNullException(nameof(eUCountryChecker));
        }

        public int GenerateInvoiceVAT(IPerson supplier, IPerson customer)
        {
            var isCustomerInSuppliersCountry = supplier.Country.ToLower() == customer.Country.ToLower();
            var isCustomerEuCitizen = _eUCountryChecker.IsEUCoutry(customer.Country);
            var isEUSupplierIsVATPayer = supplier.IsVatPayer && isCustomerEuCitizen;
            var alpha2Code = _eUCountryChecker.GetEUCoutryAlpha2Code(customer.Country);
            var customerCountryVAT = _vATRequester.GetEUCountryVAT(alpha2Code);

            if (!supplier.IsVatPayer)
            {
                return 0;
            }
            else if (supplier.IsVatPayer && !isCustomerEuCitizen)
            {
                return 0;
            }
            else if (isEUSupplierIsVATPayer && !customer.IsVatPayer && !isCustomerInSuppliersCountry)
            {
                return customerCountryVAT;
            }
            else if (isEUSupplierIsVATPayer && customer.IsVatPayer && !isCustomerInSuppliersCountry)
            {
                return 0;
            }
            else if (isEUSupplierIsVATPayer && isCustomerInSuppliersCountry)
            {
                return customerCountryVAT;
            }
            return 0;
        }
    }
}

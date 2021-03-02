namespace Supplier.Console.Services
{
    public interface IVATRequester
    {
        int GetEUCountryVAT(string countyCode);
    }
}
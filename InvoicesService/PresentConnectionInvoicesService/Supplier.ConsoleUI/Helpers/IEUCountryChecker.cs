namespace Supplier.Console.Helpers
{ 
    public interface IEUCountryChecker
    {
        string GetEUCoutryAlpha2Code(string countryName);
        bool IsEUCoutry(string countryName);
    }
}
namespace Ordering.Domain.Models;

public class Address(string city, string district, string street, string home, string tel, string location) : ValueObject
{
    public string Home { get; private set; } = home;
    public string Street { get; private set; } = street;
    public string District { get; private set; } = district;
    public string City { get; private set; } = city;
    public string Tel { get; private set; } = tel;
    public string Location { get; private set; } = location;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        // Using a yield return statement to return each element one at a time
        yield return Street;
        yield return District;
        yield return Home;
        yield return Location;
        yield return Tel;
        yield return City;
    }
}

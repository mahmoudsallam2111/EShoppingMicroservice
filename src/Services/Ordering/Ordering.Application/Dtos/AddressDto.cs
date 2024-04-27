namespace Ordering.Application.Dtos;

public record AddressDto(string firstName,
    string LastName, 
    string EmailAddress, 
    string AddressLine,
    string Country, 
    string State, 
    string ZipCode);

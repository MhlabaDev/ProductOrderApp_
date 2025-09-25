using System.Collections.Generic;

namespace ProductOrderApi.DTOs
{
    /// <summary>
    /// DTO for transferring customer data
    /// Excludes navigation properties by default
    /// </summary>
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; } = "";
        public string Surname { get; set; } = "";
        public string AddressType { get; set; } = "";
        public string StreetAddress { get; set; } = "";
        public string Suburb { get; set; } = "";
        public string City { get; set; } = "";
        public string PostalCode { get; set; } = "";

     
        public ICollection<OrderDto>? Orders { get; set; }
    }
}

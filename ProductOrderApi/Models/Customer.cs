using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductOrderApi.Models
{
    public class Customer
{
    public int Id { get; set; }

    [Required, StringLength(50)]
    public string FirstName { get; set; } = "";

    [Required, StringLength(50)]
    public string Surname { get; set; } = "";

    [Required]
    public string AddressType { get; set; } = "";

    [Required, StringLength(200)]
    public string StreetAddress { get; set; } = "";

    [StringLength(100)]
    public string Suburb { get; set; } = "";

    [Required, StringLength(100)]
    public string City { get; set; } = "";

    [Required, StringLength(10)]
    public string PostalCode { get; set; } = "";

    [JsonIgnore] 
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

}

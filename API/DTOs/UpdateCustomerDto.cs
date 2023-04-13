namespace API.DTOs;

public class UpdateCustomerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}
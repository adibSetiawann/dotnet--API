namespace FinalProjectApplication
{
    public class CustomerListDto
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Gender { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public string Address { get; set; }
        // public Task<List<PetListDto>> Pet { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public int CodePosId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}

namespace FinalProjectApplication
{
    public class CreateCustomerDto
    {
        public string CustomerName { get; set; }
        public string PasswordHash { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string Email { get; set; }
        public int GenderId { get; set; }
        public string Address { get; set; }
        public int PetTypeId { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public int CodePosId { get; set; }
        
    }
}
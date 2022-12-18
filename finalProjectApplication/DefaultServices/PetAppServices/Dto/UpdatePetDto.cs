namespace FinalProjectApplication
{
    public class UpdatePetDto
    {
        public int PetId { get; set; }
        public string PetName { get; set; }
        public int GenderId { get; set; }
        public int PetTypeId { get; set; }
        public Guid CustomerId { get; set; }
    }
}

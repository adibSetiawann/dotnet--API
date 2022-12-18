namespace FinalProjectApplication
{
    public class CreatePetDto
    {
        public string PetName { get; set; }
        public int GenderId { get; set; }
        public int PetTypeId { get; set; }
        public Guid CustomerId { get; set; }
    }
}

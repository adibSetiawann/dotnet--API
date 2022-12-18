namespace FinalProjectApplication
{
    public class BillDetailListDto
    {
        public int BillId { get; set; }
        public Guid BillDetailId { get; set; }
        public string BranchOffice { get; set; }
        public string CutomerName { get; set; }
        public string DoctorName { get; set; }
        public string PetName { get; set; }
        public string PetType { get; set; }
        public string CareType { get; set; }
        public int CarePrice { get; set; }
        public string Status { get; set; }
    }
}

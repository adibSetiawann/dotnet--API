using System.Threading.Tasks;

namespace FinalProjectApplication
{
    public interface IDoctorAppService
    {
        Task<(bool, string)>Create(CreateDoctorDto model);
        Task<(bool, string)> Update(UpdateDoctorDto model);
        Task<(bool, string)> Delete(int id);
        Task<PageResult<DoctorListDto>> GetAllDoctors(PageInfo pageInfo);
        List<DoctorListDto> SearchDoctorById(int id);
        List<DoctorListDto> SearchDoctorName(string name);
        List<DoctorListDto> SearchDoctorByCity(int cityId);
        DoctorListDto SearchDoctorPhone(string phone, string password);
        Task<(bool, string)> ForgotPassword(string email);
        Task<(bool, string)> UpdatePassword(string password, string email);
    }
}

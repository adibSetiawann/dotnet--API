using System.Threading.Tasks;

namespace FinalProjectApplication
{
    public interface IPetAppService
    {
        Task<(bool, string)>Create(CreatePetDto model);
        Task<(bool, string)> Update(UpdatePetDto model);
        Task<(bool, string)> Delete(int id);
        Task<PageResult<PetListDto>> GetAllPets(PageInfo pageInfo);
        Task<List<PetListDto>> SearchPetByCustomer(Guid id);
        Task<List<PetListDto>> SearchPetByName(string name);
    }
}

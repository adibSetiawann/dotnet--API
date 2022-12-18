using System.Data.Common;
using AutoMapper;
using FinalProjectDB;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectApplication
{
    public class PetAppService : IPetAppService
    {
        private readonly PetCareContext _petCareContext;
        private IMapper _mapper;

        public PetAppService(PetCareContext PetCareContext, IMapper mapper)
        {
            _petCareContext = PetCareContext;
            _mapper = mapper;
        }

        public async Task<(bool, string)> Create(CreatePetDto model)
        {
            try
            {
                var Pet = _mapper.Map<Pet>(model);

                await _petCareContext.Database.BeginTransactionAsync();
                _petCareContext.Pet.Add(Pet);
                await _petCareContext.SaveChangesAsync();

                await _petCareContext.Database.CommitTransactionAsync();
                return await Task.Run(() => (true, "Created"));
            }
            catch (DbException dbex)
            {
                await _petCareContext.Database.RollbackTransactionAsync();
                return (false, dbex.Message);
            }
        }

        public async Task<(bool, string)> Delete(int id)
        {
            try
            {
                var Pet = await _petCareContext.Pet.FirstOrDefaultAsync(w => w.PetId == id);
                if (Pet != null)
                {
                    await _petCareContext.Database.BeginTransactionAsync();
                    _petCareContext.Pet.Remove(Pet);
                    await _petCareContext.SaveChangesAsync();
                    await _petCareContext.Database.CommitTransactionAsync();
                }
                return await Task.Run(() => (true, "Sucess"));
            }
            catch (DbException dbex)
            {
                await _petCareContext.Database.RollbackTransactionAsync();
                return await Task.Run(() => (false, dbex.Message));
            }
        }

        public async Task<PageResult<PetListDto>> GetAllPets(PageInfo pageInfo)
        {
            var pageResult = new PageResult<PetListDto> { Data = (
                    from Pet in _petCareContext.Pet
                    join customer in _petCareContext.Customer
                        on Pet.CustomerId equals customer.CustomerId
                    join gender in _petCareContext.Gender on Pet.GenderId equals gender.GenderId
                    join petType in _petCareContext.PetType
                        on Pet.PetTypeId equals petType.PetTypeId
                    select new PetListDto
                    {
                        PetId = Pet.PetId,
                        PetName = Pet.PetName,
                        Gender = gender.Description,
                        PetType = petType.Description,
                        CustomerName = customer.CustomerName
                    }
                ).Skip(pageInfo.Skip).Take(pageInfo.PageSize).OrderBy(w => w.PetId), Total = _petCareContext.Pet.Count() };
            return await Task.Run(() => pageResult);
        }

        public async Task<List<PetListDto>> SearchPetByCustomer(Guid id)
        {
            var customerData = _petCareContext.Customer.FirstOrDefault(w => w.CustomerId == id);
            var pageResult = (from Pet in _petCareContext.Pet
                    join customer in _petCareContext.Customer
                        on Pet.CustomerId equals customer.CustomerId
                    join gender in _petCareContext.Gender on Pet.GenderId equals gender.GenderId
                    join petType in _petCareContext.PetType
                        on Pet.PetTypeId equals petType.PetTypeId
                    where Pet.CustomerId == customerData.CustomerId
                    select new PetListDto
                    {
                        PetId = Pet.PetId,
                        PetName = Pet.PetName,
                        Gender = gender.Description,
                        PetType = petType.Description,
                        CustomerName = customer.CustomerName
                    }).ToList();
            
            return await Task.Run(() => pageResult);
        }

        public async Task<List<PetListDto>> SearchPetByName(string name)
        {
            var results = (
                from Pet in _petCareContext.Pet
                join customer in _petCareContext.Customer
                    on Pet.CustomerId equals customer.CustomerId
                join gender in _petCareContext.Gender on Pet.GenderId equals gender.GenderId
                join petType in _petCareContext.PetType on Pet.PetTypeId equals petType.PetTypeId
                where Pet.PetName.Contains($"{name}")
                select new PetListDto
                {
                    PetId = Pet.PetId,
                    PetName = Pet.PetName,
                    Gender = gender.Description,
                    PetType = petType.Description,
                    CustomerName = customer.CustomerName
                }
            ).ToList();
            return await Task.Run(() => results);
        }

        public async Task<(bool, string)> Update(UpdatePetDto model)
        {
            try
            {
                var Pet = _mapper.Map<Pet>(model);
                await _petCareContext.Database.BeginTransactionAsync();
                _petCareContext.Pet.Update(Pet);
                await _petCareContext.SaveChangesAsync();
                await _petCareContext.Database.CommitTransactionAsync();
                return await Task.Run(() => (true, "Success"));
            }
            catch (DbException dbex)
            {
                await _petCareContext.Database.RollbackTransactionAsync();
                return await Task.Run(() => (false, dbex.Message));
            }
        }
    }
}

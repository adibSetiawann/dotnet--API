using AutoMapper;
using finalProjectApplication.DefaultServices.CustomerAppServices.Dto;
using FinalProjectDB;

namespace FinalProjectApplication
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
            CreateMap<Doctor, CreateDoctorDto>();
            CreateMap<CreateDoctorDto, Doctor>();

            CreateMap<Doctor, UpdateDoctorDto>();
            CreateMap<UpdateDoctorDto, Doctor>();

            CreateMap<Doctor, DoctorListDto>();
            CreateMap<DoctorListDto, Doctor>();

            CreateMap<Customer, CreateCustomerDto>();
            CreateMap<CreateCustomerDto, Customer>();

            CreateMap<Customer, UpdateCustomerDto>();
            CreateMap<UpdateCustomerDto, Customer>();

            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();

            CreateMap<Bill, CreateBillDto>();
            CreateMap<CreateBillDto, Bill>();

            CreateMap<Bill, BillListDto>();
            CreateMap<BillListDto, Bill>();

            CreateMap<BillDetail, CreateBillDetailDto>();
            CreateMap<CreateBillDetailDto, BillDetail>();

            CreateMap<BillDetail, BillDetailListDto>();
            CreateMap<BillDetailListDto, BillDetail>();

            CreateMap<Pet, CreatePetDto>();
            CreateMap<CreatePetDto, Pet>();

            CreateMap<Pet, UpdatePetDto>();
            CreateMap<UpdatePetDto, Pet>();

            CreateMap<Pet, PetListDto>();
            CreateMap<PetListDto, Pet>();
        }
    }
}
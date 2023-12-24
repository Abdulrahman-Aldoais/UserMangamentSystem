using Application.Features.Departments.Dtos.Get;
using Application.Features.Departments.Dtos.GetList;
using Application.Repositories.DepartmentRepository;
using Application.Repositories.EmployeeRepositoty;
using Application.Repositories.UserRepository;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentReadRepository _departmentReadRepository;
        private readonly IDepartmentWriteRepository _departmentWriteRepositoty;
        public readonly IMapper _mapper;
        public DepartmentService(IDepartmentReadRepository departmentReadRepository, IMapper mapper, IDepartmentWriteRepository departmentWriteRepositoty)
        {
            _departmentReadRepository = departmentReadRepository;
            _mapper = mapper;
            _departmentWriteRepositoty = departmentWriteRepositoty;
        }
        public async Task<List<GetListDepartmentOutput>> GetAllDepartmentAsync(GetListDepartmentOutput department)
        {
            var allDepartment = _departmentReadRepository.GetAll();
            return await allDepartment.Select(x => new GetListDepartmentOutput
            {
                Id = x.Id,
                Name = x.Name,
                //CreatedBy = x.CreatedBy.HasValue ? x.CreatedBy.Value : 0,
                //CreatedDate = x.CreatedDate,
                //DeletedBy = x.DeletedBy.HasValue ? x.DeletedBy.Value : 0,
                //IsDeleted = x.IsDeleted,
                //ModifiedBy = x.ModifiedBy.HasValue ? x.ModifiedBy.Value : 0,
                //ModifiedDate = x.ModifiedDate.HasValue ? x.ModifiedDate.Value : DateTime.MinValue,

            }).ToListAsync();
        }

        public async Task<GetDepartmentOutput> GetDepartmentByIdAsync(int departmentId)
        {
            var getDepartmentInformation = await _departmentReadRepository.GetAsync(x => x.Id.Equals(departmentId));
            var mappDepartmentFromEintityToDto = _mapper.Map<GetDepartmentOutput>(getDepartmentInformation);
            return mappDepartmentFromEintityToDto;
        }

        public async Task<string> AddNewDepartment(Department department)
        {
            try
            {
                await _departmentWriteRepositoty.AddAsync(department);
                return "Success";
            }
            catch (Exception ex)
            {

                // Log the exception details, including inner exception
                Console.WriteLine("Exception message: " + ex.Message);
                Console.WriteLine("Stack trace: " + ex.StackTrace);

                // Check for inner exception
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception message: " + ex.InnerException.Message);
                    Console.WriteLine("Inner Exception stack trace: " + ex.InnerException.StackTrace);
                    // Log any additional information from


                }
                return "Failed";
            }
        }
        
        public async Task<string> UpdateDepartment(Department department)
        {
            try
            {
                await _departmentWriteRepositoty.UpdateAsync(department);
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
                throw;
            }
        }
    }
}

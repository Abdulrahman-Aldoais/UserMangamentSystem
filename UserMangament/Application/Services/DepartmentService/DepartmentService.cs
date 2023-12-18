using Application.Features.Departments.Dtos.Get;
using Application.Features.Departments.Dtos.GetList;
using Application.Repositories.DepartmentRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentReadRepository _departmentReadRepository;
        public readonly IMapper _mapper;
        public DepartmentService(IDepartmentReadRepository departmentReadRepository, IMapper mapper)
        {
            _departmentReadRepository = departmentReadRepository;
            _mapper = mapper;
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
    }
}

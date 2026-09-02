using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UserManagement.Dtos;
using UserManagement.entities;

namespace UserManagement.Services
{

    public interface ISchoolService
    {
        Task<SchoolResponse> Create(SchoolCreateDto schoolCreateDto);
        Task<IEnumerable<SchoolResponse>> GetAll();
        Task<SchoolResponse?> GetById(Guid id);
        Task<SchoolResponse?> Update(SchoolUpdateDto schoolUpdateDto);
        Task<String> Delete(Guid id);
    }

    public class SchoolService(AppDbContext dbContext) : ISchoolService
    {
        public async Task<SchoolResponse> Create(SchoolCreateDto schoolCreateDto)
        {
            SchoolEntity school = new()
            {
                id = Guid.CreateVersion7(),
                Title = schoolCreateDto.Title,
            };
            EntityEntry<SchoolEntity> createSchool = dbContext.School.Add(school);
            await dbContext.SaveChangesAsync();
            return new SchoolResponse
            {
                id = school.id,
                Title = school.Title,
            };
        }

        public async Task<IEnumerable<SchoolResponse>> GetAll()
        {
            List<SchoolResponse> list = await dbContext.School
                .Include(x => x.Classes)
                .Select(x => new SchoolResponse { id = x.id, Title = x.Title })
                .ToListAsync();
            return list;
        }

        public async Task<SchoolResponse?> GetById(Guid id)
        {
            SchoolEntity? school = await dbContext.School.FindAsync(id);
            if (school == null) return null;
            return new SchoolResponse { id = school.id, Title = school.Title };
        }

        public async Task<SchoolResponse?> Update(SchoolUpdateDto schoolUpdateDto)
        {
            SchoolEntity? school = await dbContext.School.FindAsync(schoolUpdateDto.id);
            if (school == null) return null;
            if(schoolUpdateDto.Title != null) school.Title = schoolUpdateDto.Title;
            await dbContext.SaveChangesAsync();

            return new SchoolResponse { id = school.id, Title = school.Title };
        }

        public async Task<string> Delete(Guid id)
        {
            SchoolEntity? school = await dbContext.School.FindAsync(id);
            if (school == null) return "Mission Not Complete";
            dbContext.School.Remove(school);
            return "Mission Complete";
        }
    }
}

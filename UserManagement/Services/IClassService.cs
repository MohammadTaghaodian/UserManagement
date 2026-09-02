using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using UserManagement.Dtos;
using UserManagement.entities;

namespace UserManagement.Services
{
    public interface IClassService
    {
        Task<ClassResponse> Create(ClassCreateDto classCreateDto);
        Task<IEnumerable<ClassResponse>> GetAll();
        Task<ClassResponse?> GetById(Guid id);
        Task<ClassResponse?> Update(ClassUpdateDto classUpdateDto);
        Task<IResult> Delete(Guid id);
    }
    public class ClassService(AppDbContext dbContext) : IClassService
    {
        public async Task<ClassResponse> Create(ClassCreateDto classCreateDto)
        {
            List<UserEntities> userList = new();

            if (classCreateDto.Users?.Any() == true)
            {
                userList = await dbContext.Users
                    .Where(u => classCreateDto.Users.Contains(u.id))
                    .ToListAsync();
            }
            SchoolEntity? school = await dbContext.School.FindAsync(classCreateDto.SchoolId);
            //Console.WriteLine(JsonSerializer.Serialize(school));

            ClassEntity e = new()
            {
                id = Guid.CreateVersion7(),
                Title = classCreateDto.Title,
                Subject = classCreateDto.Subject,
                SchoolId = classCreateDto.SchoolId,
                Users = userList
            };
            await dbContext.SaveChangesAsync();

            ClassEntity entity = dbContext.Class.Add(e).Entity;
            return new ClassResponse
            {
                id = entity.id,
                Title = entity.Title,
                Subject = entity.Subject,
                SchoolId = entity.SchoolId,
                School = school == null ? null : new SchoolResponse { id = school.id, Title = school.Title },
                Users = entity.Users.Select(x => new UserResponse
                {
                    id = x.id,
                    FullName = x.FullName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Birthdate = x.Birthdate,
                    IsMarried = x.IsMarried,
                })
            };
        }

        public async Task<IEnumerable<ClassResponse>> GetAll()
        {
            List<ClassResponse> list = await dbContext.Class
                .Select(x => new ClassResponse
                {
                    id = x.id,
                    Title = x.Title,
                    Subject = x.Subject,
                    School = x.School == null ? null : new SchoolResponse { id = x.School.id, Title = x.School.Title },
                    Users = x.Users.Select(u => new UserResponse
                    {
                        id = u.id,
                        FullName = u.FullName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        IsMarried = u.IsMarried,
                        Birthdate = u.Birthdate,
                    })
                }).ToListAsync();

            return list;
        }

        public async Task<ClassResponse?> GetById(Guid id)
        {
            ClassEntity? e = await dbContext.Class.FindAsync(id);
            return e == null ? null : new ClassResponse
            {
                id = e.id,
                Title = e.Title,
                Subject = e.Subject,
            };
        }

        public async Task<ClassResponse?> Update(ClassUpdateDto classUpdateDto)
        {
            ClassEntity? e = await dbContext.Class.FindAsync(classUpdateDto.id);
            if (e == null) return null;
            if (classUpdateDto.Title != null) e.Title = classUpdateDto.Title;
            if (classUpdateDto.Subject != null) e.Subject = classUpdateDto.Subject;
            if (classUpdateDto.Users?.Any() == true)
            {
                e.Users = await dbContext.Users
                    .Where(u => classUpdateDto.Users.Contains(u.id))
                    .ToListAsync();
            }
            if(classUpdateDto.SchoolId != null)
            {
                var school = await dbContext.School.FindAsync(classUpdateDto.SchoolId);
                e.School = school != null ? school : e.School;
            }
            await dbContext.SaveChangesAsync();

            return new ClassResponse
            {
                id = e.id,
                Title = e.Title,
                Subject = e.Subject,
                School = e.School == null ? null : new SchoolResponse { id = e.School.id, Title = e.School.Title },
                Users = e.Users.Select(x => new UserResponse
                {
                    id = x.id,
                    FullName = x.FullName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Birthdate = x.Birthdate,
                    IsMarried = x.IsMarried,
                })
            };
        }

        public async Task<IResult> Delete(Guid id)
        {
            var e = await dbContext.Class.FindAsync(id);
            if (e == null) return Results.NotFound();
            dbContext.Class.Remove(e);
            return Results.Ok(e);
        }

    }
}

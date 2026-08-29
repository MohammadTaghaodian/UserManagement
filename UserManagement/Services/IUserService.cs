using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using UserManagement.Dtos;
using UserManagement.entities;

namespace UserManagement.Services
{
    public interface IUserService
    {
        Task<UserResponse> Create(UserCreateDto user);
        Task<IEnumerable<UserResponse>> GetAll();
        Task<UserResponse?> GetById(Guid id);
        Task<UserResponse?> Update(UserUpdateDto user);
        Task<String> Delete(Guid id);
    }

    public class UserService(AppDbContext dbContext) : IUserService
    {
        public async Task<UserResponse> Create(UserCreateDto dto)
        {

            UserEntities user = new()
            {
                id = Guid.CreateVersion7(),
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Birthdate = dto.Birthdate,
                IsMarried = dto.IsMarried
            };
            UserEntities entity = dbContext.Users.Add(user).Entity;
            await dbContext.SaveChangesAsync();

            int? age = null;
            if (entity.Birthdate != null)
            {
                age = DateTime.UtcNow.Year - entity.Birthdate.Value.Year;
            }

            return new UserResponse
            {
                id = entity.id,
                FullName = entity.FullName,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email,
                Birthdate = entity.Birthdate,
                IsMarried = entity.IsMarried,
                Age = age
            };

        }
        
        public async Task<IEnumerable<UserResponse>> GetAll()
        {
            List<UserResponse> list = await dbContext.Users.Select(x => new UserResponse
            {
                id = x.id,
                Email = x.Email,
                FullName = x.FullName,
                PhoneNumber = x.PhoneNumber,
                Birthdate = x.Birthdate,
                IsMarried = x.IsMarried,
                Age = 7
            }).ToListAsync();
            return list;
        }

        public async Task<UserResponse?> GetById(Guid id)
        {
            UserEntities? user = await dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }

            int? age = null;
            if (user.Birthdate != null)
            {
                age = DateTime.UtcNow.Year - user.Birthdate.Value.Year;
            }

            UserResponse response = new()
            {
                id = user.id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Birthdate = user.Birthdate,
                IsMarried = user.IsMarried,
                Age = age
            };

            return response;
        }

        public async Task<UserResponse?> Update(UserUpdateDto dto)
        {
            UserEntities? user = await dbContext.Users.FindAsync(dto.id);
            if (user == null) return null;
            if (dto.IsMarried != null) user.IsMarried = dto.IsMarried.Value;
            if (dto.PhoneNumber != null) user.PhoneNumber = dto.PhoneNumber;
            if (dto.Birthdate != null) user.Birthdate = dto.Birthdate;
            if (dto.FullName != null) user.FullName = dto.FullName;
            if (dto.Email != null) user.Email = dto.Email;

            await dbContext.SaveChangesAsync();
            return new UserResponse
            {
                id = user.id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Birthdate = user.Birthdate,
                IsMarried = user.IsMarried,
            };
        }

        public async Task<String> Delete(Guid id)
        {
            await dbContext.Users.Where(x => x.id == id).ExecuteDeleteAsync();
            return "<Mission> Complete";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Models;
using App.Services.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace App.Services
{
    public class UserService
    {
        private MyDbContext _context;

        public UserService (MyDbContext context)
        {
            _context = context;
        }

        //FIND
        public async Task<List<User>> FindAllAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Email == email);
        }

        //UPDATE
        public async Task UpdateAsync(User obj)
        {
            var hasAny = await _context.User.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("id not found");
            }
            try
            {
                _context.User.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbUpdateException(e.Message);
            }
        }

        //REMOVE
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.User.FindAsync(id);
                _context.User.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
        ////////////////////////////////////////////////////
        //CREATE
        public async Task InsertAsync(User user)
        {
            if(user.Name == null || user.Email == null)
            {
                throw new IntegrityException("Nome e Email não podem estar vazios");
            }
            else if (user.Name.Length< 4 || user.Email.Length< 4)
            {
                throw new IntegrityException("Nome e Email não podem ter menos que 4 caracteres");
            }

            else if (user.ImgFile != null && user.ImgFile.Length > 0)
            {
                var cont = user.ImgFile.ContentType;
                if (cont == "image/png" || cont == "image/bmp" || cont == "image/jpeg")
                {
                    user.Image = ImgMethodsService.ImageIFormForBytetArray(user.ImgFile);
                    if(user.UserLevel == 0)
                    {
                        user.UserLevel = Models.Enums.UserLevel.Public;
                    }
                    _context.User.Add(user);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new IntegrityException("Nenhum arquivo do tipo bmp, jpg, png foi selecionado");
                }
            }
            else
            {
                throw new IntegrityException("Nenhum arquivo do tipo bmp, jpg, png foi selecionado");
            }
        }
    }
}

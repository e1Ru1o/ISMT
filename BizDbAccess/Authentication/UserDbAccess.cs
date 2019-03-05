using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizDbAccess.Authentication
{
    public class UserDbAccess : IEntityDbAccess<Usuario>
    {
        private readonly EfCoreContext _context;

        public UserDbAccess(IUnitOfWork context)
        {
            _context = (EfCoreContext)context;
        }

        public Usuario LoginUsuario(string email, string password)
        {
            return _context.Usuarios.Where(u => u.Email == email && u.Password == password).Single();
        }

        public Usuario GetUserByEmail(string email)
        {
            return _context.Usuarios.Where(u => u.Email == email).Single();
        }

        public void Add(Usuario entity)
        {
            _context.Usuarios.Add(entity);
        }

        public void Delete(Usuario entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _context.Usuarios.ToList();
        }

        public void Update(Usuario entity)
        {
            var user = _context.Usuarios.Find(entity.UsuarioID);
            if (user == null)
                throw new Exception("User to be updated no exist");

            //just for sure that the fields of the viewModel are not null
            //null-coalescing is used
            user.FirstName = entity.FirstName ?? user.FirstName; ;
            user.FirstLastName = entity.FirstLastName ?? user.FirstLastName;
            user.SecondName = entity.SecondName ?? user.SecondName;
            user.Email = entity.Email ?? user.Email;
            user.Password = entity.Password ?? user.Password;
        }
    }
}

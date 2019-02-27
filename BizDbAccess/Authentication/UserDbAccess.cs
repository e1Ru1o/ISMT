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
            return _context.Usuarios.Where(u => u.Email == email && u.Password == password).Select(u => new Usuario()).Single();
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
            throw new System.NotImplementedException();
        }

        public void Update(Usuario entity)
        {
            throw new System.NotImplementedException();
        }
    }
}

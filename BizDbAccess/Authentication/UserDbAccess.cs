using BizData.Entities;
using BizDbAccess.GenericInterfaces;
using DataLayer.EfCode;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizDbAccess.Authentication
{
    public class UserDbAccess : IEntityDbAccess<Usuario>
    {
        private readonly EfCoreContext _context;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;

        public UserDbAccess(IUnitOfWork context, SignInManager<Usuario> signInManager,
            UserManager<Usuario> userManager)
        {
            _context = (EfCoreContext)context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUsuarioAsync(Usuario user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<Usuario> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public void Add(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _userManager.Users.ToList();
        }

        public Usuario GetUsuario(string Id)
        {
            return _context.Users.Find(Id);
        }

        public async Task<Usuario> UpdateAsync(Usuario entity, Usuario user)
        {
            if (user == null)
                throw new Exception("User to be updated no exist");

            //just for sure that the fields of the viewModel are not null
            //null-coalescing is used
            user.FirstName = entity.FirstName ?? user.FirstName; ;
            user.SecondName = entity.SecondName ?? user.SecondName;
            user.FirstLastName = entity.FirstLastName ?? user.FirstLastName;
            user.SecondLastName = entity.SecondLastName ?? user.SecondLastName;
            user.HasPassport |= entity.HasPassport;

            await _userManager.UpdateAsync(user);
            return user;
        }

        public Usuario Update(Usuario entity, Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Itinerario> GetItinerariosNotFinished(Usuario usuario)
        {
            var itinerarios = from it in usuario.Itinerarios
                              where it.Estado != Estado.Realizado && it.Estado != Estado.Cancelado
                              select it;
            return itinerarios;
        }

        public IEnumerable<Itinerario> GetItinerariosDone(Usuario usuario)
        {
            var itinerarios = from it in usuario.Itinerarios
                              where it.Estado == Estado.Realizado
                              select it;
            return itinerarios;
        }

        public IEnumerable<Itinerario> GetItinerariosCanceled(Usuario usuario)
        {
            var itinerarios = from it in usuario.Itinerarios
                              where it.Estado == Estado.Cancelado
                              select it;
            return itinerarios;
        }

        public Itinerario GetItinerario(string userID, int iterID)
        {
            return _userManager.Users.Where(u => u.Id == userID).Single()
                               .Itinerarios.Where(i => i.ItinerarioID == iterID).Single();
        }

        public List<Itinerario> GetItinerarios(string userID)
        {
            return _userManager.Users.Where(u => u.Id == userID).Single()
                               .Itinerarios.ToList();
        }

        public List<Itinerario> GetAllItinerarios()
        {
            return _userManager.Users.SelectMany(u => u.Itinerarios.ToList()).ToList();
        }
    }
}

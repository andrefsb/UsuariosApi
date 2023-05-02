using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class UsuarioService
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private TokenService _tokenService;

        public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public async Task Cadastra(CreateUsuarioDto dto)
        {
            Usuario usuario = _mapper.Map<Usuario>
            (dto);

            IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);
         

            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Failed to create new User!");
            }
        }

        public async Task<string> Login(LoginUsuarioDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

            if (!result.Succeeded)
            {
                throw new ApplicationException("User not authenticated.");
            }

            var usuario = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(user=>user.NormalizedUserName==dto.UserName.ToUpper());

            var token = _tokenService.GenerateToken(usuario);

            return token;
        }
    }
}

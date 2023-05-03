using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace UsuariosApi.Authorization
{
    public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinima requirement)
        {
            var birthdayClaim = context.User.FindFirst(claim=>claim.Type==ClaimTypes.DateOfBirth);

            if (birthdayClaim is null) {
                return Task.CompletedTask;
            }

            var birthday = Convert.ToDateTime(birthdayClaim.Value);

            var idadeUsuario = DateTime.Today.Year - birthday.Year;

            if (birthday > DateTime.Today.AddYears(-idadeUsuario)) idadeUsuario--;

            if (idadeUsuario >= requirement.Idade) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}

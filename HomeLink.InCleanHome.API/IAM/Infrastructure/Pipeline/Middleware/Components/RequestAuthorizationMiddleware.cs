using HomeLink.InCleanHome.API.IAM.Application.Internal.OutboundServices;
using HomeLink.InCleanHome.API.IAM.Domain.Model.Queries;
using HomeLink.InCleanHome.API.IAM.Domain.Services;
using HomeLink.InCleanHome.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace HomeLink.InCleanHome.API.IAM.Infrastructure.Pipeline.Middleware.Components;

/// <summary>
///     Middleware that authorizes requests based on the JWT in the Authorization header.
///     If the token is valid, the user aggregate is attached to <c>HttpContext.Items["User"]</c>.
/// </summary>
public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        // 1. Se intercepta la petición HTTP y se obtiene la metadata del endpoint destino
        var endpoint = context.Request.HttpContext.GetEndpoint();

        // If there is no endpoint (e.g. Swagger UI, static files), skip authorization
        if (endpoint == null)
        {
            await next(context);
            return;
        }
        // 2. Se verifica si el endpoint destino está decorado con el atributo [AllowAnonymous]
        var allowAnonymous = endpoint.Metadata
            .Any(m => m.GetType() == typeof(AllowAnonymousAttribute));

        if (allowAnonymous)
        {
            await next(context);
            return;
        }
        // 3. Extracción del token JWT de la cabecera 'Authorization' (removiendo el esquema 'Bearer ')
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (string.IsNullOrWhiteSpace(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"message\":\"Authorization token is required.\"}");
            return;
        }
        // 4. Se delega la validación de la firma y expiración del token al servicio de dominio
        var userId = await tokenService.ValidateToken(token);
        if (userId == null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"message\":\"Invalid or expired token.\"}");
            return;
        }
        // 5. Aplicación del patrón CQRS: Se construye y despacha un Query para obtener los datos del usuario
        var getUserByIdQuery = new GetUserByIdQuery(userId.Value);
        var user = await userQueryService.Handle(getUserByIdQuery);
        context.Items["User"] = user;

        await next(context);
    }
}

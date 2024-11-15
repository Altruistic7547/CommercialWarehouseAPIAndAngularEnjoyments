using MyWarehouse.Infrastructure.Authentication.Core.Model;
using MyWarehouse.Infrastructure.Authentication.Core.Services;
using MyWarehouse.WebApi.Authentication.Models.Dtos;

namespace MyWarehouse.WebApi.API.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{v:apiVersion}/account")]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto login)
        => ProduceLoginResponse(
            await _userService.SignIn(login.Username, login.Password));

    /// <summary>
    /// OAuth2.0 compliant login endpoint. Used for Swagger login.
    /// </summary>
    [AllowAnonymous]
    [ApiVersionNeutral]
    [HttpPost("/account/oauth2/access_token")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<LoginResponseDto>> LoginForm([FromForm] LoginDto login)
    {
        var (result, model) = await _userService.SignIn(login.Username, login.Password);

        return result switch
        {
            MySignInResult.Success => Ok(new
            {
                access_token = model!.Token.AccessToken,
                token_type = model.Token.TokenType,
                expires_in = model.Token.GetRemainingLifetimeSeconds()
            }),
            _ => Unauthorized()
        };
    }

    private ActionResult<LoginResponseDto> ProduceLoginResponse((MySignInResult result, SignInData? data) loginResults)
    {
        var (result, data) = loginResults;

        return result switch
        {
            MySignInResult.Failed => Unauthorized("Username or password incorrect."),
            MySignInResult.LockedOut => Forbid("User is temporarily locked out."),
            MySignInResult.NotAllowed => Forbid("User is not allowed to sign in."),
            MySignInResult.Success when data is not null => Ok(new LoginResponseDto() {
                AccessToken = data.Token.AccessToken,
                TokenType = data.Token.TokenType,
                ExpiresIn = data.Token.GetRemainingLifetimeSeconds(),
                Username = data.Username,
                Email = data.Email,
                IsExternalLogin = data.IsExternalLogin,
                ExternalAuthenticationProvider = data.ExternalAuthenticationProvider
            }),
            _ => throw new InvalidEnumArgumentException("Unknown sign-in result or sign-in data missing.")
        };
    }
}

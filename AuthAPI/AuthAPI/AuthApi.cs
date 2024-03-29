namespace AuthAPI
{
    public static class AuthApi
    {

        public static void ApiConfiguration(this WebApplication app)
        {
            app.MapPost(pattern: "/Login", LoginAsync);
            app.MapPost(pattern: "/Register", RegisterAsync);


        }

        public static async Task<IResult> LoginAsync(IAuthService authService , LoginRequestModel loginRequestModel)
        {
            try
            {
               return Results.Ok(await authService.LoginAsync(loginRequestModel));
            }
            catch (Exception ex)
            {
               return Results.Problem(ex.Message);
            }
        }

        public static async Task<IResult> RegisterAsync(IAuthService authService, RegisterRequestModel RequestModel)
        {
            try
            {
                return Results.Ok(await authService.RegisterUserAsync(RequestModel));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }


    }
}

namespace BuzzAir.Utilities.DummyInitializers;

internal static class IdentityPageModelsInitializer
{
    public static void EnsureInitialized()
    {
        _ = new AccessDeniedModel();
        _ = new ConfirmEmailModel(null!);
        _ = new ConfirmEmailChangeModel(null!, null!);
        _ = new ExternalLoginModel(null!, null!, null!, null!, null!);
        _ = new ForgotPasswordModel(null!, null!);
        _ = new ForgotPasswordConfirmation();
        _ = new LockoutModel();
        _ = new LoginModel(null!, null!);
        _ = new LoginWith2faModel(null!, null!, null!);
        _ = new LoginWithRecoveryCodeModel(null!, null!, null!);
        _ = new LogoutModel(null!, null!);
        _ = new RegisterModel(null!, null!, null!, null!, null!, null!);
        _ = new RegisterConfirmationModel(null!);
        _ = new ResendEmailConfirmationModel(null!, null!);
        _ = new ResetPasswordModel(null!);
        _ = new ResetPasswordConfirmationModel();
    }
}

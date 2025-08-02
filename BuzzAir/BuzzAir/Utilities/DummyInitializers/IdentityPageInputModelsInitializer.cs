namespace BuzzAir.Utilities.DummyInitializers;

internal static class IdentityPageInputModelsInitializer
{
    public static void EnsureInitialized()
    {
        _ = new ChangePasswordInputModel();
        _ = new DeletePersonalDataInputModel();
        _ = new EmailInputModel();
        _ = new EnableAuthenticatorInputModel();
        _ = new ExternalLoginInputModel();
        _ = new ForgotPasswordInputModel();
        _ = new IndexInputModel();
        _ = new LoginInputModel();
        _ = new LoginWith2faInputModel();
        _ = new LoginWithRecoveryCodeInputModel();
        _ = new RegisterInputModel();
        _ = new ResendEmailConfirmationInputModel();
        _ = new ResetPasswordInputModel();
        _ = new SetPasswordInputModel();
    }
}

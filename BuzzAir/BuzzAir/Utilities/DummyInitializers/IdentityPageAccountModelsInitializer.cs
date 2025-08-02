namespace BuzzAir.Utilities.DummyInitializers;

internal static class IdentityPageAccountModelsInitializer
{
    public static void EnsureInitialized()
    {
        _ = new ChangePasswordModel(null!, null!, null!);
        _ = new DeletePersonalDataModel(null!, null!, null!);
        _ = new Disable2faModel(null!, null!);
        _ = new DownloadPersonalDataModel(null!, null!);
        _ = new EmailModel(null!, null!);
        _ = new EnableAuthenticatorModel(null!, null!, null!);
        _ = new ExternalLoginsModel(null!, null!, null!);
        _ = new GenerateRecoveryCodesModel(null!, null!);
        _ = new IndexModel(null!, null!);
        _ = new PersonalDataModel(null!);
        _ = new ResetAuthenticatorModel(null!, null!, null!);
        _ = new SetPasswordModel(null!, null!);
        _ = new ShowRecoveryCodesModel();
        _ = new TwoFactorAuthenticationModel(null!, null!);
    }
}

namespace BuzzAir.Services.Contracts
{
    public interface IBoardingPassService
    {
        Task<BoardingPassViewModel> GetBoardingPassAsync(string bookingId);
    }
}

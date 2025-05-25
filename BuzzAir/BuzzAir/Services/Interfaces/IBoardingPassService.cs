namespace BuzzAir.Services.Interfaces
{
    public interface IBoardingPassService
    {
        Task<BoardingPassViewModel> GetBoardingPassAsync(string bookingId);
    }
}

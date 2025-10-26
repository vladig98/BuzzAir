namespace BuzzAir.DTOs;

public record SeatDTO(int SeatNumber, string Type, bool Taken, bool AisleGapBefore);

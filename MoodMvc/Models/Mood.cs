using System.ComponentModel.DataAnnotations;

namespace MoodMvc.Models;

public class MoodEntry
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string UserId { get; set; }  // Link mood to user

    [Required]
    public DateTime Date { get; set; } = DateTime.UtcNow;

    [Required]
    public required string Mood { get; set; }  // e.g., Happy, Sad, Angry

    public string? JournalEntry { get; set; } // Optional journal entry
}
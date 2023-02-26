using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace VisualNovelManager.Models
{
    public class VisualNovel
    {
        [Key]
        public int GameId { get; set; }                 // Unique database id to allow multiple game entries

        public string? VndbId { get; set; }             // VNDB id if user used API search fill-in

        [Required]
        [Display(Name = "Title")]
        public string GameTitle { get; set; }           // Name of the Visual Novel

        [Display(Name = "Alias")]
        public string? GameAlias { get; set; }          // Fan given name for the Visual Novel (Used in casual conversation)

        [Required]
        [Display(Name = "Completion Status")]
        public string CompletionStatus { get; set; }    // User's state of game completion

        [Required]
        public string UserId { get; set; }              // Which user in database added the game to display in their lists

        //ToDo: Upload Image Option.                    // Cover image of the Visual Novel
    }

    public class VisualNovelViewModel
    {
        public int GameId { get; set; }

        public string GameTitle { get; set; }

        public string? GameAlias { get; set; }

        public string CompletionStatus { get; set; }
    }
}

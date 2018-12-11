namespace ChessAPI_LocalDB_.Models
{
    using System.ComponentModel.DataAnnotations;
    public partial class Game
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string FEN { get; set; }

        [Required]
        [StringLength(4)]
        public string Status { get; set; }
    }
}

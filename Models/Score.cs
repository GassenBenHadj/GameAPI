using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameAPI.Models
{
    public class Score
    {
        [Key, ForeignKey("User")]
        public int ScoreId { get; set; }
        public int ScoreValue { get; set; }
        public virtual User User { get; set; }
    }
}
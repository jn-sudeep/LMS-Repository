using System.ComponentModel.DataAnnotations;

namespace Root.Repository.Models
{
    public class User : ModelBase
    {
        #region Class Properties

        [Required]
        [MaxLength(15)]
        public string Name { get; set; }

        [Required]
        [MaxLength(15)]
        public string Password { get; set; }

        #endregion
    }
}

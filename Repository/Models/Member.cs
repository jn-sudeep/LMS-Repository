using System.ComponentModel.DataAnnotations;

namespace Root.Repository.Models
{
    public class Member : ModelBase
    {
        #region Class Properties

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        #endregion
    }
}

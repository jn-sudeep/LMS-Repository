using System;
using System.ComponentModel.DataAnnotations;

namespace Root.Repository.Models
{
    public class Book : ModelBase
    {
        #region Class Properties

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public DateTime IssueDate { get; set; }
        public Boolean IsAvailable { get; set; }
        public int MemberID { get; set; }

        #endregion
    }
}

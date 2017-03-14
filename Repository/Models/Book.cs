using System;

namespace Root.Repository.Models
{
    public class Book : ModelBase
    {
        #region Class Variables

        private string _Name;
        private DateTime _IssueDate = DateTime.Parse("1/1/1753");
        private Boolean _IsAvailable;
        private int _MemberID;

        #endregion

        #region Class Properties

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public DateTime IssueDate
        {
            get { return _IssueDate; }
            set { _IssueDate = value; }
        }

        public Boolean IsAvailable
        {
            get { return _IsAvailable; }
            set { _IsAvailable = value; }
        }

        public int MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }

        #endregion
    }
}

namespace Root.Repository.Models
{
    public class Member : ModelBase
    {
        #region Class Variables

        private string _Name;

        #endregion

        #region Class Properties

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        #endregion
    }
}

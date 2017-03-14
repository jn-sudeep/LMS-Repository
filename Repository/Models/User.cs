namespace Root.Repository.Models
{
   public class User : ModelBase
    {
      #region Class Variables

      private string _Name;
      private string _Password;

      #endregion

      #region Class Properties

      public string Name
      {
         get { return _Name; }
         set { _Name = value; }
      }

      public string Password
      {
         get { return _Password; }
         set { _Password = value; }
      }

      #endregion
   }
}

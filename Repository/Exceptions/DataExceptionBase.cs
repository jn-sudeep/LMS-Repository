using System;
using System.Collections.Generic;
using System.Text;

namespace Root.Repository.Exceptions
{
   public abstract class DataExceptionBase : System.Exception  
   {

      #region Class Variables

      private string _ErrorMessage;
      private string _MethodName;

      #endregion

      #region Class Properties

      public string ErrorMessage
      {
         get { return _ErrorMessage; }
         set { _ErrorMessage = value; }
      }

      public string MethodName
      {
         get { return _MethodName; }
         set { _MethodName = value; }
      }

      #endregion

      #region Class Constructors

      public DataExceptionBase() : base()
      {

      }

      public DataExceptionBase(string errorMessage, Exception ex)
         : base(errorMessage, ex)
      {
         ErrorMessage = errorMessage;
      }

      #endregion
   }
}

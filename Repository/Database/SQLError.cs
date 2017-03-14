using System.Data.SqlClient;

namespace Root.Repository.Database
{
   public static class SQLError 
   {
      #region Class Variables

      public enum ErrorType
      {
         UnidentifiedError = 0,
         UniqueConstraintViolation = 1,
         ReferenceConstraintViolation = 2
      }

      #endregion

      #region Class Functions

      public static ErrorType GetErrorType(SqlException ex)
      {
         ErrorType returnErrorType = ErrorType.UnidentifiedError;
         
         if ((ex.Number == 2627) && (ex.Message.IndexOf("UNIQUE KEY") > 0))
         {
            returnErrorType = ErrorType.UniqueConstraintViolation;
         }
         else if ((ex.Number == 547) && (ex.Message.IndexOf("REFERENCE") > 0))
         {
            returnErrorType = ErrorType.ReferenceConstraintViolation;
         }

         return returnErrorType;
      }

      #endregion
   }
}

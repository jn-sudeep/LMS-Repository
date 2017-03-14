using System;
using System.Reflection;
using System.Diagnostics;

namespace Root.Repository.Exceptions
{
   public class DataLayerException : DataExceptionBase
   {
      #region Class Constructors

      public DataLayerException() : base()
      {
      }

      public DataLayerException(string errorMessage, Exception parentException)
         : base(errorMessage, parentException)
      {
         StackTrace st = new StackTrace();
         StackFrame sf = st.GetFrame(1);
         MethodBase mb = sf.GetMethod();
         MethodName = mb.DeclaringType.ToString() + "." + mb.Name + "()";
      }

      #endregion
   }
}

using System;
using System.Reflection;
using System.Diagnostics;

namespace Root.Repository.Exceptions
{
   public class DataNotFoundException : DataExceptionBase
   {
      #region Class Constructors

      public DataNotFoundException() : base()
      {
      }

      public DataNotFoundException(string errorMessage, Exception parentException)
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

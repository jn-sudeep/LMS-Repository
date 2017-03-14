using System;
using System.Reflection;
using System.Diagnostics;

namespace Root.Repository.Exceptions
{
   public class DataDependencyException : DataExceptionBase
   {
      #region Class Constructors

      public DataDependencyException() : base()
      {
      }

      public DataDependencyException(string errorMessage, Exception parentException)
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

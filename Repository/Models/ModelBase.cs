using System;
using System.Collections.Generic;
using System.Text;

namespace Root.Repository.Models
{
   public abstract class ModelBase
   {
      #region Class Variables

      private int _ID;

      #endregion

      #region Class Properties

      public int ID
      {
         get { return _ID; }
         set { _ID = value; }
      }

      #endregion

      #region Class Constructors

      public ModelBase()
      {
         ID = 0;
      }

      #endregion

      #region Class Functions

      protected string FieldToString(object field)
      {
         return field.Equals(DBNull.Value) ? String.Empty : field.ToString().Trim();
      }

      protected DateTime FieldToDateTime(object field)
      {
         return field.Equals(DBNull.Value) ? DateTime.Parse("1/1/1753") : (DateTime)field;
      }

      protected bool FieldToBoolean(object field)
      {
         return field.Equals(DBNull.Value) ? false : Convert.ToBoolean(field);
      }

      protected int FieldToInt(object field)
      {
         return field.Equals(DBNull.Value) ? 0 : Convert.ToInt32(field);
      }

      #endregion
   }
}

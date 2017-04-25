using System;
using System.Data;
using System.Data.SqlClient;

using Microsoft.ApplicationBlocks.Data;
using Root.Repository.Exceptions;

namespace Root.Repository.Database
{
    public class DatabasePackage
    {
        #region Class Variables

        private SqlConnection _SQLConnection;
        private string _ConnectionString;
        private SqlTransaction _SQLTransaction;
        private string _CommandText;

        #endregion

        #region Class Properties

        public SqlConnection SQLConnection
        {
            get { return _SQLConnection; }
            set { _SQLConnection = value; }
        }

        public string CommandText
        {
            get { return _CommandText; }
            set { _CommandText = value; }
        }

        public SqlTransaction SQLTransaction
        {
            get { return _SQLTransaction; }
            set { _SQLTransaction = value; }
        }

        #endregion

        #region Class Constructors

        public DatabasePackage(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        #endregion

        #region Class Functions

        public void OpenConnection()
        {
            try
            {
                SQLConnection = new SqlConnection(_ConnectionString);
                SQLConnection.Open();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Unable to open connection with database.", ex);
            }
        }

        public SqlDataReader GetReader()
        {
            SqlDataReader reader = null;

            try
            {
                OpenConnection();
                reader = SqlHelper.ExecuteReader(SQLConnection, CommandType.Text, CommandText);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Unable to populate data reader.", ex);
            }

            return reader;
        }

        public int ExecuteNonQuery()
        {
            int rowsAffected = 0;

            try
            {
                rowsAffected = SqlHelper.ExecuteNonQuery(SQLTransaction, CommandType.Text, CommandText, null); 
                //(SQLConnection, CommandType.Text, CommandText, );
            }
            catch (SqlException ex)
            {
                if (SQLError.GetErrorType(ex) == SQLError.ErrorType.UnidentifiedError)
                {
                    throw new DataAccessException("Unidentified error occured at database server.", ex);
                }
                else if (SQLError.GetErrorType(ex) == SQLError.ErrorType.UniqueConstraintViolation)
                {
                    throw new DataDuplicityException(ex.Message, ex);
                }
                else if (SQLError.GetErrorType(ex) == SQLError.ErrorType.ReferenceConstraintViolation)
                {
                    throw new DataDependencyException(ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Unable to save data.", ex);
            }

            return rowsAffected;
        }

        public void CloseConnection()
        {
            try
            {
                if (SQLConnection != null)
                {
                    SQLConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Unable to close connection with database.", ex);
            }
        }

        public void BeginTransaction()
        {
            try
            {
                if (SQLConnection != null)
                {
                    SQLTransaction = SQLConnection.BeginTransaction();
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Unable to begin transaction.", ex);
            }
        }

        public void CommitTransaction()
        {
            try
            {
                if (SQLTransaction != null)
                {
                    SQLTransaction.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Unable to commit transaction.", ex);
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                if (SQLTransaction != null)
                {
                    SQLTransaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Unable to commit transaction.", ex);
            }
        }

        public string FieldToString(object field)
        {
            return field.Equals(DBNull.Value) ? String.Empty : field.ToString().Trim();
        }

        public DateTime FieldToDateTime(object field)
        {
            return field.Equals(DBNull.Value) ? DateTime.Parse("1/1/1753") : (DateTime)field;
        }

        public bool FieldToBoolean(object field)
        {
            return field.Equals(DBNull.Value) ? false : Convert.ToBoolean(field);
        }

        public int FieldToInt(object field)
        {
            return field.Equals(DBNull.Value) ? 0 : Convert.ToInt32(field);
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using Root.Repository.Models;
using Root.Repository.Database;
using Root.Repository.Exceptions;

namespace Root.Repository
{
    public class LibraryRepository
    {
        #region Class Variables

        private string _ConnectionString;

        #endregion

        #region Class Constructors

        public LibraryRepository(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        #endregion

        #region Book Functions

        public Book GetBookByID(int id)
        {
            DatabasePackage dbPackage = new DatabasePackage(_ConnectionString);
            SqlDataReader reader = null;

            try
            {
                dbPackage.CommandText = "Select ID, Name, IssueDate, MemberID From Book Where ID = " + id;
                reader = dbPackage.GetReader();

                reader.Read();

                Book book = new Book
                {
                    ID = id,
                    Name = dbPackage.FieldToString(reader["Name"]),
                    IssueDate = dbPackage.FieldToDateTime(reader["IssueDate"]),
                    MemberID = dbPackage.FieldToInt(reader["MemberID"])
                };

                return book;
            }
            catch (DataAccessException ex)
            {
                throw new DataLayerException("Unable to retrieve data.", ex);
            }
            catch (Exception ex)
            {
                throw new DataLayerException("Unable to retrieve data.", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public void SaveBook(Book book)
        {
            DatabasePackage dbPackage = new DatabasePackage(_ConnectionString);

            try
            {
                string sqlQuery = string.Empty;

                if (book.ID == 0) // Add a book
                {
                    sqlQuery = "Insert Into Book (Name) Values ('" + book.Name + "')";
                }
                else // Modify a book
                {
                    sqlQuery = "Update Book Set Name = '" + book.Name + "' Where ID = " + book.ID;
                }

                dbPackage.CommandText = sqlQuery;
                dbPackage.OpenConnection();
                dbPackage.BeginTransaction();

                if (dbPackage.ExecuteNonQuery() == 0)
                {
                    throw new DataAccessException("Unable to save object.", new Exception());
                }

                dbPackage.CommitTransaction();
            }
            catch (DataDuplicityException ex)
            {
                dbPackage.RollbackTransaction();

                if (ex.ErrorMessage.IndexOf("Book_Name_Unique") > 0)
                {
                    throw new DataDuplicityException("Book already exist.", ex);
                }
            }
            catch (DataAccessException ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataLayerException("Unable to save object.", ex);
            }
            catch (Exception ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataLayerException("Unable to save object.", ex);
            }
            finally
            {
                dbPackage.CloseConnection();
            }
        }

        public void DeleteBook(int id)
        {
            DatabasePackage dbPackage = new DatabasePackage(_ConnectionString);

            try
            {
                dbPackage.CommandText = "Delete From Book Where ID = " + id;
                dbPackage.OpenConnection();
                dbPackage.BeginTransaction();

                if (dbPackage.ExecuteNonQuery() == 0)
                {
                    throw new DataAccessException("Unable to save object.", new Exception());
                }

                dbPackage.CommitTransaction();
            }
            catch (DataDependencyException ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataDependencyException(ex.ErrorMessage, ex);
            }
            catch (DataAccessException ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataLayerException("Unable to save object.", ex);
            }
            catch (Exception ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataLayerException("Unable to save object.", ex);
            }
            finally
            {
                dbPackage.CloseConnection();
            }
        }

        public void IssueBook(int bookID, int memberID, DateTime issueDate)
        {
            DatabasePackage dbPackage = new DatabasePackage(_ConnectionString);

            try
            {
                dbPackage.CommandText = "Update Book Set IssueDate = '" + issueDate + "', MemberID = " + memberID + " Where ID = " + bookID;
                dbPackage.OpenConnection();
                dbPackage.BeginTransaction();

                if (dbPackage.ExecuteNonQuery() == 0)
                {
                    throw new DataAccessException("Unable to save object.", new Exception());
                }

                dbPackage.CommitTransaction();
            }
            catch (DataDuplicityException ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataDuplicityException(ex.ErrorMessage, ex);
            }
            catch (DataAccessException ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataLayerException("Unable to save object.", ex);
            }
            catch (Exception ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataLayerException("Unable to save object.", ex);
            }
            finally
            {
                dbPackage.CloseConnection();
            }
        }

        public void ReturnBook(int bookID)
        {
            DatabasePackage dbPackage = new DatabasePackage(_ConnectionString);

            try
            {
                dbPackage.CommandText = "Update Book Set IssueDate = null, MemberID = null Where ID = " + bookID;
                dbPackage.OpenConnection();
                dbPackage.BeginTransaction();

                if (dbPackage.ExecuteNonQuery() == 0)
                {
                    throw new DataAccessException("Unable to save object.", new Exception());
                }

                dbPackage.CommitTransaction();
            }
            catch (DataDuplicityException ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataDuplicityException(ex.ErrorMessage, ex);
            }
            catch (DataAccessException ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataLayerException("Unable to save object.", ex);
            }
            catch (Exception ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataLayerException("Unable to save object.", ex);
            }
            finally
            {
                dbPackage.CloseConnection();
            }
        }

        public List<Book> GetBooks()
        {
            DatabasePackage dbPackage = new DatabasePackage(_ConnectionString);
            List<Book> books = new List<Book>();
            SqlDataReader reader = null;

            try
            {
                dbPackage.CommandText = "Select ID, Name, IssueDate, MemberID From Book Order By Name";
                reader = dbPackage.GetReader();

                while (reader.Read())
                {
                    Book book = new Book();

                    book.ID = dbPackage.FieldToInt(reader["ID"]);
                    book.Name = dbPackage.FieldToString(reader["Name"]);
                    book.IssueDate = dbPackage.FieldToDateTime(reader["IssueDate"]);
                    book.MemberID = dbPackage.FieldToInt(reader["MemberID"]);

                    books.Add(book);
                }
            }
            catch (DataAccessException ex)
            {
                throw new DataLayerException("Collection could not populated.", ex);
            }
            catch (Exception ex)
            {
                throw new DataLayerException("Collection could not populated.", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                dbPackage.CloseConnection();
            }

            return books;
        }

        public List<Book> GetAvailableBooks()
        {
            DatabasePackage dbPackage = new DatabasePackage(_ConnectionString);
            List<Book> books = new List<Book>();
            SqlDataReader reader = null;

            try
            {
                dbPackage.CommandText = "Select ID, Name, IssueDate, MemberID From Book Where MemberID is Null Order By Name";
                reader = dbPackage.GetReader();

                while (reader.Read())
                {
                    Book book = new Book();

                    book.ID = dbPackage.FieldToInt(reader["ID"]);
                    book.Name = dbPackage.FieldToString(reader["Name"]);
                    book.IssueDate = dbPackage.FieldToDateTime(reader["IssueDate"]);
                    book.MemberID = dbPackage.FieldToInt(reader["MemberID"]);

                    books.Add(book);
                }
            }
            catch (DataAccessException ex)
            {
                throw new DataLayerException("Collection could not populated.", ex);
            }
            catch (Exception ex)
            {
                throw new DataLayerException("Collection could not populated.", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                dbPackage.CloseConnection();
            }

            return books;
        }

        public List<Book> GetIssuedBooks()
        {
            DatabasePackage dbPackage = new DatabasePackage(_ConnectionString);
            List<Book> books = new List<Book>();
            SqlDataReader reader = null;

            try
            {
                dbPackage.CommandText = "Select ID, Name, IssueDate, MemberID From Book Where MemberID is Not Null Order By Name";
                reader = dbPackage.GetReader();

                while (reader.Read())
                {
                    Book book = new Book();

                    book.ID = dbPackage.FieldToInt(reader["ID"]);
                    book.Name = dbPackage.FieldToString(reader["Name"]);
                    book.IssueDate = dbPackage.FieldToDateTime(reader["IssueDate"]);
                    book.MemberID = dbPackage.FieldToInt(reader["MemberID"]);

                    books.Add(book);
                }
            }
            catch (DataAccessException ex)
            {
                throw new DataLayerException("Collection could not populated.", ex);
            }
            catch (Exception ex)
            {
                throw new DataLayerException("Collection could not populated.", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                dbPackage.CloseConnection();
            }

            return books;
        }

        #endregion

        #region Member Functions

        public Member GetMemberByID(int id)
        {
            DatabasePackage dbPackage = new DatabasePackage(_ConnectionString);
            SqlDataReader reader = null;

            try
            {
                dbPackage.CommandText = "Select ID, Name From Member Where ID = " + id;
                reader = dbPackage.GetReader();

                reader.Read();

                Member member = new Member
                {
                    ID = id,
                    Name = dbPackage.FieldToString(reader["Name"])
                };

                return member;
            }
            catch (DataAccessException ex)
            {
                throw new DataLayerException("Unable to retrieve data.", ex);
            }
            catch (Exception ex)
            {
                throw new DataLayerException("Unable to retrieve data.", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public void SaveMember(Member member)
        {
            DatabasePackage dbPackage = new DatabasePackage(_ConnectionString);
            try
            {
                string sqlQuery = string.Empty;

                if (member.ID == 0) // Add a member
                {
                    sqlQuery = "Insert Into Member (Name) Values ('" + member.Name + "')";
                }
                else // Modify a book
                {
                    sqlQuery = "Update Member Set Name = '" + member.Name + "' Where ID = " + member.ID;
                }

                dbPackage.CommandText = sqlQuery;
                dbPackage.OpenConnection();
                dbPackage.BeginTransaction();

                if (dbPackage.ExecuteNonQuery() == 0)
                {
                    throw new DataAccessException("Unable to save object.", new Exception());
                }

                dbPackage.CommitTransaction();

            }
            catch (DataDuplicityException ex)
            {
                dbPackage.RollbackTransaction();

                if (ex.ErrorMessage.IndexOf("Member_Name_Unique") > 0)
                {
                    throw new DataDuplicityException("Member already exist.", ex);
                }
            }
            catch (DataAccessException ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataLayerException("Unable to save object.", ex);
            }
            catch (Exception ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataLayerException("Unable to save object.", ex);
            }
            finally
            {
                dbPackage.CloseConnection();
            }
        }

        public void DeleteMember(int id)
        {
            DatabasePackage dbPackage = new DatabasePackage(_ConnectionString);
            try
            {
                dbPackage.CommandText = "Delete From Member Where ID = " + id;
                dbPackage.OpenConnection();
                dbPackage.BeginTransaction();

                if (dbPackage.ExecuteNonQuery() == 0)
                {
                    throw new DataAccessException("Unable to save object.", new Exception());
                }

                dbPackage.CommitTransaction();
            }
            catch (DataDependencyException ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataDependencyException(ex.ErrorMessage, ex);
            }
            catch (DataAccessException ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataLayerException("Unable to save object.", ex);
            }
            catch (Exception ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataLayerException("Unable to save object.", ex);
            }
            finally
            {
                dbPackage.CloseConnection();
            }
        }

        public List<Member> GetMembers()
        {
            DatabasePackage dbPackage = new DatabasePackage(_ConnectionString);
            List<Member> members = new List<Member>();
            SqlDataReader reader = null;

            try
            {
                dbPackage.CommandText = "Select ID, Name From Member Order By Name";
                reader = dbPackage.GetReader();

                while (reader.Read())
                {
                    Member member = new Member();

                    member.ID = dbPackage.FieldToInt(reader["ID"]);
                    member.Name = dbPackage.FieldToString(reader["Name"]);

                    members.Add(member);
                }
            }
            catch (DataAccessException ex)
            {
                throw new DataLayerException("Collection could not populated.", ex);
            }
            catch (Exception ex)
            {
                throw new DataLayerException("Collection could not populated.", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                dbPackage.CloseConnection();
            }

            return members;
        }

        public List<Member> GetNoBookIssuedMembers()
        {
            DatabasePackage dbPackage = new DatabasePackage(_ConnectionString);
            List<Member> members = new List<Member>();
            SqlDataReader reader = null;

            try
            {
                dbPackage.CommandText = "Select ID, Name From Member Where ID Not In (Select MemberID From Book Where MemberID Is Null) Order By Name";
                reader = dbPackage.GetReader();

                while (reader.Read())
                {
                    Member member = new Member();

                    member.ID = dbPackage.FieldToInt(reader["ID"]);
                    member.Name = dbPackage.FieldToString(reader["Name"]);

                    members.Add(member);
                }
            }
            catch (DataAccessException ex)
            {
                throw new DataLayerException("Collection could not populated.", ex);
            }
            catch (Exception ex)
            {
                throw new DataLayerException("Collection could not populated.", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                dbPackage.CloseConnection();
            }

            return members;
        }

        #endregion

        #region User Functions

        public User GeUserByID(int id)
        {
            DatabasePackage dbPackage = new DatabasePackage(_ConnectionString);
            SqlDataReader reader = null;

            try
            {
                dbPackage.CommandText = "Select ID, Name, Password From User Where ID = " + id;
                reader = dbPackage.GetReader();

                reader.Read();

                User user = new User
                {
                    ID = id,
                    Name = dbPackage.FieldToString(reader["Name"]),
                    Password = dbPackage.FieldToString(reader["Password"])
                };

                return user;
            }
            catch (DataAccessException ex)
            {
                throw new DataLayerException("Unable to retrieve data.", ex);
            }
            catch (Exception ex)
            {
                throw new DataLayerException("Unable to retrieve data.", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public void SaveUser(User user)
        {
            DatabasePackage dbPackage = new DatabasePackage(_ConnectionString);
            try
            {
                string sqlQuery = string.Empty;

                if (user.ID == 0) // Add a user
                {
                    sqlQuery = "Insert Into User (Name, Password) Values ('" + user.Name + "', '" + user.Password +")";
                }
                else // Modify a user
                {
                    sqlQuery = "Update User Set Name = '" + user.Name + "', Password = '" + user.Password + "' Where ID = " + user.ID;
                }

                dbPackage.CommandText = sqlQuery;
                dbPackage.OpenConnection();
                dbPackage.BeginTransaction();

                if (dbPackage.ExecuteNonQuery() == 0)
                {
                    throw new DataAccessException("Unable to save object.", new Exception());
                }

                dbPackage.CommitTransaction();

            }
            catch (DataDuplicityException ex)
            {
                dbPackage.RollbackTransaction();

                if (ex.ErrorMessage.IndexOf("User_Name_Unique") > 0)
                {
                    throw new DataDuplicityException("User already exist.", ex);
                }
            }
            catch (DataAccessException ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataLayerException("Unable to save object.", ex);
            }
            catch (Exception ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataLayerException("Unable to save object.", ex);
            }
            finally
            {
                dbPackage.CloseConnection();
            }
        }

        public void DeleteUser(int id)
        {
            DatabasePackage dbPackage = new DatabasePackage(_ConnectionString);
            try
            {
                dbPackage.CommandText = "Delete From User Where ID = " + id;
                dbPackage.OpenConnection();
                dbPackage.BeginTransaction();

                if (dbPackage.ExecuteNonQuery() == 0)
                {
                    throw new DataAccessException("Unable to save object.", new Exception());
                }

                dbPackage.CommitTransaction();
            }
            catch (DataDependencyException ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataDependencyException(ex.ErrorMessage, ex);
            }
            catch (DataAccessException ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataLayerException("Unable to save object.", ex);
            }
            catch (Exception ex)
            {
                dbPackage.RollbackTransaction();
                throw new DataLayerException("Unable to save object.", ex);
            }
            finally
            {
                dbPackage.CloseConnection();
            }
        }

        public List<User> GetUsers()
        {
            DatabasePackage dbPackage = new DatabasePackage(_ConnectionString);
            List<User> users = new List<User>();
            SqlDataReader reader = null;

            try
            {
                dbPackage.CommandText = "Select ID, Name, Password From User Order By Name";
                reader = dbPackage.GetReader();

                while (reader.Read())
                {
                    User user = new User();

                    user.ID = dbPackage.FieldToInt(reader["ID"]);
                    user.Name = dbPackage.FieldToString(reader["Name"]);
                    user.Password = dbPackage.FieldToString(reader["Password"]);

                    users.Add(user);
                }
            }
            catch (DataAccessException ex)
            {
                throw new DataLayerException("Collection could not populated.", ex);
            }
            catch (Exception ex)
            {
                throw new DataLayerException("Collection could not populated.", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                dbPackage.CloseConnection();
            }

            return users;
        }

        #endregion
    }
}

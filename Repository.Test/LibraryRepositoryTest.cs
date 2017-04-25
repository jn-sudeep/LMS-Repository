using System;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Root.Repository.Database;
using Root.Repository.Models;

namespace Root.Repository.Test
{
    [TestClass]
    public class LibraryRepositoryTest
    {

        private string _ConnectionString = "Initial Catalog=Library;Data Source=localhost\\orbica;User ID=sa;Password=India123;";

        [TestMethod]
        public void GetBookByIDTest()
        {
            DatabasePackage databasePackage = new DatabasePackage(_ConnectionString);
            SqlDataReader reader = null;

            LibraryRepository libraryRepository = new LibraryRepository(_ConnectionString);

            try
            {
                // Add a new book                

                Book newBook = new Book();
                newBook.Name = "Unit Test Book";

                libraryRepository.SaveBook(newBook);

                databasePackage.CommandText = "Select ID From Book Where Name = 'Unit Test Book'";
                reader = databasePackage.GetReader();
                reader.Read();

                int bookID = databasePackage.FieldToInt(reader["ID"]);

                // Test the function

                Book book = libraryRepository.GetBookByID(bookID);
                Assert.AreEqual(book.ID, bookID);

                // Delete newly added book

                libraryRepository.DeleteBook(bookID);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        [TestMethod]
        public void SaveBookTest()
        {
            DatabasePackage databasePackage = new DatabasePackage(_ConnectionString);
            SqlDataReader reader = null;

            try
            {
                LibraryRepository libraryRepository = new LibraryRepository(_ConnectionString);

                // Test Add Book

                Book newBook = new Book();
                newBook.Name = "Unit Test Book";

                libraryRepository.SaveBook(newBook);

                databasePackage.CommandText = "Select ID From Book Where Name = 'Unit Test Book'";
                reader = databasePackage.GetReader();

                reader.Read();

                int bookID = databasePackage.FieldToInt(reader["ID"]);

                Assert.AreNotEqual(0, bookID);

                // Test Modify Book

                Book existingBook = libraryRepository.GetBookByID(bookID);
                existingBook.Name = "Unit Test Book (Modified)";
                libraryRepository.SaveBook(existingBook);

                Book modifiedBook = libraryRepository.GetBookByID(bookID);

                Assert.AreEqual("Unit Test Book (Modified)", modifiedBook.Name);

                // Delete newly added book

                libraryRepository.DeleteBook(bookID);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        [TestMethod]
        public void IssueBookTest()
        {
            DatabasePackage databasePackage = new DatabasePackage(_ConnectionString);
            SqlDataReader reader = null;

            LibraryRepository libraryRepository = new LibraryRepository(_ConnectionString);

            try
            {
                // Add a new Book                

                Book newBook = new Book();
                newBook.Name = "Unit Test Book";

                libraryRepository.SaveBook(newBook);

                databasePackage.CommandText = "Select ID From Book Where Name = 'Unit Test Book'";
                reader = databasePackage.GetReader();
                reader.Read();

                int bookID = databasePackage.FieldToInt(reader["ID"]);

                // Add a new Member

                Member newMember = new Member();
                newMember.Name = "Unit Test Member";

                libraryRepository.SaveMember(newMember);

                databasePackage.CommandText = "Select ID From Member Where Name = 'Unit Test Member'";
                reader = databasePackage.GetReader();
                reader.Read();

                int memberID = databasePackage.FieldToInt(reader["ID"]);

                // Test the function

                libraryRepository.IssueBook(bookID, memberID, DateTime.Now);

                Book issuedBook = libraryRepository.GetBookByID(bookID);

                Assert.AreEqual(issuedBook.MemberID, memberID);

                // Delete newly added book

                libraryRepository.DeleteBook(bookID);

                // Delete newly added Member

                libraryRepository.DeleteMember(memberID);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        [TestMethod]
        public void ReturnBookTest()
        {
            DatabasePackage databasePackage = new DatabasePackage(_ConnectionString);
            SqlDataReader reader = null;

            LibraryRepository libraryRepository = new LibraryRepository(_ConnectionString);

            try
            {
                // Add a new Book                

                Book newBook = new Book();
                newBook.Name = "Unit Test Book";

                libraryRepository.SaveBook(newBook);

                databasePackage.CommandText = "Select ID From Book Where Name = 'Unit Test Book'";
                reader = databasePackage.GetReader();
                reader.Read();

                int bookID = databasePackage.FieldToInt(reader["ID"]);

                // Add a new Member

                Member newMember = new Member();
                newMember.Name = "Unit Test Member";

                libraryRepository.SaveMember(newMember);

                databasePackage.CommandText = "Select ID From Member Where Name = 'Unit Test Member'";
                reader = databasePackage.GetReader();
                reader.Read();

                int memberID = databasePackage.FieldToInt(reader["ID"]);

                // Issue this Book to a Member

                libraryRepository.IssueBook(bookID, memberID, DateTime.Now);

                // Test the function

                libraryRepository.ReturnBook(bookID);

                Book returnBook = libraryRepository.GetBookByID(bookID);

                Assert.AreEqual(returnBook.MemberID, 0);

                // Delete newly added book

                libraryRepository.DeleteBook(bookID);

                // Delete newly added Member

                libraryRepository.DeleteMember(memberID);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

    }
}

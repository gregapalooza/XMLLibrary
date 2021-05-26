using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace _7LibraryXML
{
    class Book
    {
        #region ---- Private class fields ----
        private string bookTitle;
        private string bookAuthor;
        private string bookSubject;
        private string bookISBN;
        private string dateAdded;
        private int numCopies;
        private int copiesCheckedOut;
        #endregion
        #region ---- Getter and setters ----
        public string BookTitle { get => bookTitle; set { bookTitle = value; } }
        public string BookAuthor { get => bookAuthor; set {bookAuthor = value; } }
        public string BookSubject { get => bookSubject; set {bookSubject = value; } }
        public string BookISBN { get => bookISBN; set { bookISBN = value; } }
        public string DateAdded { get => dateAdded; set { dateAdded = value; } }
        public int NumCopies { get => numCopies; set { numCopies = value; } }
        public int CopiesCheckedOut { get => copiesCheckedOut; set { copiesCheckedOut = value; } }
        #endregion
        #region ---- Constructors ----
        public Book() { }
        public Book(string bookTitle, string bookAuthor, string bookSubject, 
            string bookISBN, string dateAdded, int numCopies, int copiesCheckedOut)
        {
            BookTitle = bookTitle;
            BookAuthor = bookAuthor;
            BookSubject = bookSubject;
            BookISBN = bookISBN;
            DateAdded = dateAdded;
            NumCopies = numCopies;
            CopiesCheckedOut = copiesCheckedOut;
        }
        #endregion
        #region ---- Class Methods ----
        public override string ToString()
        {
            string bookInfo = "";
            bookInfo = $"Book: {BookTitle}  Author: {BookAuthor} | Subject: {BookSubject}  " +
                $"ISBN: {BookISBN} Date Added: {DateAdded} | Num Copies {NumCopies}";
            return bookInfo;
        }
        /// <summary>
        /// This method compares the amount of books overall to the
        /// ones check out to determine if there are any 
        /// left to check out.
        /// </summary>
        /// <returns></returns>
        public bool IsBookAvailable()
        {
            return (copiesCheckedOut < numCopies);
        }
        /// <summary>
        /// Based off the answer of book availibility 
        /// this method checks out a book and gives a 
        /// return date for the book
        /// </summary>
        public void CheckOutBook()
        {
            DateTime today = DateTime.Now;
            if (IsBookAvailable())
            {
                CopiesCheckedOut++;
                MessageBox.Show($"Book is available. It is due back in 2 weeks: {today.AddDays(14)}.");
                //.AddDays(14) gives the user 2 weeks to read the book
            }
            else
            {
                MessageBox.Show("We're sorry, all copies are check out at the moment.");
                
            }
        }
        /// <summary>
        /// This method allows for the user to check a book back in
        /// doesn't allow for 'over returns'
        /// </summary>
        public void CheckInBook()
        {
            if (copiesCheckedOut < numCopies && copiesCheckedOut != 0)
            {
                copiesCheckedOut--;
                MessageBox.Show("The book has been returned. Thank you!");
            }
            else
            {
                MessageBox.Show("All books are accounted for.");
            }
        }
        #endregion
    }
}

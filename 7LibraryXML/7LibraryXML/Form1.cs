using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace _7LibraryXML
{
    public partial class Form1 : Form
    {
        //string variable for xml file
        string xmlName = "Books.xml";
        #region ----- Form control methods -----
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PutBooksOnShelf();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Checkout();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReturnBook();
        }
        #endregion
        #region ----- Other methods -----
        /// <summary>
        /// This method loads the xml file to 
        /// listBox control to view all the books
        /// </summary>
        private void PutBooksOnShelf()
        {
            var Books = new List<Book>();;
            // try/catch to help program not crash instantly
            try
            {
                var xml = XDocument.Load(xmlName).Root;

                foreach (var bk in xml.Elements()) //cycle through each book in xml file
                {
                    var Book = new Book();

                    Book.BookTitle = bk.Elements().Where(x => x.Name == "title").FirstOrDefault().Value.ToString();
                    Book.BookAuthor = bk.Elements().Where(x => x.Name == "author").FirstOrDefault().Value.ToString();
                    Book.BookSubject = bk.Elements().Where(x => x.Name == "subject").FirstOrDefault().Value.ToString();
                    Book.BookISBN = bk.Elements().Where(x => x.Name == "isbn").FirstOrDefault().Value.ToString();
                    Book.DateAdded = bk.Elements().Where(x => x.Name == "dateadded").FirstOrDefault().Value.ToString();
                    Book.NumCopies = Convert.ToInt16(bk.Elements().Where(x => x.Name == "numcopies").FirstOrDefault().Value);

                    listBox1.Items.Add(Book);
                }
            }
            catch (Exception e)
            {
                listBox1.Items.Add(e.Message);
            }

        }
        /// <summary>
        /// This method control the check out button on the form
        /// </summary>
        public void Checkout()
        {
            try
            {
                Book thisBook = (Book)listBox1.Items[listBox1.SelectedIndex]; //user needs to click on a selection first this helps prevent this error
                thisBook.CheckOutBook();
                var xml = XDocument.Load(xmlName).Root;
                foreach (var bk in xml.Elements())
                {
                    if (bk.Elements().Where(x => x.Name == "title").FirstOrDefault().Value.ToString() == thisBook.BookTitle)
                    {
                        bk.Elements().Where(x => x.Name == "numcopies").FirstOrDefault().Value = thisBook.NumCopies.ToString();
                    }
                }

                xml.Save(xmlName);
            }
            catch (Exception e)
            { 
                MessageBox.Show(e.Message); //Better to show as a messagebox than add to listBox
            }
        }
        /// <summary>
        /// This method controls the return button on form
        /// works the same as check out method
        /// </summary>
        public void ReturnBook()
        {
            try
            {
                Book thisBook = (Book)listBox1.Items[listBox1.SelectedIndex];
                thisBook.CheckInBook();
                var xml = XDocument.Load(xmlName).Root;
                foreach (var bk in xml.Elements())
                {
                    if (bk.Elements().Where(x => x.Name == "title").FirstOrDefault().Value.ToString() == thisBook.BookTitle)
                    {
                        bk.Elements().Where(x => x.Name == "numcopies").FirstOrDefault().Value = thisBook.NumCopies.ToString();
                    }
                }

                xml.Save(xmlName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #endregion
    }
}

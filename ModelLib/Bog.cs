using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib
{
    public class Bog
    {
        private string _title;
        private string _author;
        private double _pagecount;
        private string _isbn13;

        public string Title
        {
            get => _title;
            set
            {
                if (value.Length < 2)
                {
                    throw new ArgumentOutOfRangeException("Too short, minimum length is 2");
                }
                else
                {
                    _title = value;
                }
            }
        }
        public string Author
        {
            get => _author;
            set => _author = value;
        }
        public double Pagecount
        {
            get => _pagecount;
            set
            {
                if (value <= 10 || value >= 1000)
                {
                    throw new ArgumentOutOfRangeException("Too many or too few pages! 11 to 999");
                }
                else
                {
                    _pagecount = value;
                }
            }
        }
        public string Isbn13
        {
            get => _isbn13;
            set
            {
                if (value.Length < 13 || value.Length > 13)
                {
                    throw new ArgumentOutOfRangeException("ISBN number MUST have 13 character count");
                }
                else
                {
                    _isbn13 = value;
                }
            }
        }

        public Bog()
        {

        }
    }
    public class FakeClass
    {
        public string S { get; set; }
        public FakeClass(string s)
        {
            s = S;
        }
    }
}

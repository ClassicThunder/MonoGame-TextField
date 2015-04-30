//using System;
//using System.Globalization;
//using NUnit.Framework;
//using Text = CTTextBox.Text;

//namespace TextBoxUnitTests
//{
//    [TestFixture]
//    public class TextTest 
//    {
//        private Text _text;

//        [SetUp]
//        public void SetUp() 
//        {
//            _text = new Text(20);
//        }

//        [TearDown]
//        public void TearDown() 
//        {
//            _text = null;
//        }        

//        [Test]
//        public void TestInsertCharacter()
//        {
//            for (var x = 0; x < 10; x++)
//            {
//                _text.InsertCharacter(x, x.ToString(CultureInfo.InvariantCulture)[0]);
//                Assert.AreEqual(_text.Length - 1, x);
//            }
//            Assert.AreEqual(_text.String, "0123456789");
//            Assert.AreEqual(_text.Length, 10);
//            for (var x = 0; x < 10; x++)
//            {
//                _text.InsertCharacter(x, x.ToString(CultureInfo.InvariantCulture)[0]);
//                Assert.AreEqual(_text.Length - 1, 10 + x);
//            }
//            Assert.AreEqual(_text.String, "01234567890123456789");
//            Assert.AreEqual(_text.Length, 20);

//            Assert.Throws<ArgumentException>(() => _text.InsertCharacter(0, 'a'));
//            Assert.AreEqual(_text.Length, 20);

//            Assert.Throws<ArgumentException>(() => _text.InsertCharacter(10, 'a'));
//            Assert.AreEqual(_text.Length, 20);

//            Assert.Throws<ArgumentException>(() => _text.InsertCharacter(20, 'a'));
//            Assert.AreEqual(_text.Length, 20);

//            _text.String = "0123456789";

//            _text.InsertCharacter(0, 'a');
//            Assert.AreEqual(_text.String, "a0123456789");
//            Assert.AreEqual(_text.Length, "a0123456789".Length);

//            _text.InsertCharacter(6, 'a');
//            Assert.AreEqual(_text.String, "a01234a56789");
//            Assert.AreEqual(_text.Length, "a01234a56789".Length);

//            _text.InsertCharacter(12, 'a');
//            Assert.AreEqual(_text.String, "a01234a56789a");
//            Assert.AreEqual(_text.Length, "a01234a56789a".Length);

//            _text.String = "";

//            _text.InsertCharacter(0, 'a');
//            Assert.AreEqual(_text.String, "a");
//            Assert.AreEqual(_text.Length, "a".Length);

//            Assert.Throws<ArgumentException>(() => _text.InsertCharacter(2, 'a'));
//            Assert.AreEqual(_text.Length, "a".Length);
//        }

//        [Test]
//        public void Replace()
//        {
//            _text.String = "0123456789";

//            _text.Replace(0, 0, "abcdefghij");
//            Assert.AreEqual(_text.String, "abcdefghij0123456789");
//            Assert.AreEqual(_text.Length, 20);

//            _text.String = "01234567890123456789";

//            _text.Replace(0, 10, "abcdefghij");
//            Assert.AreEqual(_text.String, "abcdefghij0123456789");
//            Assert.AreEqual(_text.Length, 20);

//            _text.Replace(10, 20, "abcdefghij");
//            Assert.AreEqual(_text.String, "abcdefghijabcdefghij");
//            Assert.AreEqual(_text.Length, 20);

//            Assert.Throws<ArgumentException>(() => _text.Replace(0, 10, "01234567890"));
//            Assert.Throws<ArgumentException>(() => _text.Replace(10, 20, "01234567890"));
//        }

//        [Test]
//        public void RemoveCharacters()
//        {
//            _text.String = "0";

//            _text.RemoveCharacters(0, 1);
//            Assert.AreEqual(_text.String, "");
//            Assert.AreEqual(_text.Length, "".Length);

//            _text.String = "0123456789abcdefghij";

//            _text.RemoveCharacters(0, 1);
//            Assert.AreEqual(_text.String, "123456789abcdefghij");
//            Assert.AreEqual(_text.Length, "123456789abcdefghij".Length);

//            _text.String = "0123456789abcdefghij";

//            _text.RemoveCharacters(19, 20);
//            Assert.AreEqual(_text.String, "0123456789abcdefghi");
//            Assert.AreEqual(_text.Length, "0123456789abcdefghi".Length);

//            _text.String = "0123456789abcdefghij";

//            _text.RemoveCharacters(0, 10);
//            Assert.AreEqual(_text.String, "abcdefghij");
//            Assert.AreEqual(_text.Length, 10);

//            _text.RemoveCharacters(0, 10);
//            Assert.AreEqual(_text.String, "");
//            Assert.AreEqual(_text.Length, "".Length);

//            _text.String = "0123456789abcdefghij";

//            _text.RemoveCharacters(0, 20);
//            Assert.AreEqual(_text.String, "");
//            Assert.AreEqual(_text.Length, "".Length);

//            _text.String = "0123456789abcdefghij";

//            _text.RemoveCharacters(0, 11);
//            Assert.AreEqual(_text.String, "bcdefghij");
//            Assert.AreEqual(_text.Length, "bcdefghij".Length);

//            _text.String = "0123456789abcdefghij";

//            _text.RemoveCharacters(9, 20);
//            Assert.AreEqual(_text.String, "012345678");
//            Assert.AreEqual(_text.Length, "012345678".Length);

//        }

//        [Test]
//        public void TestResetText()
//        {
//            _text.String = "";
//            Assert.AreEqual(_text.Length, 0);

//            _text.String = "0123456789";
//            Assert.AreEqual(_text.Length, 10);

//            _text.String = "01234567890123456789";
//            Assert.AreEqual(_text.Length, 20);

//            Assert.Throws<ArgumentException>(() => { _text.String = "012345678901234567891"; });
//            Assert.Throws<ArgumentException>(() => { _text.String = "012345678901234567890123456789"; });
//        }
//    }
//}

using System;

namespace CTTextBox
{
    /// <summary>
    /// Data structure responsible for keeping track of the characters.
    /// </summary>
    internal class Text
    {
        /*####################################################################*/
        /*                             Variables                              */
        /*####################################################################*/

        public string String
        {
            get { return new string(_char).Substring(0, Length); }
            set { ResetText(value); }
        }

        public char[] CharacterArray
        {
            get { return _char; }
            set { ResetText(new string(value)); }
        }

        private int _lenght ;
        public int Length 
        {
            get 
            {
                return _lenght;
            }
            private set 
            {
                _lenght = value;
                if (_lenght < MaxLenght) 
                {
                    _char[_lenght] = '\0';
                }
                IsDirty = true;
            }
        }

        public int MaxLenght { get; private set; }

        public bool IsDirty { get; set; }

        private readonly char[] _char;

        /*####################################################################*/
        /*                           Initialization                           */
        /*####################################################################*/

        public Text(int maxLenght) 
        {
            MaxLenght = maxLenght;

            _char = new char[MaxLenght];

            IsDirty = true;
        }

        /*####################################################################*/
        /*                      Character Array Editing                       */
        /*####################################################################*/

        public void InsertCharacter(int location, char character)
        {
            ValidateEditRange(location, location);
            ValidateLenght(location, location, 1);

            //Validation
            if (!(Length < MaxLenght))
            {
                return;
            }

            //Shift everything right once then insert the character into the gap
            Array.Copy(
                _char, location,
                _char, location + 1,
                Length - location);

            _char[location] = character;

            Length++;

            IsDirty = true;
        }

        public void Replace(int start, int end, string replacment) 
        {
            ValidateEditRange(start, end);
            ValidateLenght(start, end, replacment.Length);

            RemoveCharacters(start, end);
            foreach (var character in replacment)
            {
                InsertCharacter(start, character);
                start++;
            }

            IsDirty = true;
        }

        public void RemoveCharacters(int start, int end) 
        {
            ValidateEditRange(start, end);

            Array.Copy(
                _char, end,
                _char, start,
                Length - end);

            Length -= end - start;

            IsDirty = true;
        }

        private void ResetText(string value) 
        {
            Length = 0;
            ValidateLenght(0, 0, value.Length);

            var x = value.IndexOf('\0');
            if (x != -1)
            {
                value = value.Substring(0, x);
            }

            Length = value.Length;

            Array.Clear(_char, 0, _char.Length);

            value.ToCharArray().CopyTo(_char, 0);

            IsDirty = true;
        }

        /*####################################################################*/
        /*                       Parameter Validation                         */
        /*####################################################################*/        

        private void ValidateEditRange(int start, int end) 
        {
            if (end > Length || start < 0 || start > end)
            {
                throw new ArgumentException("Invalid character range");
            }
        }

        private void ValidateLenght(int start, int end, int added)
        {
            if (Length - (end - start) + added > MaxLenght)
            {
                throw new ArgumentException("Character limit of " + MaxLenght +" exceeded.");
            }
        }
    }
}

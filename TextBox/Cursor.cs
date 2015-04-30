using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CTTextBox
{
    internal class Cursor 
    {
        /*####################################################################*/
        /*                             Variables                              */
        /*####################################################################*/

        public Texture2D IconTexture { get; set; }
        public Rectangle IconSource { get; set; }

        public bool CursorActive { get; set; }

        private bool _cursorToggle;
        private int _ticksPerToggle;
        private int _ticks;

        /// <summary>
        /// The current location of the cursor in the array
        /// </summary>
        public int TextCursor
        {
            get
            {
                return _textCursor;
            }
            set
            {
                _textCursor = Clamp(value, 0, _textBox.Text.Length);
            }
        }

        /// <summary>
        /// All characters between SelectedChar and the TextCursor are selected 
        /// when SelectedChar != null. Cannot be the same as the TextCursor value.
        /// </summary>
        public int? SelectedChar
        {
            get
            {
                return _selectedChar;
            }
            set
            {
                if (value.HasValue)
                {
                    if (value.Value != TextCursor)
                    {
                        _selectedChar = (short)Clamp(value.Value, 0, _textBox.Text.Length);
                    }
                }
                else
                {
                    _selectedChar = null;
                }
            }
        }

        /*####################################################################*/
        /*                             Variables                              */
        /*####################################################################*/

        private readonly TextBox _textBox;

        private int _textCursor;
        private int? _selectedChar;
        
        /*####################################################################*/
        /*                             Variables                              */
        /*####################################################################*/

        public Cursor(TextBox textBox, Texture2D iconTexture, Rectangle iconSource) 
        {
            _textBox = textBox;

            IconTexture = iconTexture;
            IconSource = iconSource;

            CursorActive = true;

            _cursorToggle = false;
            _ticksPerToggle = 30;
            _ticks = 0;
        }

        public void Update() 
        {
            _ticks++;

            if (_ticks <= _ticksPerToggle) 
            {
                return;
            }

            _cursorToggle = !_cursorToggle;
            _ticks = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_cursorToggle) { return; }

            //Top left corner of the text area
            int x = _textBox.TextRenderer.Area.X,
                y = _textBox.TextRenderer.Area.Y;

            if (TextCursor > 0) 
            {
                if (_textBox.Text.CharacterArray[TextCursor - 1] == '\n' 
                    || _textBox.Text.CharacterArray[TextCursor - 1] == '\r')
                {
                    //Beginning of next line
                    y += _textBox.TextRenderer.CharY[TextCursor - 1] + _textBox.TextRenderer.Font.LineSpacing;
                }
                else if (TextCursor == _textBox.Text.Length)
                {
                    //After last character
                    x += _textBox.TextRenderer.CharX[TextCursor - 1] + _textBox.TextRenderer.CharWidth[TextCursor - 1];
                    y += _textBox.TextRenderer.CharY[TextCursor - 1];
                } else {
                    //Beginning of current character                
                    x += _textBox.TextRenderer.CharX[TextCursor];
                    y += _textBox.TextRenderer.CharY[TextCursor];
                }
            }

            spriteBatch.Draw(
                IconTexture,
                new Rectangle(x, y, IconSource.Width, _textBox.TextRenderer.Font.LineSpacing), //User overridable function to determine this
                IconSource, 
                Color.White);
        }

        /*####################################################################*/
        /*                             Variables                              */
        /*####################################################################*/

        private static int Clamp(int value, int min, int max)
        {
            if (value > max) return max;
            if (value < min) return min;
            return value;
        }
    }
}

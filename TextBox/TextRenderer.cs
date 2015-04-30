using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CTTextBox
{
    internal class TextRenderer
    {       
        /*####################################################################*/
        /*                             Variables                              */
        /*####################################################################*/

        public Rectangle Area { get; set; }
        public SpriteFont Font { get; set; }
        public Color TextColor { get; set; }

        private TextBox _textBox;

        //Cached texture that has all of the characters
        private Texture2D _renderedText;

        //Location of the character
        internal readonly short[] CharX;
        internal readonly short[] CharY;

        //With of the character
        internal readonly byte[] CharWidth;

        //Row the character is on
        private byte[] _row;

        public TextRenderer(TextBox textBox) 
        {
            _textBox = textBox;

            CharX = new short[_textBox.Text.MaxLenght];
            CharY = new short[_textBox.Text.MaxLenght];

            CharWidth = new byte[_textBox.Text.MaxLenght];

            _row = new byte[_textBox.Text.MaxLenght];
        }

        public void Update() 
        {
            if (!_textBox.Text.IsDirty)
            {
                return;
            }

            MeasureCharacterWidths();
            _renderedText = RenderText();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_renderedText, Area, Color.White);
        }

        /*####################################################################*/
        /*                    Convert to Screen Location                      */
        /*####################################################################*/

        public int CharAt(Point localLocation)
        {
            var charRectangle = new Rectangle(0, 0, 0, Font.LineSpacing);

            var row = localLocation.Y / (Font.LineSpacing);

            for (short i = 0; i < _textBox.Text.Length; i++)
            {

                if (_row[i] != row)
                {
                    continue;
                }

                //Rectangle that encompasses the current character
                charRectangle.X = CharX[i];
                charRectangle.Y = CharY[i];
                charRectangle.Width = CharWidth[i];

                //Click on a character so put the cursor in front of it
                if (charRectangle.Contains(localLocation))
                {
                    return i;
                }

                //Next character is not on the correct row so this is the last character for this row so select it.
                if (i < _textBox.Text.Length - 1 && _row[i + 1] != row)
                {
                    return i;
                }
            }

            //Missed a character so return the end.
            return _textBox.Text.Length;
        }

        private void MeasureCharacterWidths()
        {
            for (var i = 0; i < _textBox.Text.Length; i++)
            {
                CharWidth[i] = MeasureCharacter(i);
            }
        }

        private byte MeasureCharacter(int location)
        {
            var value = _textBox.Text.String;
            var front = Font.MeasureString(value.Substring(0, location)).X;
            var end = Font.MeasureString(value.Substring(0, location + 1)).X;

            return (byte)(end - front);
        }        

        /*####################################################################*/
        /*                             Rendering                              */
        /*####################################################################*/

        private Texture2D RenderText()
        {
            var spriteBatch = new SpriteBatch(_textBox.GraphicsDevice);

            var renderTarget = new RenderTarget2D(_textBox.GraphicsDevice, Area.Width, Area.Height);
            _textBox.GraphicsDevice.SetRenderTarget(renderTarget);

            _textBox.GraphicsDevice.Clear(Color.Transparent);

            var start = 0;
            var height = 0.0f;

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            while (true)
            {
                start = RenderLine(spriteBatch, start, height);

                if (start >= _textBox.Text.Length)
                {
                    spriteBatch.End();
                    _textBox.GraphicsDevice.SetRenderTarget(null);

                    return renderTarget;
                }

                height += Font.LineSpacing;
            }
        }

        private int RenderLine(SpriteBatch spriteBatch, int start, float height)
        {
            var breakLocation = start;
            var lineLength = 0.0f;
            var row = (byte)(height / Font.LineSpacing);

            var text = _textBox.Text.String;
            string tempText;

            //Starting from end of last line loop though the characters
            for (var iCount = start; iCount < _textBox.Text.Length; iCount++)
            {
                //Calculate screen location of current character
                CharX[iCount] = (short)lineLength;
                CharY[iCount] = (short)height;
                _row[iCount] = row;

                //Calculate the width of the current line
                lineLength += CharWidth[iCount];

                //Current line is too long need to split it
                if (lineLength > Area.Width)
                {
                    if (breakLocation == start)
                    {
                        //Have to split a word
                        //Render line and return start of new line
                        tempText = text.Substring(start, iCount - start);
                        spriteBatch.DrawString(Font, tempText, new Vector2(0.0f, height), TextColor);
                        return iCount + 1;
                    }

                    //Have a character we can split on
                    //Render line and return start of new line
                    tempText = text.Substring(start, breakLocation - start);
                    spriteBatch.DrawString(Font, tempText, new Vector2(0.0f, height), TextColor);
                    return breakLocation + 1;
                }

                //Handle characters that force/allow for breaks
                switch (_textBox.Text.CharacterArray[iCount])
                {
                    //These characters force a line break
                    case '\r':
                    case '\n':
                        //Render line and return start of new line
                        tempText = text.Substring(start, iCount - start);
                        spriteBatch.DrawString(Font, tempText, new Vector2(0.0f, height), TextColor);
                        return iCount + 1;
                    //These characters are good break locations
                    case '-':
                    case ' ':
                        breakLocation = iCount + 1;
                        break;
                }
            }

            //We hit the end of the text box render line and return
            //_textData.Length so RenderText knows to return
            tempText = text.Substring(start, _textBox.Text.Length - start);
            spriteBatch.DrawString(Font, tempText, new Vector2(0.0f, height), TextColor);
            return _textBox.Text.Length;
        }
    }
}
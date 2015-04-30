using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CTTextBox
{
    public class TextBox 
    {
        public GraphicsDevice GraphicsDevice { get; set; }

        internal readonly Text Text;
        internal readonly TextRenderer TextRenderer;
        internal readonly Cursor Cursor;

        public TextBox(
            Rectangle area,
            int maxCharacters, 
            string text,
            GraphicsDevice graphicsDevice,
            SpriteFont spriteFont, 
            Texture2D cursorIcon, 
            Rectangle cursorSource) 
        {
            GraphicsDevice = graphicsDevice;

            Text = new Text(maxCharacters) 
            {
                String = text
            };

            TextRenderer = new TextRenderer(this) 
            {
                Area = area,
                Font = spriteFont,
                TextColor = Color.Black
            };


            Cursor = new Cursor(this, cursorIcon, cursorSource);
        }

        public void Update() 
        {
            TextRenderer.Update();
            Cursor.Update();
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            TextRenderer.Draw(spriteBatch);
            Cursor.Draw(spriteBatch);
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using CTInput;

namespace CTInput_Example
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        /*####################################################################*/
        /*                                Game                                */
        /*####################################################################*/

        private string
            _keyTyped = "",
            _characterTyped = "",
            _keyDown = "",
            _characterDown = "",
            _keyUp = "",
            _characterUp = "";

        GraphicsDeviceManager _graphics;        
        SpriteBatch _spriteBatch;
        SpriteFont _spriteFont;

        Input _input;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _input = new SiInput();

            _input.KeyTyped += KeyTyped;
            _input.KeyDown += KeyDown;
            _input.KeyUp += KeyUp;

            _input.MouseMoved += MouseMoved;
            _input.MouseDragged += MouseDragged;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _spriteFont = Content.Load<SpriteFont>("TestFont");
        }

        protected override void Update(GameTime gameTime)
        {
            _input.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            var topLeft = new Vector2(10, 10);
            _spriteBatch.DrawString(_spriteFont, "Key Typed = " + _keyTyped, topLeft, Color.Black);
            topLeft.Y += _spriteFont.LineSpacing;
            _spriteBatch.DrawString(_spriteFont, "Character Typed = " + _characterTyped, topLeft, Color.Black);

            topLeft.Y += _spriteFont.LineSpacing;
            _spriteBatch.DrawString(_spriteFont, "Character Down = " + _keyDown, topLeft, Color.Black);
            topLeft.Y += _spriteFont.LineSpacing;
            _spriteBatch.DrawString(_spriteFont, "Character Down = " + _characterDown, topLeft, Color.Black);

            topLeft.Y += _spriteFont.LineSpacing;
            _spriteBatch.DrawString(_spriteFont, "Character Up = " + _keyUp, topLeft, Color.Black);
            topLeft.Y += _spriteFont.LineSpacing;
            _spriteBatch.DrawString(_spriteFont, "Character Up = " + _characterUp, topLeft, Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /*####################################################################*/
        /*                          Keyboard Events                           */
        /*####################################################################*/

        private void KeyTyped(object sender, KeyboardEventArgs keyTyped) 
        {
            _keyTyped = keyTyped.Key.ToString();
            _characterTyped = keyTyped.Character.ToString();
        }

        private void KeyDown(object sender, KeyboardEventArgs keyDown)
        {
            _keyDown = keyDown.Key.ToString();
            _characterDown = keyDown.Character.ToString();
        }

        private void KeyUp(object sender, KeyboardEventArgs keyUp)
        {
            _keyUp = keyUp.Key.ToString();
            _characterUp = keyUp.Character.ToString();
        }

        /*####################################################################*/
        /*                            Mouse Events                            */
        /*####################################################################*/

        //Movement

        private void MouseMoved(object sender, MouseEventArgs mouseMoved)
        {
            mouseMoved.
        }

        private void MouseDragged(object sender, MouseEventArgs mouseDragged)
        {
        }

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using CTTextBox;

namespace TextBoxes_Example 
{
    public class Game1 : Game 
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;

        private Texture2D _testColor;

        private TextBox _textBox;

        public Game1() 
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }


        protected override void Initialize() 
        {            
            base.Initialize();

            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();

            IsMouseVisible = true;
        }

        protected override void LoadContent() 
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _testColor = new Texture2D(GraphicsDevice, 1, 1);
            _testColor.SetData(new [] { Color.LightGray } );

            _spriteFont = Content.Load<SpriteFont>("SpriteFont1");

            var str =
                "adhkfasdjklfh asjkdhf asdjklf kjladhf jkasdh fjklashfjukugheuigfahg asdjklgf asdjklgf askldgf ajklsdghfasdjklgfh asdjklhgfajklsdfh asjkldgfasdjklfh asjkldgf asjkldhfjkla gfsdhjklgf lasjkgf ljksda";
            _textBox = new TextBox(
                new Rectangle(50, 50, 500, 200),
                200,
                str,
                _graphics.GraphicsDevice,
                _spriteFont,
                _testColor,
                new Rectangle(0, 0, 1, 1));
        }

        protected override void Update(GameTime gameTime) 
        {
            base.Update(gameTime);

            _textBox.Update();
        }

        protected override void Draw(GameTime gameTime) 
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _textBox.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

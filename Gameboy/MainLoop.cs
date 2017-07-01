using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gameboy
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class MainLoop : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
        CPU cpu;
		Texture2D pixel;

        KeyboardState key;
        KeyboardState oldKey;

        string gameName;

        Dictionary<Keys, int> keyboardTranslation = new Dictionary<Keys, int>() {
            {Keys.A, 4},
            {Keys.S, 5},
            {Keys.Enter, 7},
            {Keys.Space, 6},
            {Keys.Right, 0},
            {Keys.Left, 1},
            {Keys.Up, 2},
            {Keys.Down, 3}
        };

        public MainLoop()
		{
            graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 320;
            graphics.PreferredBackBufferHeight = 288;
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.ApplyChanges();

            oldKey = Keyboard.GetState();
            key = Keyboard.GetState();

			base.Initialize();
		}

        public void SetGameName(string gameName) 
        {
            this.gameName = gameName;
        }

		/// <summary>		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{

			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			pixel = Content.Load<Texture2D>("pixel");

            cpu = new CPU();

            cpu.LoadCartridge(new Cartidge(File.ReadAllBytes("Games/" + gameName)));
			//TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
            // Exit() is obsolete on iOS
            oldKey = key;
            key = Keyboard.GetState();

#if !__IOS__ && !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
            #endif

            foreach (Keys keyToCheck in keyboardTranslation.Keys)
            {
                if (key.IsKeyDown(keyToCheck) && oldKey.IsKeyUp(keyToCheck))
                    cpu.KeyPressed(keyboardTranslation[keyToCheck]);
                else if (key.IsKeyUp(keyToCheck) && oldKey.IsKeyDown(keyToCheck))
                    cpu.KeyReleased(keyboardTranslation[keyToCheck]);
            }
            cpu.Step(gameTime.ElapsedGameTime.TotalSeconds);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            Color[] buf = cpu.GFXBuffer;
            
            for (int y = 0; y < 144; y++)
            {
                for (int x = 0; x < 160; x++)
                {
                    int position = x + (160 * y);
                    spriteBatch.Draw(pixel, new Vector2(x * 8, y * 8), buf[position]);
                }
            }
            spriteBatch.End();
			//TODO: Add your drawing code here

			base.Draw(gameTime);
		}
	}
}

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Maps.Tiled;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.ViewportAdapters;
using System.Globalization;
using Core;

namespace osx {
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game {

		int TILE_WIDTH_HALF = 32;
		int TILE_HEIGHT_HALF = 16;

		Texture2D background;

		private Camera2D _camera;
		private GraphicsDeviceManager _graphicsDeviceManager;
		private Sprite _sprite;
		private SpriteBatch _spriteBatch;
		private Texture2D _texture;
		private TiledMap _tiledMap;
		private ViewportAdapter _viewportAdapter;
		private Vector2 mouseScreenPostion;
		private Vector2 mouseOverTile;

		private MainOverlayUI mainUI;
		double lastUpdateTime = 0;
		World _world;

		public SpriteBatch spriteBatch {
			get { return _spriteBatch; }
		}

		public Game1 () {
			_graphicsDeviceManager = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize () {
			_viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
			_camera = new Camera2D(_viewportAdapter);

			Window.AllowUserResizing = true;

//			Components.Add(_fpsCounter = new FramesPerSecondCounterComponent(this));

			_world = new World ();

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent () {
			background = Content.Load<Texture2D> ("stars");

			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_texture = Content.Load<Texture2D>("highlight");
			_sprite = new Sprite(_texture) { };

			_tiledMap = Content.Load<TiledMap>("isometric_test_level");

			mainUI = new MainOverlayUI ();
			mainUI.LoadContent (Content);

		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime) {
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__ &&  !__TVOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState ().IsKeyDown (Keys.Escape))
				Exit ();
			#endif
		
			var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
			var keyboardState = Keyboard.GetState();
			var mouseState = Mouse.GetState();

			if (keyboardState.IsKeyDown(Keys.Escape))
				Exit();

			const float cameraSpeed = 200f;
			const float zoomSpeed = 0.2f;

			if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
				_camera.Move(new Vector2(0, -cameraSpeed)*deltaSeconds);

			if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
				_camera.Move(new Vector2(-cameraSpeed, 0)*deltaSeconds);

			if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
				_camera.Move(new Vector2(0, cameraSpeed)*deltaSeconds);

			if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
				_camera.Move(new Vector2(cameraSpeed, 0)*deltaSeconds);

			if (keyboardState.IsKeyDown(Keys.R))
				_camera.ZoomIn(zoomSpeed*deltaSeconds);

			if (keyboardState.IsKeyDown(Keys.F))
				_camera.ZoomOut(zoomSpeed*deltaSeconds);

			Vector2 p = _camera.ScreenToWorld(mouseState.X, mouseState.Y);
			//mouseScreenPostion = p;

			Vector2 coordinate = new Vector2 ();
			coordinate.X = (float)Math.Round((decimal)(p.X / TILE_WIDTH_HALF + p.Y / TILE_HEIGHT_HALF) / 2);
			coordinate.Y = (float)Math.Round((decimal)(p.Y / TILE_HEIGHT_HALF -(p.X / TILE_WIDTH_HALF)) /2);

			// This offset is so that the correct 0,0 is in the top middle.
			// But setting this causes the screenPosition to be off
			// So we need a few methods, one that returns the correct tile
			// one that gives the correct screen position for overlay and selection rectangles
//			coordinate.X -= 13;
//			coordinate.Y += 13;

			mouseOverTile = coordinate;

			Vector2 screen;
			screen.X = (int)(coordinate.X - coordinate.Y) * TILE_WIDTH_HALF;
			screen.Y = (int)(coordinate.X + coordinate.Y) * TILE_HEIGHT_HALF;

			mouseScreenPostion = screen;

			_sprite.Position = screen;


			double now = gameTime.TotalGameTime.TotalMilliseconds;
			double diff = now - lastUpdateTime;
			if (diff >= 50) {
				_world.Update (10);
				lastUpdateTime = now;
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime) {
			// Draw background
		
			GraphicsDevice.Clear(_tiledMap.BackgroundColor ?? Color.Black);

			_spriteBatch.Begin ();
			_spriteBatch.Draw (background, new Rectangle(0, 0, 800, 480), Color.White);
			_spriteBatch.End ();

			_spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());

			// or you can have more control over drawing each individual layer
			foreach (var layer in _tiledMap.Layers) {
				TiledTileLayer tiledTileLayer = layer as TiledTileLayer;
				TiledTile t = tiledTileLayer.GetTile (0, 24);
			    _spriteBatch.Draw(layer, _camera);
			}

			_spriteBatch.Draw(_sprite); // Draws a sprite over the cursor

			_spriteBatch.End();

			mainUI.Update (_spriteBatch, _world.WorldCalendar, gameTime);

//			var textColor = Color.White;
//			_spriteBatch.Begin(samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend);
//			_spriteBatch.DrawString(_bitmapFont, "WASD/Arrows: move", new Vector2(5, 32), textColor);
//			_spriteBatch.DrawString(_bitmapFont, "RF: zoom", new Vector2(5, 32 + _bitmapFont.LineSpacing), textColor);
//			_spriteBatch.DrawString(_bitmapFont, $"FPS: {_fpsCounter.AverageFramesPerSecond:0}", Vector2.One, Color.AliceBlue);
//			_spriteBatch.DrawString(_bitmapFont, "Tile: " + mouseOverTile.X + "," + mouseOverTile.Y, new Vector2(5, 32 + _bitmapFont.LineSpacing*2), Color.AliceBlue);
//			_spriteBatch.DrawString(_bitmapFont, "MouseOver: " + mouseScreenPostion.X + "," + mouseScreenPostion.Y, new Vector2(5, 32 + _bitmapFont.LineSpacing*3), Color.AliceBlue);
//			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}


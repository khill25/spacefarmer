using System;
using Microsoft.Xna.Framework;
using Core;

namespace osx {
	public class GameManager {

		double _simulationRate = 1.0/60.0;
		double simulationLastUpdated = 0;

		Game1 game;

		InputManager _inputManager;
		RenderingManager _renderer;

		public Core.Player playerOne = new Player ();

		public enum GameState {
			MainMenu = 0,
			Running = 1,
			Paused = 2,
			InMenu = 3,
		}

		private static GameManager _instance;
		public static GameManager Instance {
			get { 
				return _instance; 
			}
		}
		private GameManager (Game1 game) {
			this.game = game;
			_instance = this;
		}

		public void Update(GameTime gameTime) {

			_inputManager.Update (gameTime);

			double totalTime = gameTime.ElapsedGameTime.TotalMilliseconds;
			double diff = totalTime - simulationLastUpdated;
			if (diff >= _simulationRate) {
				simulationLastUpdated = totalTime;
			}
				
			_renderer.Update (game.spriteBatch, gameTime);
		}
	}
}


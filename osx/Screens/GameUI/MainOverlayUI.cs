using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Core;

namespace osx {
	public class MainOverlayUI {

		SpriteFont uiFont;

		public MainOverlayUI () {
		}

		public void LoadContent(ContentManager Content) {
			uiFont = Content.Load<SpriteFont>("arial");
		}

		public void Update(SpriteBatch spriteBatch, Core.Calendar calendar, GameTime gameTime) {
			// Draw the UI
			DrawTime (spriteBatch, calendar);
			DrawPlayerInfo (spriteBatch, null);
		}

		protected void DrawTime(SpriteBatch spriteBatch, Core.Calendar calendar) {

			spriteBatch.Begin (samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend);
			spriteBatch.DrawString (uiFont, "Year: " + calendar.CurrentYear, new Vector2(5,1), Color.White);
			spriteBatch.DrawString (uiFont, "Day: " + calendar.CurrentDay, new Vector2(5,1+uiFont.LineSpacing), Color.White);
			spriteBatch.DrawString (uiFont, "Season: " + calendar.CurrentDisplaySeason, new Vector2(5,1+uiFont.LineSpacing*2), Color.White);
			spriteBatch.DrawString (uiFont, calendar.DisplayTime, new Vector2(5,1+uiFont.LineSpacing*3), Color.White);
			spriteBatch.End ();
		
		}

		protected void DrawPlayerInfo (SpriteBatch spriteBatch, Player player) {
			DrawPlayerInventory (spriteBatch, player);
		}

		protected void DrawPlayerInventory(SpriteBatch spriteBatch, Player player) {

		}
	}
}


using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace osx {
	public interface Renderable {

		String Name { get; set;}
		long id {get;set;}
		void LoadContent (ContentManager Content);
		void Render(SpriteBatch spriteBatch, GameTime gameTime);

	}
}


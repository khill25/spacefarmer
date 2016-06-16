using System;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace osx {
	public class GameObject : Renderable {

		public Texture2D sprite;
		public float rotation;
		public Vector2 position;
		public Dictionary<long, Renderable> children = new Dictionary<long, Renderable>();

		public GameObject (String textureName, Vector2 position, float rotation) {
			
		}

		public void AddChild(Renderable child) {
			this.children.Add (child.id, child);
		}

		public Renderable RemoveChild(long childId) {
			Renderable ret = null;

			try {
				ret = this.children [childId];
				this.children.Remove (childId);
			} catch (Exception e) {
				
			}

			return ret;
		}

		#region Renderable implementation

		public string Name {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		public long id {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		public void LoadContent (Microsoft.Xna.Framework.Content.ContentManager Content) {
			throw new NotImplementedException ();
		}
		public void Render (SpriteBatch spriteBatch, Microsoft.Xna.Framework.GameTime gameTime) {
			throw new NotImplementedException ();
		}
		#endregion
	}
}


using System;
using System.Collections.Generic;
using Core;
using System.Xml.Linq;

namespace Core {
	public class TileGroup {

		public Position position;
		public int Width;
		public int Height;
		public Tile[,] tiles;

		public TileGroup (int x, int y, int w, int h) {
			this.position = new Position (x,y);
			this.Width = w;
			this.Height = h;

			this.tiles = new Tile[w, h];
			for (int i = 0; i < w; i++) {
				for (int j = 0; j < h; j++) {
					this.tiles [i, j] = new Tile(new Position(i,j));
				}				
			}
		}

		public bool AddTile(Tile t) {
			if (t == null)
				return false;
			return AddTile (t.position.X, t.position.Y, t);
		}

		public bool AddTile(int x, int y, Tile t) {

			if (t == null)
				return false;

			if (CanAddTile (x,y)) {
				this.tiles [x, y] = t;
				return true;
			} else {
				return false;
			}
		}

		public bool Contains(Tile t) {

			if (t == null)
				return false;

			if (!ValidLocation (t.position.X, t.position.Y))
				return false;

			return t.Equals(TileAt (t.position.X, t.position.Y));
		} 

		public Tile TileAt(int x, int y) {

			if (ValidLocation (x,y)) {
				return this.tiles [x, y];	
			} else {
				return null;
			}
		}

		bool ValidLocation(int x, int y) {
			return this.tiles.GetLength (0) < x && this.tiles.GetLength (1) < y;
		}

		bool CanAddTile(int x, int y) {

			if (!ValidLocation (x, y))
				return false;

			return this.tiles [x, y] == null;
		}

//		public static Position ConvertGlobalPositionToLocalPosition(TileGroup globalGroup, TileGroup localGroup, Position position) {
//			return new Position (-1, -1);
//		}
	}
}


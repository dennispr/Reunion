using Sifteo;
using Sifteo.MathExt;
using Sifteo.Util;
using System;
using System.Collections;

namespace Game
{
	/// <summary>
	/// A wrapper for cubes to be used in a maze game
	/// </summary>
	public class MazeCube
	{
		/// <summary>
		/// the cube
		/// </summary>
		public Cube cube;
		
		/// <summary>
		/// the rotation value for the cube's graphics
		/// </summary>
		public int rotation = 0;
		
		/// <summary>
		/// The owner of this cube.
		/// </summary>
		public Player owner;
		
		/// <summary>
		/// The current room to display.
		/// </summary>
		public MazeRoom currentRoom;
		
		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="cube">
		/// A <see cref="Cube"/>
		/// </param>
		public MazeCube (Cube cube)
		{
			this.cube = cube;
			cube.userData = this;
		}
		
		/// <summary>
		/// Draws the room.
		/// </summary>
		public void PaintCube()
		{
			if (currentRoom != null)
			{
				currentRoom.DrawTo(this.cube);
				// draw the occupant, if any
				if (currentRoom.occupant != null)
					currentRoom.occupant.DrawTo(new Int2((Cube.SCREEN_WIDTH-Character.pixelSize.x)/2,(Cube.SCREEN_HEIGHT-Character.pixelSize.y)/2), this.cube);
			}
			else
			{
				cube.FillScreen(Color.Black);
			}
			cube.Paint();
		}
		
		/// <summary>
		/// Determines whether this instance is chained to a player.
		/// </summary>
		/// <returns>
		/// <c>true</c> if this instance is chained to a player; otherwise, <c>false</c>.
		/// </returns>
		public bool IsChainedToPlayer()
		{
			if (owner != null) return true;
			Cube[] network = CubeHelper.FindConnected(cube);
			bool isNetworked = false;
			MazeCube mc;
			foreach (Cube c in network)
			{
				mc = (MazeCube)c.userData;
				if (mc.owner != null) isNetworked = true;
			}
			return isNetworked;
		}
	}
}


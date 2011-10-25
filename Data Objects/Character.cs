using Sifteo;
using Sifteo.MathExt;
using System;

namespace Game
{
	/// <summary>
	/// Character.
	/// </summary>
	public class Character
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Game.Character"/> class.
		/// </summary>
		public Character ()
		{
		}
		
		/// <summary>
		/// The current room the character is in.
		/// </summary>
		private MazeRoom _currentRoom;
		/// <summary>
		/// Gets the current room.
		/// </summary>
		/// <value>
		/// The current room.
		/// </value>
		public MazeRoom currentRoom { get { return _currentRoom; } }
		
		/// <summary>
		/// Occupies the specified room.
		/// </summary>
		/// <param name='room'>
		/// Room.
		/// </param>
		public void OccupyRoom(MazeRoom room)
		{
			_currentRoom = room;
			room.occupant = this;
		}
		
		/// <summary>
		/// The size of the character in pixels.
		/// </summary>
		public static readonly Int2 pixelSize = new Int2(Cube.SCREEN_WIDTH/4, Cube.SCREEN_HEIGHT/4);
		
		/// <summary>
		/// Draws to the specified cube at the specified position.
		/// </summary>
		/// <param name='position'>
		/// Position (from bottom left).
		/// </param>
		/// <param name='cube'>
		/// Cube.
		/// </param>
		public virtual void DrawTo(Int2 position, Cube cube)
		{
			cube.FillRect(new Color(255, 128, 128), position.x, position.y, pixelSize.x, pixelSize.y);
		}
	}
}


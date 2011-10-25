using Sifteo;
using Sifteo.MathExt;
using System;

namespace Game
{
	/// <summary>
	/// A room in the maze
	/// </summary>
	public class MazeRoom
	{
		/// <summary>
		/// An enum to describe whether a side is open or closed
		/// </summary>
		public enum EntryState { Closed, Open };
		
		/// <summary>
		/// The entry states for the sides of the room
		/// </summary>
		private EntryState[] entryStates = new EntryState[4];
		
		/// <summary>
		/// The neighbors, if any
		/// </summary>
		public MazeRoom[] neighbors = new MazeRoom[4];
		
		/// <summary>
		/// The size of open segments
		/// </summary>
		private static readonly int segmentSize = Cube.SCREEN_WIDTH/3;
		
		/// <summary>
		/// The color of the background.
		/// </summary>
		private static readonly Color bgColor = Color.Black;
		/// <summary>
		/// The color of open passages.
		/// </summary>
		private static readonly Color passageColor = Color.White;
		
		/// <summary>
		/// The position of the room in the maze.
		/// </summary>
		public Int2 position = new Int2();
		
		/// <summary>
		/// The occupant of the room, if any.
		/// </summary>
		public Character occupant;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Game.MazeRoom"/> class.
		/// </summary>
		public MazeRoom()
		{
			
		}
		
		/// <summary>
		/// Sets the neighbor to another room on the specified side.
		/// </summary>
		/// <param name='otherRoom'>
		/// The neighbor room.
		/// </param>
		/// <param name='side'>
		/// The side to which the neighbor room should be attached.
		/// </param>
		/// <param name='connectionState'>
		/// Specifies whether the neighboring room should be open or closed for access.
		/// </param>
		public void SetNeighbor(MazeRoom otherRoom, Cube.Side side, EntryState connectionState)
		{
			this.neighbors[(int)side] = otherRoom;
			this.entryStates[(int)side] = (otherRoom==null)?EntryState.Closed:connectionState;
		}
		
		/// <summary>
		/// Draws to the supplied cube.
		/// </summary>
		/// <param name='cube'>
		/// The cube to which the room should be drawn.
		/// </param>
		public void DrawTo(Cube cube)
		{
			// draw the background
			cube.FillScreen(bgColor);
						
			// draw the center
			cube.FillRect(passageColor, segmentSize, segmentSize, segmentSize, segmentSize);
			
			// draw open passages
			for (int i=0; i<entryStates.Length; i++)
			{
				if (entryStates[i] == EntryState.Closed) continue;
				int x=segmentSize, y=segmentSize;
				switch ((Cube.Side)i)
				{
				case Cube.Side.BOTTOM:
					y = 2*segmentSize;
					break;
				case Cube.Side.LEFT:
					x = 0;
					break;
				case Cube.Side.RIGHT:
					x = 2*segmentSize;
					break;
				case Cube.Side.TOP:
					y = 0;
					break;
				}
				cube.FillRect(passageColor, x, y, segmentSize, segmentSize);
			}
			
			// paint the cube
			cube.Paint();
		}
		
		/// <summary>
		/// Gets the entry state of the supplied side
		/// </summary>
		/// <returns>
		/// The entry state of of the supplied side
		/// </returns>
		/// <param name='side'>
		/// The side of the cube in question
		/// </param>
		public EntryState GetEntryStateOf(Cube.Side side)
		{
			return entryStates[(int)side];
		}
		
		public override string ToString ()
		{
			return string.Format ("[MazeRoom] at ({0}, {1})", position.x, position.y);
		}
	}
}


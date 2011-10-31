using Sifteo;
using Sifteo.MathExt;
using System;

namespace Game
{
	/// <summary>
	/// Manage the internal state of the game's maze.
	/// </summary>
	public class MazeManager
	{
		/// <summary>
		/// Singleton.
		/// </summary>
		public static MazeManager use;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Game.MazeManager"/> class.
		/// </summary>
		public MazeManager()
		{
			use = this;
		}
		
		/// <summary>
		/// Gets the rooms.
		/// </summary>
		/// <value>
		/// The rooms.
		/// </value>
		public MazeRoom[,] rooms { get { return _rooms; } }
		/// <summary>
		/// The rooms in the maze.
		/// </summary>
		private MazeRoom[,] _rooms;
		
		public void MakeMaze(int height, int width){
			Log.Debug("making maze");
			_rooms = new MazeRoom[height, width];
			for(int y = 0; y<height; y++){
				for(int x = 0; x<width; x++){
					_rooms[x,y] = new MazeRoom();
					_rooms[x,y].position.x = x;
					_rooms[x,y].position.y = y;
				}
			}
		}
		
		public void setMazeRoom(int x, int y, string type){
			Log.Debug("setting room "+x+" "+y);
			Log.Debug("You can go:");
			if(type.IndexOf('n') != -1){
				//north!
				Log.Debug("North");
				_rooms[x,y].SetNeighbor(_rooms[x,y-1], Cube.Side.TOP, MazeRoom.EntryState.Open);
			}
			if(type.IndexOf('s') != -1){
				//south!
				Log.Debug("South");
				_rooms[x,y].SetNeighbor(_rooms[x,y+1], Cube.Side.BOTTOM, MazeRoom.EntryState.Open);
			}
			if(type.IndexOf('e') != -1){
				//east!
				Log.Debug("East");
				_rooms[x,y].SetNeighbor(_rooms[x+1,y], Cube.Side.RIGHT, MazeRoom.EntryState.Open);
			}
			if(type.IndexOf('w') != -1){
				//west!
				Log.Debug("West");
				_rooms[x,y].SetNeighbor(_rooms[x-1,y], Cube.Side.LEFT, MazeRoom.EntryState.Open);
			}
		}
		
		/// <summary>
		/// Generates the random maze.
		/// </summary>
		/// <param name='length'>
		/// How long should the maze be?
		/// </param>
		public void GenerateRandomMaze(int length)
		{
			// initialize the grid
			_rooms = new MazeRoom[length, length];
			
			// for now just make a straight line and start on the left
			for (int i=0; i<length; i++)
			{
				_rooms[i,0] = new MazeRoom();
				_rooms[i,0].position.x = i;
				_rooms[i,0].position.y = 0;
			}
			
			// for now just connect all adjacent _rooms
			for (int c=0; c<=_rooms.GetUpperBound(0); c++)
			{
				for (int r=0; r<=_rooms.GetUpperBound(1); r++)
				{
					if (_rooms[c,r] == null) continue;
					if (c>0)
						_rooms[c,r].SetNeighbor(_rooms[c-1,r], Cube.Side.LEFT, MazeRoom.EntryState.Open);
					if (c<_rooms.GetUpperBound(0))
						_rooms[c,r].SetNeighbor(_rooms[c+1,r], Cube.Side.RIGHT, MazeRoom.EntryState.Open);
					if (r>0)
						_rooms[c,r].SetNeighbor(_rooms[c,r-1], Cube.Side.BOTTOM, MazeRoom.EntryState.Open);
					if (r<=_rooms.GetUpperBound(1))
						_rooms[c,r].SetNeighbor(_rooms[c,r+1], Cube.Side.TOP, MazeRoom.EntryState.Open);
				}
			}
		}
		
		/// <summary>
		/// Move in the specified direction.
		/// </summary>
		/// <param name='direction'>
		/// Direction to move.
		/// </param>
		/*
		public void Move(Cube.Side direction)
		{
			if (_currentRoom.GetEntryStateOf(direction) == MazeRoom.EntryState.Closed) return;
			switch (direction)
			{
			case Cube.Side.BOTTOM:
				if (_currentRoom.position.y > 0)
					_currentRoom = _rooms[_currentRoom.position.x, _currentRoom.position.y-1];
				break;
			case Cube.Side.LEFT:
				if (_currentRoom.position.x > 0)
					_currentRoom = _rooms[_currentRoom.position.x-1, _currentRoom.position.y];
				break;
			case Cube.Side.RIGHT:
				if (_currentRoom.position.x < _rooms.GetUpperBound(0))
					_currentRoom = _rooms[_currentRoom.position.x+1, _currentRoom.position.y];
				break;
			case Cube.Side.TOP:
				if (_currentRoom.position.y < _rooms.GetUpperBound(1))
					_currentRoom = _rooms[_currentRoom.position.x, _currentRoom.position.y+1];
				break;
			}
		}*/
	}
}


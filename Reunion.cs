using Sifteo;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Game
{
	public class Reunion : BaseApp
	{
		/// <summary>
		/// application framerate
		/// </summary>
	    override public int FrameRate
	    {
			get { return 30; }
	    }
		
		/// <summary>
		/// singleton
		/// </summary>
		public static Reunion use;
		
		/// <summary>
		/// Constant number of players.
		/// </summary>
		const int numPlayers = 2;
		
		/// <summary>
		/// The cubes as MazeCubes.
		/// </summary>
		List<MazeCube> mazeCubes = new List<MazeCube>();
		
		/// <summary>
		/// The players.
		/// </summary>
		List<Player> players = new List<Player>();
		
		/// <summary>
		/// The player cube.
		/// </summary>
		List<MazeCube>playerCubes = new List<MazeCube>();
	
	    /// <summary>
	    /// Initialize
	    /// </summary>
	    override public void Setup ()
		{
			// initialize singleton
			use = this;
			
			// initialize events
			CubeSet.NeighborAddEvent += OnNeighborAdd;
			CubeSet.NeighborRemoveEvent += OnNeighborRemove;
			
			// initialize MazeCubes
			foreach (Cube cube in CubeSet)
			{
				mazeCubes.Add(new MazeCube(cube));
			}
			
			// create players
			for (int i=0; i<numPlayers; i++)
			{
				players.Add(new Player(i));
				playerCubes.Add(mazeCubes[i]);
			}
			
			// instantiate a MazeManager and generate a level
			new MazeManager();
			MazeManager.use.GenerateRandomMaze(3);
			
			// put the players in rooms
			List<MazeRoom> validRooms = new List<MazeRoom>();
			for (int c=0; c<MazeManager.use.rooms.GetUpperBound(0); c++)
			{
				for (int r=0; r<MazeManager.use.rooms.GetUpperBound(1); r++)
				{
					if (MazeManager.use.rooms[c,r] == null) continue;
					validRooms.Add(MazeManager.use.rooms[c,r]);
				}
			}
			for (int i=0; i<players.Count; i++)
			{
				while (players[i].currentRoom == null)
				{
					int j = (new Random()).Next(0, validRooms.Count);
					if (validRooms[j].occupant == null)
					{
						players[i].OccupyRoom(validRooms[j]);
						playerCubes[i].owner = players[i];
					}
				}
			}
			foreach (MazeCube c in playerCubes)
			{
				c.currentRoom = c.owner.currentRoom;
			}
			
			// draw the cubes to start with
			foreach (MazeCube c in mazeCubes)
			{
				c.PaintCube();
			}
		}
		
		/// <summary>
		/// Called once per frame
		/// </summary>
	    override public void Tick()
	    {
			
	    }
		
		/// <summary>
		/// Set the current room of the neighbor.
		/// </summary>
		/// <param name='cube1'>
		/// Cube1.
		/// </param>
		/// <param name='side1'>
		/// Side1.
		/// </param>
		/// <param name='cube2'>
		/// Cube2.
		/// </param>
		/// <param name='side2'>
		/// Side2.
		/// </param>
		void OnNeighborAdd(Cube cube1, Cube.Side side1, Cube cube2, Cube.Side side2)
		{
			MazeCube visible=null, empty=null;
			Cube.Side visibleSide;
			MazeCube c1 = (MazeCube)cube1.userData;
			MazeCube c2 = (MazeCube)cube2.userData;
			
			// early out if both cubes are already showing a room
			if (c1.currentRoom!=null && c2.currentRoom!= null) return;
			
			// the visible cube is the one showing a room already
			if (c1.currentRoom != null)
			{
				visible = c1; empty = c2;
				visibleSide = side1;
			}
			else
			{
				visible = c2; empty = c1;
				visibleSide = side2;
			}
			
			// orient to the visible cube and draw the room
			empty.cube.OrientTo(visible.cube);
			if (visible.currentRoom == null) return;
			empty.currentRoom = visible.currentRoom.neighbors[(int)visibleSide];
			empty.PaintCube();
			if (empty.currentRoom == null) return;
			
			// success if they match up
			if (empty.currentRoom.occupant != null &&
				empty.currentRoom.occupant.GetType() == typeof(Player) &&
				visible.currentRoom.occupant != empty.currentRoom.occupant
				)
			{
				Log.Debug("Great Success!");
				Sounds.CreateSound("Submarine"); // TODO: For some reason this sound is not actually playing for me
			}
		}
		
		/// <summary>
		/// Clear the current room of the neighbor if not chained to a player cube.
		/// </summary>
		/// <param name='cube1'>
		/// Cube1.
		/// </param>
		/// <param name='side1'>
		/// Side1.
		/// </param>
		/// <param name='cube2'>
		/// Cube2.
		/// </param>
		/// <param name='side2'>
		/// Side2.
		/// </param>
		void OnNeighborRemove(Cube cube1, Cube.Side side1, Cube cube2, Cube.Side side2)
		{
			// clear cubes no longer chained to a player
			MazeCube mc = (MazeCube)cube1.userData;
			if (!mc.IsChainedToPlayer())
			{
				mc.currentRoom = null;
				mc.PaintCube();
			}
			mc = (MazeCube)cube2.userData;
			if (!mc.IsChainedToPlayer())
			{
				mc.currentRoom = null;
				mc.PaintCube();
			}
		}
	
	    // development mode only
	    // start Reunion as an executable and run it, waiting for Siftrunner to connect
	    static void Main(string[] args) { new Reunion().Run(); }
	}
}
using Sifteo;
using Sifteo.MathExt;
using System;

namespace Game
{
	/// <summary>
	/// Player.
	/// </summary>
	public class Player : Character
	{
		/// <summary>
		/// The identifier.
		/// </summary>
		private int _id;
		/// <summary>
		/// Gets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public int id { get { return _id; } }
				
		/// <summary>
		/// Initializes a new instance of the <see cref="Game.Player"/> class.
		/// </summary>
		/// <param name='id'>
		/// Identifier.
		/// </param>
		public Player (int id)
		{
			this._id = id;
		}
		
		/// <summary>
		/// Draws to the specified cube at the specified position.
		/// </summary>
		/// <param name='position'>
		/// Position (from bottom left).
		/// </param>
		/// <param name='cube'>
		/// Cube.
		/// </param>
		public override void DrawTo(Int2 position, Cube cube)
		{
			base.DrawTo(position, cube);
			switch (id)
			{
			case 0:
				cube.FillRect(new Color(128,128,255), Cube.SCREEN_WIDTH/2, Cube.SCREEN_HEIGHT/2-10, 1, 20);
				break;
			case 1:
				cube.FillRect(new Color(128,128,255), Cube.SCREEN_WIDTH/2-4, Cube.SCREEN_HEIGHT/2-10, 1, 20);
				cube.FillRect(new Color(128,128,255), Cube.SCREEN_WIDTH/2+4, Cube.SCREEN_HEIGHT/2-10, 1, 20);
				break;
			}
		}
	}
}


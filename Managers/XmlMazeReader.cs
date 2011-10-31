using System;
using System.Xml;
using Sifteo;
using System.Collections;
using System.Collections.Generic;
namespace Game
{
	public class XmlMazeReader
	{
		public static XmlMazeReader use;
		
		public XmlMazeReader (){
			use = this;
		}
		
		public void setUpMap(string path, MazeManager maze, List<Player> players){
			bool onHeight = false; 
			bool onWidth = false; 
			bool onRooms = false;
			bool onPlayer1 = false;
			bool onPlayer2 = false;
			
			int height=0;
			int width=0;
			int currentRow=0;
			List<MazeRoom> validRooms = new List<MazeRoom>();
			XmlTextReader reader = new XmlTextReader(path);
			
			//Log.Debug("Great Success!");
			
			while (reader.Read()){
				switch (reader.NodeType){
					case XmlNodeType.Element:
						if(reader.Name == "height"){
							onHeight = true;
						}
						if(reader.Name == "width"){
							onWidth = true;
						}
						if(reader.Name == "player1"){
							onPlayer1 = true;
						}
						if(reader.Name == "player2"){
							onPlayer2 = true;
						}
            		break;
						
  					case XmlNodeType.Text:
   	    				//Console.WriteLine (reader.Value);
						if(onHeight){
							height = int.Parse(reader.Value);
							onHeight = false;
						}
						if(onWidth){
							width = int.Parse(reader.Value);
							onWidth = false;
						}
						if(onRooms){
							string[]  roomVals = reader.Value.Split(new char[]{','},2);
							for(int x = 0; x<width; x++){
								maze.setMazeRoom(x,currentRow,roomVals[x]);
								validRooms.Add(MazeManager.use.rooms[x,currentRow]);
							}
						}
						if(onPlayer1){
							string[]  roomVals = reader.Value.Split(new char[]{','},2);
							Log.Debug("Setting player 1 to "+roomVals[0]+","+roomVals[1]);
							//players[0].OccupyRoom(maze.rooms[int.Parse(roomVals[0]),int.Parse(roomVals[1])]);
							players[0].OccupyRoom(maze.rooms[0,0]);
						}
						if(onPlayer2){
							string[]  roomVals = reader.Value.Split(new char[]{','},2);
							Log.Debug("Setting player 1 to "+roomVals[0]+","+roomVals[1]);
							players[1].OccupyRoom(maze.rooms[1,1]);
						}
       				break;
					
  					case XmlNodeType. EndElement:
						if(reader.Name == "height"){
							onHeight = false;
						}
						if(reader.Name == "width"){
							onWidth= false;
							onRooms = true;
							maze.MakeMaze(height,width);
						}
						if(reader.Name == "row"){
							currentRow++;
							if(currentRow>=height){
								onRooms=false;
								Log.Debug("Done with rooms");
							}
						}
						if(reader.Name == "player1"){
							onPlayer1 = false;
						}
						if(reader.Name == "player2"){
							onPlayer2 = false;
						}
						
       				break;
   				}
			}
		}
		
	}
}


using UnityEngine;
using System.Collections;

public class World {

	WorldController wc;
	
	Tile[,] tiles;
	int size;
	
	public World(WorldController wc, int size)
	{
		//Debug.Log ("Create a new World of size "+ size);
		tiles = new Tile[size, size];
		this.size = size;
		
		// Create Floor:
		for (int i_x = 1; i_x < size-1; i_x++)
		{
			for (int i_y = 1; i_y < size-1; i_y++)
			{
				tiles[i_x, i_y] = new Tile(this, i_x, i_y, Tile.Tiletype.Floor);
			}
		}
		// Create Wall:
		for (int i=0; i < size; i++)
		{
			Tile.Tiletype t = Tile.Tiletype.Wall;
			tiles[i,0] = new Tile(this, i, 0, t);
			tiles[i,size-1] = new Tile(this, i, size-1, t);
			if (i == size/2)
			{
				t = Tile.Tiletype.Door;
			}
			tiles[0,i] = new Tile(this, 0, i, t);
			tiles[size-1,i] = new Tile(this, size-1, i, t);
		}
	}
	
	public int get_size()
	{
		return size;
	}
	
	public Tile get_tile_at(int pos_x, int pos_y)
	{
		return tiles[pos_x, pos_y];
	}
	
	
}

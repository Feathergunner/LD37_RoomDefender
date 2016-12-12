using UnityEngine;
using System.Collections;

public class Tile{

	public enum Tiletype {Floor, Wall, Door, Lava}

	World world;
	Tiletype tiletype;
	
	int pos_x;
	int pos_y;
	
	public Tile(World w, int x, int y, Tiletype t)
	{
		this.world = w;
		this.pos_x = x;
		this.pos_y = y;
		this.tiletype = t;
	}
	
	public int get_x()
	{
		return pos_x;
	}
	
	public int get_y()
	{
		return pos_y;
	}
	
	public Tiletype get_type()
	{
		return tiletype;
	}
}

using UnityEngine;
//using System.Collections;
using System;
using System.Collections.Generic;

public class Entity : MonoBehaviour {

	public static List<Entity> all_entities = new List<Entity>();

	World world;
	protected Vector3 move_direction;
	protected float speed;
	protected int points;
	
	protected Tile.Tiletype[] allowed_tiles;
	
	protected int max_life;
	protected int cur_life;
	
	protected bool got_hit;
	
	protected Animator animator;

	// Use this for initialization
	protected void Start () {
		got_hit = false;
		animator = GetComponent<Animator>();
		
		init();
	}
	
	// Update is called once per frame
	protected void Update ()
	{
		move_direction.Normalize();
		Vector3 move = new Vector3(move_direction.x*speed*Time.deltaTime, move_direction.y*speed*Time.deltaTime, 0);
		
		float target_x = transform.position.x + move.x;
		float target_y = transform.position.y + move.y;
		
		if (target_x < 0 || target_x > world.get_size())
		{
			move.x = 0;
			target_x = transform.position.x;
		}
		if (target_y < 0 || target_y > world.get_size())
		{
			move.y = 0;
			target_y = transform.position.y;
		}
		
		Vector3 target = new Vector3(target_x, target_y, -1);
		
		// check if target tile is legit:
		if (Array.Exists(allowed_tiles, element => element == world.get_tile_at((int)(target_x+0.5*(1+move_direction.x)), (int)(target_y+0.5*(1+move_direction.y))).get_type()))
		{
			// check if target position is not occupied by any other entity:
			bool target_is_free = true;
			foreach (Entity e in all_entities)
			{
				if (e != this)
				{
					float dist = Vector3.Distance(e.transform.position, target);
					//Debug.Log (dist);
					if (dist < 0.5)
					{
						target_is_free = false;
						break;
					}
				}
			}
			if (target_is_free)
			{
				transform.Translate(move);
			}
		}
	}
	
	public void set_world(World w)
	{
		world = w;
	}
	
	protected void apply_area_damage(int dmg, float distance)
	{
		List<Entity> enteties_to_remove = new List<Entity>();
		foreach (Entity e in all_entities)
		{
			if (e != this)
			{
				float dist = Vector3.Distance(e.transform.position, this.transform.position);
				//Debug.Log (dist);
				if (dist < distance)
				{
					points++;
					// Debug.Log(e.name + " wird zerstört!");
					 enteties_to_remove.Add(e);
					//all_entities.Remove(e);
					
				}
			}
		}
		foreach (Entity e in enteties_to_remove)
		{
			e.get_damage(dmg);
			// e.GetComponent<SpriteRenderer>().enabled = false;
			// all_entities.Remove(e);
			// Destroy(e);
		}
	}
	
	public void get_damage(int dmg)
	{
		cur_life -= dmg;
		if (cur_life <= 0)
		{
			on_death();
		}
	}
	
	protected virtual void on_death()
	{
		Debug.Log("Empty on_death of superclasss");
	}
	
	public void init()
	{
		cur_life = max_life;
		move_direction = new Vector3(0,0,0);
		points = 0;
	}
	
	public int get_points()
	{
		return points;
	}
	
}

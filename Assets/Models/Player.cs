using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : Entity{

	public Text display_life;
	public Text display_points;
	
	int attack;
	int defense;
	
	float attack_fire_dist = 1.7f;
	float attack_fire_cooldown = 0.5f;
	float attack_fire_timer;
	
	WorldController wc;
	
	void Start()
	{
		max_life = 20;
		speed = 8;
		attack_fire_timer = attack_fire_cooldown;
		
		allowed_tiles = new Tile.Tiletype[] {Tile.Tiletype.Floor};
		base.Start();
	}
	
	void Update()
	{
		// get attacks etc:
		bool action_attack;
		if (Input.GetKeyDown(KeyCode.Space) && attack_fire_timer >= attack_fire_cooldown)
		{
			action_attack = true;
			attack_fire_timer = 0;
			
			apply_area_damage(1,attack_fire_dist);
		}
		else
		{
			action_attack = false;
		}
		animator.SetBool("action_attack", action_attack);
		
		
		animator.SetBool("get_hit", got_hit);
		got_hit = false;

		// increase timer:
		attack_fire_timer += Time.deltaTime;
		
		// update displays:
		display_life.text = "Life: "+cur_life.ToString() +"/"+max_life.ToString();
		display_points.text = "Points: "+points.ToString();
		
		
		// get movement:
		float dx = Input.GetAxisRaw("Horizontal");
		float dy = Input.GetAxisRaw("Vertical");
		
		move_direction = new Vector3(dx, dy, 0);
		base.Update();
	}
	
	override protected void on_death()
	{
		// Debug.Log("GAME OVER!");
		wc.end_game();
	}
	
	public void set_wc(WorldController wc)
	{
		this.wc = wc;
	}
}
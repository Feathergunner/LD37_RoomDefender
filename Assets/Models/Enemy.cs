using UnityEngine;
using System.Collections;

public class Enemy : Entity {

	Entity target;
	
	float range = 1.0f;
	float attack_cooldown = 1.0f;
	float attack_timer;

	// Use this for initialization
	void Start ()
	{
		max_life = 1;
		speed = 4;
		attack_timer = attack_cooldown;
		
		allowed_tiles = new [] {Tile.Tiletype.Floor, Tile.Tiletype.Door};
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		move_direction.x = (target.transform.position.x - transform.position.x);
		move_direction.y = (target.transform.position.y - transform.position.y);
		
		bool action_attack = false;
		if (attack_timer >= attack_cooldown && Vector3.Distance(target.transform.position, this.transform.position) < range)
		{
			action_attack = true;
			target.get_damage(1);
			attack_timer = 0f;
		}
		animator.SetBool("action_attack", action_attack);
		
		// increase timer:
		attack_timer += Time.deltaTime;
		
		base.Update();
	}
	
	public void set_target(Entity t)
	{
		target = t;
	}
	
	override protected void on_death()
	{
		//GetComponent<Animator>().enabled = false;
		Entity.all_entities.Remove(this);
		Destroy(gameObject);
	}
}

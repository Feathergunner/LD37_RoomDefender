using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public WorldController wc;
	public GameObject enemy_prefab;
	int enemy_counter;
	float spawn_cooldown;
	float spawn_timer;
	bool active;

	// Use this for initialization
	void Start () {
		enemy_counter = 1;
		active = false;
		spawn_timer = 0;
						
		//InvokeRepeating ("Spawn", 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (active && spawn_timer >= spawn_cooldown)
		{
			Spawn();
			spawn_timer = 0;
			if (spawn_cooldown > 1)
			{
				spawn_cooldown -= 0.1f;
			}
			else if (spawn_cooldown > 0.5)
			{
				spawn_cooldown -= 0.05f;
			}
		}
		// increase timer:
		spawn_timer += Time.deltaTime;
	}
	
	void Spawn ()
	{
		Vector3[] spawnpoints = new [] {new Vector3(0,7,-1), new Vector3(14,7,-1)};
		int randspawn = (int)(Random.value *2);
		
		GameObject enemy = (GameObject)Instantiate(enemy_prefab, spawnpoints[randspawn], enemy_prefab.transform.rotation);
		string enemy_name = "enemy_"+enemy_counter;
		enemy.name = enemy_name;
		GameObject enemy_go = GameObject.Find(enemy_name);
		
		Enemy enemy_script = enemy_go.GetComponent<Enemy>();
		enemy_script.set_world(wc.get_world());
		enemy_script.set_target(wc.get_player());
		enemy_counter++;
		
		Entity.all_entities.Add(enemy_script);
	}
	
	public void init()
	{
		spawn_cooldown = 2;
		active = true;		
	}
	
	public void stop()
	{
		active = false;
	}
	
}

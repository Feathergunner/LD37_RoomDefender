using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WorldController : MonoBehaviour {

	public Sprite sprite_floor;
	public Sprite sprite_wall;
	public Sprite sprite_door;
	public Sprite sprite_lava;
	
	public GameObject player_prefab;

	public int world_size;
	
	public Camera camera;
	
	public RectTransform main_menu;
	public Text display_final_score;
	
	public Text display_life;
	public Text display_points;

	World world;
	
	Player player_script;
	
	int tilesize = 32;
	
	// Use this for initialization
	void Start () {
		world = new World(this, world_size);
		
		// Create GameObjects for tiles:
		for (int i_x = 0; i_x < world.get_size(); i_x++)
		{
			for (int i_y = 0; i_y < world.get_size(); i_y++)
			{
				// Debug.Log ("Creating object " +i_x+"/"+i_y);
				Tile tile_data = world.get_tile_at(i_x, i_y);
				GameObject tile_go = new GameObject();
				
				tile_go.name = "Tile_" + i_x + "_" + i_y;
				tile_go.transform.position = new Vector3(tile_data.get_x(), tile_data.get_y(), 0);
				
				SpriteRenderer tile_sr = tile_go.AddComponent<SpriteRenderer>();
				tile_sr.sprite = get_sprite_by_type(tile_data.get_type());
			}
		}
		// create Player:
		Vector3 spawn_player = new Vector3(world_size/2, world_size/2, -1);
		GameObject player = (GameObject)Instantiate(player_prefab, spawn_player, player_prefab.transform.rotation);
		player.name = "PLAYER";
		GameObject player_go = GameObject.Find ("PLAYER");
		player_script = player_go.GetComponent<Player>();
		player_script.set_world(world);
		player_script.display_life = this.display_life;
		player_script.display_points = this.display_points;
		player_script.set_wc(this);
		
		Entity.all_entities.Add(player_script);
				
		// set camera position:
		Vector3 cam_position = new Vector3(world_size/2, world_size/2, -10);
		camera.transform.Translate(cam_position);
		camera.orthographicSize = world_size/2 + 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape))
			Application.Quit();
	}
	
	public void start_game()
	{
		// init player:
		player_script.init();
		
		// hide main menu:
		main_menu.gameObject.SetActive(false);
		
		// start enemy spawning:
		EnemyController ec = gameObject.GetComponent<EnemyController>();
		ec.init();
	}
	
	public void end_game()
	{
		// stop enemy spawning:
		EnemyController ec = gameObject.GetComponent<EnemyController>();
		ec.stop();
		
		// delete all existing enemies:
		List<Entity> enteties_to_remove = new List<Entity>();
		foreach (Entity e in Entity.all_entities)
		{
			if (e != player_script)
			{
				enteties_to_remove.Add(e);
			}
		}
		
		foreach (Entity e in enteties_to_remove)
		{
			e.GetComponent<SpriteRenderer>().enabled = false;
			Entity.all_entities.Remove(e);
			Destroy(e.gameObject);
		}
		
		// display main menu and final score:
		main_menu.gameObject.SetActive(true);
		display_final_score.text = "FINAL SCORE: " + player_script.get_points();
	}

	
	Sprite get_sprite_by_type(Tile.Tiletype t)
	{
		if (t == Tile.Tiletype.Floor)
			return sprite_floor;
		if (t == Tile.Tiletype.Wall)
			return sprite_wall;
		if (t == Tile.Tiletype.Door)
			return sprite_door;
		if (t == Tile.Tiletype.Lava)
			return sprite_lava;
		else return sprite_wall;
	}
	
	public World get_world()
	{
		return world;
	}
	
	public Player get_player()
	{
		return player_script;
	}
	
	public void quit_game()
	{
		Application.Quit();
	}
}

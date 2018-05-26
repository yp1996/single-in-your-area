using Godot;
using System;

public class CollectibleSpawner : Node2D
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

	private Timer spawnTimer;
	private int maxSpawnable = 10;
	private int minSpawnable = 3;
	private int maxSpawnX = 800;
	private int maxSpawnY = 400;
	private int maxItems = 15; 
	PackedScene f;
	PackedScene x;
	
    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		spawnTimer = (Timer) GetNode("SpawnTimer");
		f = (PackedScene) ResourceLoader.Load("res://Scenes/Items/FentaLeanCollectible.tscn");
		x = (PackedScene) ResourceLoader.Load("res://Scenes/Items/XanaxCollectible.tscn");
        Spawn();
    }
	
	private void Spawn() {
		Random rnd = new Random();
		int numSpawned = rnd.Next(minSpawnable, maxSpawnable);
		for (int i = 0; i < numSpawned; i++) {
			int type = rnd.Next(0, 2);
			if (type == 0) {
				FentaLeanCollectible fc = (FentaLeanCollectible) f.Instance();
				AddChild(fc);
				fc.SetPosition(new Vector2(rnd.Next(0, maxSpawnX), rnd.Next(0, maxSpawnY)));
			} else {
				XanaxCollectible xc = (XanaxCollectible) x.Instance();
				AddChild(xc);
				xc.SetPosition(new Vector2(rnd.Next(0, maxSpawnX), rnd.Next(0, maxSpawnY)));
			}
		}
	}
	
	private void OnSpawnTimerTimeout()
	{
	    // Replace with function body
		int numItems = GetTree().GetNodesInGroup("Collectibles").Length;
		if (numItems < maxItems) {
			Spawn();
		}
	}

}



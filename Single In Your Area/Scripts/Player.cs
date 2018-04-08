using Godot;
using System;

public class Player : KinematicBody2D
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";
	const int walk_speed = 200;
    Vector2 velocity;
    
    Node2D nav_pt;

    public override void _Ready()
    {
        GD.Print("HELLO THERE");
        // Called every time the node is added to the scene.
        // Initialization here
        nav_pt = (Node2D)GetNode("../navpoint_container");
        // Node2D s = (Node2D)GetNode("../navpoint_container");

        
    }

    public override void _Process(float delta)
    {

		Vector2 target_pos = nav_pt.Position;
		MoveAndSlide(target_pos - GetPosition());
	//	if (Input.IsActionPressed("left_click")) {
        	
	//	}
    }
}

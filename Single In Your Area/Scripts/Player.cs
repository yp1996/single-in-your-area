using Godot;
using System;

public class Player : KinematicBody2D
{

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
    }

    public override void _Process(float delta)
    {
		
		if (Input.IsActionPressed("left_click")) {
        	MoveAndSlide(GetGlobalMousePosition() - GetPosition());
		}
    }
}

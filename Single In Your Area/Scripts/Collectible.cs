using Godot;
using System;

public abstract class Collectible : Node2D
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

	protected StatManager statManager; 
	
    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        statManager = (StatManager) GetNode("/root/StatManager");
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
    }
	
	public void Consume() {
	 	Timer timer = (Timer) GetNode("Timer");
		timer.Start();
		ConsumeFunction();
	}
	
	private void OnTimerTimeout()
	{
    	// Delete the collectable after a timeout 
		QueueFree();
	}
	
	public abstract void ConsumeFunction();
}





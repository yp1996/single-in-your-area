using Godot;
using System;

public abstract class Collectible : Node2D
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";
	protected bool goingUp = true;
	protected float stepSize = 0.05f;
	protected int cycleLength = 500;
	protected int timeLastUpdate = 0;	
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
	    if (OS.GetTicksMsec() - timeLastUpdate > cycleLength) {
		goingUp = !goingUp;
		timeLastUpdate = OS.GetTicksMsec();
		}
		if (goingUp) {
			this.SetPosition(this.GetPosition() + new Vector2(0f, stepSize));
		} else {
			this.SetPosition(this.GetPosition() - new Vector2(0f, stepSize));
		}
		ProcessFunction();
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
	
	public abstract void ProcessFunction();
	
	public abstract void ConsumeFunction();

}





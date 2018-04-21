using Godot;
using System;

public class LabelScript : Label

// Testing the stat manager
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";
	
	StatManager statManager;

    public override void _Ready()
    {
		statManager = (StatManager) GetNode("/root/StatManager");
        this.Text = "Anxiety: " + statManager.GetStat("anxiety");
        
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
        this.Text = "Anxiety: " + statManager.GetStat("anxiety");
    }
}

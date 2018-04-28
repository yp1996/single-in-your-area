using Godot;
using System;

public class ModulateMusic : AudioStreamPlayer2D
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";
	
	StatManager statManager;
	AudioStreamSample sample;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        statManager = (StatManager) GetNode("/root/StatManager");
		sample = (AudioStreamSample) Stream;
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
        float relAnx = 1.0f - statManager.GetRelativeStat("anxiety");
		sample.MixRate = (int) ((22100.0f)*relAnx + 22100.0f);
    }
}

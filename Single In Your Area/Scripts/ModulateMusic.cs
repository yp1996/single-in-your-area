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
        float relAnx = (float) Math.Log(2.0f - statManager.GetRelativeStat("anxiety"), 2f);
		SlowDownMusic(relAnx*Engine.TimeScale);
    }
	
	public void SlowDownMusic(float percentBy, float minValue = 22100.0f) {
		sample.MixRate = (int) ((44200.0f - minValue)*percentBy + minValue);
	}
}

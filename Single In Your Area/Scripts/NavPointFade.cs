using Godot;
using System;

public class NavPointFade : Sprite
{
	float alpha = 1.0f;
	int startTime;
	float fadeSpeed = 0.05f;

    public override void _Ready()
    {
		startTime = OS.GetUnixTime();         
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
		if (OS.GetUnixTime() - startTime > 0.8) {	
			this.Modulate().a = alpha;
			alpha -= fadeSpeed;
		}
		if (Input.IsActionPressed("left_click")) {
			alpha = 1.0f;
			this.Modulate().a = alpha;
			startTime = OS.GetUnixTime();
		}
		
        
    }
}

using Godot;
using System;

public class FentaLeanCollectible : Collectible
{
	private bool isActive = false;
	private const int chanceOfPassOut = 50;
	
    public override void ConsumeFunction() {
		
		Timer timer = (Timer) GetNode("SlowdownTimer");
		timer.Start();
		isActive = true;
		
		AudioStreamPlayer audio = (AudioStreamPlayer) GetNode("Audio");
		audio.Play();
		statManager.IncrementStat("anxiety", -100);
		
		Random rnd = new Random();
		int rndNum = rnd.Next(0, 100);
		if (rndNum <= chanceOfPassOut) {
			statManager.IncrementStat("health", -500);
		}
		else {
			statManager.IncrementStat("emotional_distance", 20);
			Engine.TimeScale = 0.1f;
		}
	}
	
	public override void ProcessFunction() 
	{
		if (isActive) {
			statManager.SetStat("anxiety", 0);
		}
	}
	
	private void OnSlowdownTimerTimeout()
	{
		isActive = false;
    	Engine.TimeScale = 1.0f;
	}
}




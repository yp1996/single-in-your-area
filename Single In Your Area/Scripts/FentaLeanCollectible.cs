using Godot;
using System;

public class FentaLeanCollectible : Collectible
{
    public override void ConsumeFunction() {
		AudioStreamPlayer audio = (AudioStreamPlayer) GetNode("Audio");
		audio.Play();
		statManager.IncrementStat("anxiety", -100);
		statManager.IncrementStat("health", -500);
	}
}

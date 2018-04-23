using Godot;
using System;

public class XanaxCollectible : Collectible
{
    public override void ConsumeFunction() {
		AudioStreamPlayer audio = (AudioStreamPlayer) GetNode("Audio");
		audio.Play();
		statManager.IncrementStat("anxiety", -50);
	}
}

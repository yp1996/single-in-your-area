using Godot;
using System;

public class AnimationTransition : AnimationPlayer
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here

        AddUserSignal("transition_finished");
	
    }

//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
	private void _on_TransitionAnimation_animation_finished(String anim_name)
	{
		GD.Print(anim_name);
		if (anim_name.Contains("heart")) {
		
			EmitSignal("transition_finished");
			
		}
	}
}




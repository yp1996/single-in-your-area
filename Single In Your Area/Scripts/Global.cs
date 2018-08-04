using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Global : Godot.Node
{
    public Node CurrentScene { get; set; }
	public const string animationPath = "res://Scenes/AnimationTransition.tscn";
	private AnimationPlayer transitionAnimationPlayer;
	private PackedScene transitionAnimation;

    public override void _Ready()
    {
        Viewport root = GetTree().GetRoot();
        CurrentScene = root.GetChild(root.GetChildCount() - 1);
    }

    public void GotoScene(string path, bool shouldAnimate = true)
    {
        // This function will usually be called from a signal callback,
        // or some other function from the running scene.
        // Deleting the current scene at this point might be
        // a bad idea, because it may be inside of a callback or function of it.
        // The worst case will be a crash or unexpected behavior.

        // The way around this is deferring the load to a later time, when
        // it is ensured that no code from the current scene is running:

		GD.Print("sure let's go");
        CallDeferred(nameof(DeferredGotoScene), path, shouldAnimate);
    }

    public async void DeferredGotoScene(string path, bool shouldAnimate)
    {
		GD.Print("deferred scene");
        // Immediately free the current scene, there is no risk here.
		if (shouldAnimate) {
			transitionAnimation = (PackedScene) GD.Load(animationPath);
			transitionAnimationPlayer = (AnimationPlayer) transitionAnimation.Instance();
			CurrentScene.AddChild(transitionAnimationPlayer); 
	
			transitionAnimationPlayer.Play("heart");
			await ToSignal(transitionAnimationPlayer, "animation_finished");	
		}
        CallDeferred(nameof(DeferredClearScene), path, shouldAnimate);
    }
	
	public async void DeferredClearScene(string path, bool shouldAnimate) {
		GD.Print("deferred scene [t 2");
		CurrentScene.Free();

        // Load a new scene
        var nextScene = (PackedScene)GD.Load(path);

        // Instance the new scene
        CurrentScene = nextScene.Instance();
			
		//transitionAnimationPlayer.Free();

        // Add it to the active scene, as child of root
        GetTree().GetRoot().AddChild(CurrentScene);
		
		
        // optional, to make it compatible with the SceneTree.change_scene() API
        GetTree().SetCurrentScene(CurrentScene);
		
		if (shouldAnimate) {
			transitionAnimationPlayer = (AnimationPlayer) transitionAnimation.Instance();
			CurrentScene.AddChild(transitionAnimationPlayer); 
			transitionAnimationPlayer.PlayBackwards("heart");
			await ToSignal(transitionAnimationPlayer, "transition_finished");
			CallDeferred(nameof(DeferredClearTransition));
		}
	}
	
	public void DeferredClearTransition() {
		transitionAnimationPlayer.QueueFree();
	}
}
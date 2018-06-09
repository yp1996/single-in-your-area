using Godot;
using System;

public class Global : Godot.Node
{
    public Node CurrentScene { get; set; }

    public override void _Ready()
    {
        Viewport root = GetTree().GetRoot();
        CurrentScene = root.GetChild(root.GetChildCount() - 1);
    }

    public void GotoScene(string path)
    {
        // This function will usually be called from a signal callback,
        // or some other function from the running scene.
        // Deleting the current scene at this point might be
        // a bad idea, because it may be inside of a callback or function of it.
        // The worst case will be a crash or unexpected behavior.

        // The way around this is deferring the load to a later time, when
        // it is ensured that no code from the current scene is running:

        CallDeferred(nameof(DeferredGotoScene), path);
    }

    public void DeferredGotoScene(string path)
    {
        // Immediately free the current scene, there is no risk here.
        CurrentScene.Free();

        // Load a new scene
        var nextScene = (PackedScene)GD.Load(path);

        // Instance the new scene
        CurrentScene = nextScene.Instance();

        // Add it to the active scene, as child of root
        GetTree().GetRoot().AddChild(CurrentScene);

        // optional, to make it compatible with the SceneTree.change_scene() API
        GetTree().SetCurrentScene(CurrentScene);
    }
}
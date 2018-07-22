using Godot;
using System;

public class SceneSwitcher : StaticBody2D
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    public string scene = "res://Scenes/Levels/Mall.tscn";
    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
    }

    public void SwitchScene()
    {
        var global = (Global)GetNode("/root/Global");
        global.GotoScene(scene);
    }

}

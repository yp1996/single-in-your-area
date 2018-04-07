using Godot;
using System;

public class ButtonPress : Button
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    public void _OnButtonPressed()
    {
        var label = (Label)GetNode("Label");
		
        label.Text = "HELLO!";
    }

    public override void _Ready()
    {
        this.Connect("pressed", this, nameof(_OnButtonPressed));
    }   
}

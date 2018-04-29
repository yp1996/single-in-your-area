using Godot;
using System;

public class Message : HBoxContainer {
  TextureRect theirAvatar, myAvatar;
  Label text;


  public override void _Ready() {
  // public Message() {
    theirAvatar = ((TextureRect)GetNode("TheirAvatar"));
    myAvatar = ((TextureRect)GetNode("MyAvatar"));
    text = ((Label)GetNode("Label"));
    GD.Print("My size = ");
    GD.Print(this.RectSize);
  }

  public void setMessage(string message) {
    text.Text = message;
  }

  public void setIsPlayer(bool isPlayer) {
    theirAvatar.Visible = !isPlayer;
    myAvatar.Visible = isPlayer;
    text.Align = isPlayer ? Label.AlignEnum.Right : Label.AlignEnum.Left;
  }
}

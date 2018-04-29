/*
TODO:
 - Timing for responses?
 - typing / read status indicator?
 - Make font smaller
 - Avatar images
 - clean up message UI
 - Shorter message options.
 - Top of App UI.
*/
using Godot;
using System;

public class ViewportContainer : VBoxContainer {
  Timer timer;
  PackedScene msgScene;

  bool themSay = true;
  int messageAt = 0;

  public override void _Ready() {
    // Initialization here
    GD.Print("VC Init");
    timer = (Timer) GetNode("Timer");
    msgScene = (PackedScene) ResourceLoader.Load("res://Scenes/Chat/Message.tscn");
    doThemSay();
  }


  private void addMessage(string text, bool isPlayer) {
    GD.Print("Adding messages...");

    Message msg = (Message) msgScene.Instance();
    GetNode("Container/MessageList").AddChild(msg);
    msg.setMessage(text);
    msg.setIsPlayer(isPlayer);
  }

  private void setAnswerVisibility(bool visible) {
    HBoxContainer answerContainer = (HBoxContainer) GetNode("TextEntry");
    foreach (Node child in answerContainer.GetChildren()) {
      Label answerButton = (Label) child;
      answerButton.Visible = visible;
    }
  }

  // soulmate talks
  private void doThemSay() {
    string messageText = MessageLogic.GetTheirMessage(messageAt);
    Message msg = (Message) msgScene.Instance();
    GetNode("Container/MessageList").AddChild(msg);
    msg.setMessage(messageText);
    msg.setIsPlayer(false);
    themSay = false;
    setAnswerVisibility(true);

    // Add delay?
    doWeSay();
  }

  // player gets given options of what to say
  private void doWeSay() {
    string[] optionLabels = MessageLogic.GetOurOptions(messageAt);
    HBoxContainer answerContainer = (HBoxContainer) GetNode("TextEntry");
    object[] buttonList = answerContainer.GetChildren();

    for (int i = 0; i < buttonList.Length; i++) {
      Label answerButton = (Label) buttonList[i];
      if (i < optionLabels.Length) {
        answerButton.Visible = true;
        answerButton.Text = optionLabels[i];
      } else {
        answerButton.Visible = false;
      }
    }
  }

  // player talks by picking option
  private void doWeRespond(int option) {
    string ourResponse = MessageLogic.GetOurResponse(messageAt, option);

    Message msg = (Message) msgScene.Instance();
    GetNode("Container/MessageList").AddChild(msg);
    msg.setMessage(ourResponse);
    msg.setIsPlayer(true);
    themSay = true;
    messageAt++;

    setAnswerVisibility(false);
    timer.WaitTime = 5;
    timer.Start();
  }


  //
  // Event handlers
  //

  private void _on_Answer1_gui_input(Godot.Object ev) {
    if (ev is InputEventScreenTouch) {
      bool down = ((InputEventScreenTouch)ev).Pressed;
      if (!down) {
        doWeRespond(0);
      }
    }
  }

  private void _on_Answer2_gui_input(Godot.Object ev) {
    if (ev is InputEventScreenTouch) {
      bool down = ((InputEventScreenTouch)ev).Pressed;
      if (!down) {
        doWeRespond(1);
      }
    }
  }

  private void _on_Answer3_gui_input(Godot.Object ev) {
    if (ev is InputEventScreenTouch) {
      bool down = ((InputEventScreenTouch)ev).Pressed;
      if (!down) {
        doWeRespond(2);
      }
    }
  }

  private void _on_Timer_timeout() {
    doThemSay();
  }
}

/*
TODO:
 - Timing for responses?
 - typing / read status indicator?
 - Avatar images
 - Top of App UI.
*/
using Godot;
using System;

public class ViewportContainer : VBoxContainer {
  Timer replyTimer, scrollTimer;
  PackedScene msgScene;

  bool themSay = true;
  int messageAt = 0;

  public override void _Ready() {
    // Initialization here
    GD.Print("VC Init");
    replyTimer = (Timer) GetNode("ReplyTimer");
    scrollTimer = (Timer) GetNode("MoveScrollTimer");
    msgScene = (PackedScene) ResourceLoader.Load("res://Scenes/Chat/Message.tscn");
    doThemSay();
  }


  private void addMessage(string text, bool isPlayer) {
    GD.Print("Adding messages...");
    Message msg = (Message) msgScene.Instance();
    GetNode("Panel/ScrollContainer/MessageList").AddChild(msg);
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
    GetNode("Panel/ScrollContainer/MessageList").AddChild(msg);
    msg.setMessage(messageText);
    msg.setIsPlayer(false);
    themSay = false;
    setAnswerVisibility(true);
    forceBottomScroll();

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

    if (ourResponse == "") {
      GD.Print("TODO: Close the app back up again");
      return;
    }

    Message msg = (Message) msgScene.Instance();
    GetNode("Panel/ScrollContainer/MessageList").AddChild(msg);
    msg.setMessage(ourResponse);
    msg.setIsPlayer(true);
    themSay = true;
    messageAt++;
    forceBottomScroll();

    setAnswerVisibility(false);
    replyTimer.WaitTime = 5;
    replyTimer.Start();
  }

  private void forceBottomScroll() {
    scrollTimer.WaitTime = 0.001f;
    scrollTimer.Start();
  }


  //
  // Event handlers
  //

  private void _on_Answer1_gui_input(Godot.Object ev) {
    replyIfTouchUp(ev, 0);
  }
  private void _on_Answer2_gui_input(Godot.Object ev) {
    replyIfTouchUp(ev, 1);
  }
  private void _on_Answer3_gui_input(Godot.Object ev) {
    replyIfTouchUp(ev, 2);
  }
  private void _on_ReplyTimer_timeout() {
    doThemSay();
  }
  private void _on_MoveScrollTimer_timeout() {
    ScrollContainer container = (ScrollContainer) GetNode("Panel/ScrollContainer");
    VBoxContainer messageList = (VBoxContainer) GetNode("Panel/ScrollContainer/MessageList");
    float overlap = messageList.RectSize.y - container.RectSize.y + 12;
    if (overlap > 0) {
      container.ScrollVertical = (int) overlap;
    }
  }

  private void replyIfTouchUp(Godot.Object ev, int option) {
    if (ev is InputEventScreenTouch) {
      if (!((InputEventScreenTouch)ev).Pressed) {
        doWeRespond(option);
      }
    }
  }
}

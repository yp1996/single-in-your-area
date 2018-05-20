using Godot;
using System;
using System.Collections.Generic;

/** Reply we can make, given possible requirements. */
public class ChatReply {  
  // Text to put on the button for making the reply.
  private readonly string _buttonText;
  // Longer text to add to the chat log.
  private readonly string _responseText;
  // Optional requirments that are all needed for the reply to be available.
  private readonly ChatRequirement[] _requirements;

  public ChatReply(string buttonText, string responseText, ChatRequirement[] requirements) {
    _buttonText = buttonText;
    _responseText = responseText;
    _requirements = requirements;
  }

  /** @return Whether the reply can be made given the current player state. */
  public bool isAvailable(StatManager statManager) {
    foreach (ChatRequirement req in _requirements) {
      if (!req.isFulfilled(statManager)) {
        return false;
      }
    }
    return true;
  }

  public string buttonText() {
    return _buttonText;
  }

  public string responseText() {
    return _responseText;
  }
}

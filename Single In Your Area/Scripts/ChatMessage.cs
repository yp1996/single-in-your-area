using Godot;
using System;
using System.Collections.Generic;

public class ChatMessage {
  private string _theirMessage;
  private ChatReply[] _ourReplies;

  public ChatMessage(string theirMessage, ChatReply[] ourReplies) {
    _theirMessage = theirMessage;
    _ourReplies = ourReplies;
  }

  public string theirMessage() {
    return _theirMessage;
  }
  public ChatReply[] ourAvailableReplies(StatManager statManager) {
    List<ChatReply> replies = new List<ChatReply>();
    for (int i = 0; i < _ourReplies.Length; i++) {
      if (_ourReplies[i].isAvailable(statManager)) {
        replies.Add(_ourReplies[i]);
      }
    }
    return replies.ToArray();
  }

  public string ourResponse(int response) {
    return _ourReplies[response].responseText();
  }
}

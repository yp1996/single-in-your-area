using Godot;
using System;

/**
 * Manage which conversation the player is currently in.
 */
static class MessageLogic {
  // TODO: Allow multiple conversations, and transition logic. Currently we have just one.
  static InitialConversation currentConvo = new InitialConversation();

  public static string GetTheirMessage(int messageID) {
    return currentConvo.GetTheirMessage(messageID);
  }
  public static ChatReply[] GetOurAvailableReplies(int messageID, StatManager statManager) {
    return currentConvo.GetOurAvailableReplies(messageID, statManager);
  }
  public static string GetOurResponse(int messageID, int response) {
    return currentConvo.GetOurResponse(messageID, response);
  }
}

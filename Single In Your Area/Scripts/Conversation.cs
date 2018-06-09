using Godot;
using System;

/**
 * To make a new conversation, extend this, and implement BuildMessages().
 *   @see InitialConversation for an example, and ConversationParts for details.
*/
abstract class Conversation {
  // IMPLEMENT THIS FOR YOUR CONVERSATION.
  public abstract ChatMessage[] BuildMessages();

  /** @return The text the other person speaks at this point. */
  public string GetTheirMessage(int messageID) {
    return _messages[messageID].theirMessage();
  }

  /** @return All replies we have available. */
  public ChatReply[] GetOurAvailableReplies(int messageID, StatManager statManager) {
    return _messages[messageID].ourAvailableReplies(statManager);
  }

  /** @return The text in our reply at this point. */
  public string GetOurResponse(int messageID, int response) {
    return _messages[messageID].ourResponse(response);
  }

  private ChatMessage[] _messages;
  public Conversation() {
    _messages = BuildMessages();
  }
}

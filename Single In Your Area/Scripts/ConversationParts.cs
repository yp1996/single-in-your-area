using Godot;
using System;

/**
 * Different parts of talk that a conversation can be made up off.
 *   Import this statically, then use these within your convo specification.
 */
class ConversationParts {
  /** Use this a reply which will close the chat app. */
  public static ChatReply CLOSE_FINDR = Reply("Close Findr", "");

  /**
   * Simply converts Messages(m1, m2, m3, m4, ...) to a list.
   */
  public static ChatMessage[] Messages(params ChatMessage[] messages) {
    return messages;
  }

  /**
   * Single them -> you interaction.
   * They start with a message, then you have a range of options you can reply with.
   */
  public static ChatMessage Message(string theirMessage, params ChatReply[] ourReplies) {
    return new ChatMessage(theirMessage, ourReplies);
  }

  /**
   * Simply converts Replies(r1, r2, r3, r4, ...) to a list.
   */
  public static ChatReply[] Replies(params ChatReply[] replies) {
    return replies;
  }

  /**
   * Possible reply we can make. Contains:
   *   optionText: The caption on the button to click to answer this.
   *   responseText: The text written out in the chat log when the button is clicked.
   *   requirements: Optional stat requirements needed to be able to respond in this way.
   */
  public static ChatReply Reply(string optionText, string responseText, params ChatRequirement[] requirements) {
    return new ChatReply(optionText, responseText, requirements);
  }

  /**
   * List of requiements for a given reply.
   *   Supported are "stat < 50" or "stat > 50", where 'stat' is a player stat, and '50' is any number.
   * For stat names, see StatManager.
   */
  public static ChatRequirement[] Requirements(params string[] requirements) {
    ChatRequirement[] reqs = new ChatRequirement[requirements.Length];
    for (int i = 0; i < requirements.Length; i++) {
      reqs[i] = new ChatRequirement(requirements[i]);
    }
    return reqs;
  }
}

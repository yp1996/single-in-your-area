using Godot;
using System;

static class MessageLogic {

  static string[] THEIR_MESSAGES = {
    "Hey there!",
    "Nm, just chillin. Hbu?",
    "Haha okay",
    "Want to cam? Follow this link http://dvdmrn.xyz ;)  uwu\n\n"
      + "Jk, was just messing w u rawr xD\n"
      + "I'm so random!!!!!!11!1 >w<",
    "Ah! No but u can follow me on DeviantArt",
    "Uh, thanks I guess?",
    "I'm thinking of hitting up the mall later... meet me there? I'm on my way.",
  };

  static string[][] OUR_OPTIONS = { new string[] {
    "Reply"
  }, new string[] {
    "Flirt",
    "Be honest",
    "Be a sadboi"
  }, new string[] {
    "Be a gentleman",
    "Panic",
    "Be cool"
  }, new string[] {
    "Ask about social status",
    "Ask about life story",
    "Ask about income"
  }, new string[] {
    "Make it about your kinks",
    "Tell them they look just like your parent",
    "Tell them you used to like that art style"
  }, new string[] {
    "Act thirsty",
    "Play it cool",
    "Appear lonely"
  }, new string[] {
    "Close Findr"
  }};

  static string[][] OUR_RESPONSES = { new string[] {
    "Hey what's up?"
  }, new string[] {
    "Having a good time talking to you ;)",
    "Freaking out bc i thought everyone was dead",
    "Just alone in my room. Thinking about how meaningless existence is, how much I wish I could fucking kill myself rn."
  }, new string[] {
    "*sweats nervously* so uh, how is m'lady/lord feeling today? Would they care to make my acquaintance? *tips fedora*",
    "Haha I mean it's cool everything's cool nice weather we got today right? It hasn't been this nice in a while I think I'm gonna go out for a walk",
    "I saw that you like $ui$ide Boy$, I'm on their new EP. I make post-clout emo soundcloud beats."
  }, new string[] {
    "Omg do you have Myspace?",
    "Do u like anime?",
    "I'd like to add you to my professional network on LinkedIn."
  }, new string[] {
    "Woa this is so cool! The way you draw women is so subversive - those biceps! She looks like she's on HGH. I want those thighs to crush my face.",
    "dude you look so much like my dad...",
    "I used to like that art style"
  }, new string[] {
    "NEEDS TEXT",
    "U heard of music before?",
    "TBH I'm just happy to be talking to someone. I've been in my room all week. I have no friends. I'm so lonely. Please don't leave."
  }, new string[] {
    "" // Empty string = close
  }};

  public static string GetTheirMessage(int messageID) {
    return THEIR_MESSAGES[messageID];
  }
  public static string[] GetOurOptions(int messageID) {
    return OUR_OPTIONS[messageID];
  }
  public static string GetOurResponse(int messageID, int response) {
    return OUR_RESPONSES[messageID][response];
  }
}

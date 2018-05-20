using Godot;
using System;

using static ConversationParts;

class InitialConversation : Conversation  {
  public override ChatMessage[] BuildMessages() { return Messages(

Message(
  "Hey there", Replies(
    Reply("Reply", "Hey what's up?")
  )
),
Message(
  "Nm, just chillin. Hbu?", Replies(
    Reply("Flirt",
      "Having a good time talking to you ;)",
      Requirements("anxiety < 10")),
    Reply("Be honest",
        "Freaking out bc i thought everyone was dead"),
    Reply("Be a sadboi",
        "Just alone in my room. Thinking about how meaningless existence is, how much I wish I could fucking kill myself rn.")
  )
),
/*
Message(
  "Haha okay", Replies(
    Reply("Be a gentleman",
            "*sweats nervously* so uh, how is m'lady/lord feeling today? Would they care to make my acquaintance? *tips fedora*"),
    Reply("Panic",
            "Haha I mean it's cool everything's cool nice weather we got today right? It hasn't been this nice in a while I think I'm gonna go out for a walk"),
    Reply("Be cool",
            "I saw that you like $ui$ide Boy$, I'm on their new EP. I make post-clout emo soundcloud beats.")
  )
),
Message (
  "Want to cam? Follow this link http://dvdmrn.xyz ;)  uwu\n\nJk, was just messing w u rawr xD\nI'm so random!!!!!!11!1 >w<", Replies(
    Reply("Ask about social status",
            "Omg do you have Myspace?"),
    Reply("Ask about life story",
            "Do u like anime?"),
    Reply("Ask about income",
            "I'd like to add you to my professional network on LinkedIn.")
  )
),
Message (
  "Ah! No but u can follow me on DeviantArt", Replies(
    Reply("Make it about your kinks",
            "Woa this is so cool! The way you draw women is so subversive - those biceps! She looks like she's on HGH. I want those thighs to crush my face."),
    Reply("Tell them they look just like your parent",
            "dude you look so much like my dad..."),
    Reply("Tell them you used to like that art style",
            "I used to like that art style")
  )
),
Message (
  "Uh, thanks I guess?", Replies(
    Reply("Act thirsty",
            "NEEDS TEXT"),
    Reply("Play it cool",
            "U heard of music before?"),
    Reply("Appear lonely",
            "TBH I'm just happy to be talking to someone. I've been in my room all week. I have no friends. I'm so lonely. Please don't leave.")
  )
),
*/
Message (
  "I'm thinking of hitting up the mall later... meet me there? I'm on my way.",
  Replies(CLOSE_FINDR)
)

  );}
}

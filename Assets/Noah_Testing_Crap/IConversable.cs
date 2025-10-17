using UnityEngine;

public interface IConversable
{
   // A Conversable Object has the ability to do some ChitChat and be able to give Talking/Prompts to where the player can give a response
   //void LoadChitChat();
   void StartChitChat();
   bool NextChitChat();

   void StartTalkPrompt();
   bool TryResponse();
}

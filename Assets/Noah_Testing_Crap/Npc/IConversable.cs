using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IConversable
{
   // A Conversable Object has the ability to do some ChitChat and be able to give Talking/Prompts to where the player can give a response
   //void LoadChitChat();
   bool StartChitChat();
   bool NextChitChat();

   bool StartTalkPrompt();
   bool TryResponse(List<WordData> sentence);
   void StartSolutionChitChat();
   Sentence GetSolutionSentence();
}

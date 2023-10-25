using System;
using System.Collections.Generic;
using UnityEngine;

namespace DailyUpdates.Components
{
    public class StoryDialogue : MonoBehaviour
    {
        private static readonly List<string> DialogueMsgs = new();

        // Start is called before the first frame update
        private void Start()
        {
            // Day 1
            DialogueMsgs.Add("You completed your first day! You are proud of " +
                              "what you've accomplished so far. However, you question" +
                              " whether it will be enough...");
            
            DialogueMsgs.Add("You're not quite used to everything. You were usually " +
                             "making robots to do simple inconvenient tasks, like take " +
                             "out the trash, organize the shelves, and do calculations.\n\n" +
                             "Now, you are depending on these robots to survive. " +
                             "You begin thinking about how you could have made robots " +
                             "to solve more important issues, rather than encourage " +
                             "your laziness...");
            
            DialogueMsgs.Add("Day 3 down. You think to say to yourself \"3rd day's the charm!\"" +
                             "in an attempt to be optimistic. \"Why is the third time the charm anyway?\" " +
                             "you thought. \"Why do so many things come in threes?\"");
            
            DialogueMsgs.Add("You know you should be panicking a lot more than you have " +
                             "been so far. There's only so long that your fortress (or rather your elevated " +
                             "semi-hidden house) will last before you're found. Hopefully your robots' " +
                             "program to not lure any animals back to your house works correctly..");
            
            DialogueMsgs.Add("You think about why the Alligator Queen decided to take control " +
                             "of the land... maybe just for fun? Maybe for the power?\n\n" +
                             "In that case... is she any much different from you, a person " +
                             "controlling robots to do everything for you? Perhaps one day " +
                             "when this is all over, you'll get to let your robots free...\n\n" +
                             "Or alternatively, need to depend on your robots for the rest " +
                             "of your life...");
            
            DialogueMsgs.Add("You're starting to realize how horrible the world has become. You start " 
                             + "remembering your friends... your family... or at least you try to. "
                             + "The nonstop working and the Alligator Queen growing stronger every day has made "
                             + "you forget some of the most important people of your life, or at least a few " 
                             + "important memories.");
            
            
            DialogueMsgs.Add("You've made it an entire week! You suppose it's some sort of accomplishment. " 
                             + "Though that accomplishment means nothing if you don't escape. "
                             + "Maybe you can write \"Made it a week!\" somewhere so someone else can find it. "
                             + "It'd be the only way to share this accomplishment with another human being "
                             + "anyways...");
            
            DialogueMsgs.Add("\"Wait...\" you say to yourself... \"What day of the week is it? " 
                             + "It's Sunday right?\" You only kept track of the day numbers this entire time...");
            
            DialogueMsgs.Add("You want to be friends with your robots, but you wonder if you should. " 
                             + "They are robots after all and don't have real human emotion... or do they? " 
                             + "Would you go insane if you tried to program human-like interactions into " 
                             + "these robots? You wish you could socialize with human beings anyway. It" 
                             + "gets lonely out here...");
            
            DialogueMsgs.Add("10! You've hit double digits! You hope you don't <i>have</i> to hit triple... " 
                             + "but you hope you do if it means you'll survive.");
        }

        // Gets the dialogue message for a specific day
        public static string GetMsg(int day)
        {
            try
            {
                return DialogueMsgs[day - 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                return "Need more dialogue messages...";
            }
        }
    }
}
using COMMAND;
using System;
using System.Collections;
using UnityEngine;
using Music;
using Background;

namespace Testing
{
    public class CMD_DatabaseExtensions_Examples : CMD_DatabaseExtension
    {
        new public static void Extend(CommandDatabase database)
        {
            //Sound commands
            database.AddCommand("playsound", new Action<string>(PlaySound));
            database.AddCommand("startmusic", new Action<string>(StartMusic));
            database.AddCommand("endmusic", new Action(EndMusic));

            //Background commands
            database.AddCommand("setbackground", new Action<string>(SetBackground));
        }

        public static void PlaySound(string soundName)
        {
            
            SoundManager.instance.PlaySound(soundName); ;

        }

        public static void StartMusic(string soundName)
        {
            SoundManager.instance.StartMusic(soundName);
        }

        public static void EndMusic()
        {
            SoundManager.instance.EndMusic();
        }

        public static void SetBackground(string name)
        {
            BackgroundController.instance.SetBackground(name);
        }
        
    }
}
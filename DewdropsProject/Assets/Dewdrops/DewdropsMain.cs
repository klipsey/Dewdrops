﻿/*
 * Thank you for using Thunderkit and MSU to develop your mod, We hope this Template mod boilerplate aids your 
 * development pipeline.
 * 
 * Keep in mind that MSU expects the end user to know how modding RoR2 works, as such, these comments will work mainly 
 * to explain code from the MSU API and how it's used.
 * 
 * If you stumble upon a method or field that does not have a comment, try hovering over it! MSU has COMPLETE XML
 * Documentation which can come in handy.
 * 
 * These classes utilizes the C# feature for Conditional Compilation utilizing Preprocessor Directives. More info about
 * this system can be found here: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/preprocessor-directives#conditional-compilation
*/

using BepInEx;
using MSU;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Basically all key  names are written as "Dewdrops" + X. Such as Dewdrops namespace, DewdropsMain, DewdropsAssets, 
 * Etc. it is recommended to look thru the files and renaming the word "Dewdrops" to fit your needs.
 * 
 * A common tactic used by the team behind MSU, is to make the namespace Explicit (IE: Starstorm2), and make any other
 * classes use an acronym suffix, such as "SS2" (IE: SS2Main, SS2Assets, etc).
 */ 
namespace Dewdrops
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class DewdropsMain : BaseUnityPlugin
    {
        /*
         * These are your mod's GUID, Version and Name strings, we use public constants so that these can be accessed
         * by other mods so they can easily implement soft dependencies to our mod.
         */

        //MAKE SURE TO CHANGE THE NAME OF YOUR TEMPLATE, MSU WILL FLAT OUT IGNORE THIS GUID ON IT'S FRAMEWORKS.
        public const string GUID = "com.TeamName.Dewdrops";
        public const string VERSION = "0.0.1";
        public const string NAME = "Dewdrops";

        //Singleton access pattern to our instance.
        internal static DewdropsMain instance { get; private set; }

        private void Awake()
        {
            instance = this;

            //We create our custom logger first so we can log stuff to console if necesary.
            new DewdropsLog(Logger);
            // Configuration comes next, we pass our instance of BaseUnityPlugin so we get access to the GUID of our
            // mod.
            new DewdropsConfig(this);

            //We do not load our assetbundles or content at awake, instead, we create a new instance of this class,
            //which implements the game's IContentPackProvider interface. Our content pack provider also loads language files.
            new DewdropsContent();
        }
    }
}
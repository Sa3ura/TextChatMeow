﻿using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using HintServiceMeow;
using MEC;
using PluginAPI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

//  V1.2.0
//      fixing bugs
//  V1.2.1
//      fixing bugs
// V1.2.2
//      Use count down instead of calculating the time directly. Bug fixing
// V1.2.3
//      Bug fixing
// V1.2.4
//      Bug fixing
// V1.2.5
//      Use DateTime instead of count down. Bug fixing
// V1.3.0
//      Use regex to clear rich-text tags.
// V1.3.1
//      Bug fixing, fixed the bug that the message will be cleared every 2 seconds
// V1.4.0
//      Rewrite for HintServiceMeow V5.0.0

namespace TextChatMeow
{
    internal class Plugin : Plugin<Config>
    {
        public static Plugin instance { get; set; }

        public override string Name => "TextChatMeow";
        public override string Author => "Sa3ura, MeowServerOwner";
        public override Version Version => new Version(1, 4, 0);

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Verified += EventHandler.CreateNewMessageManager;
            Exiled.Events.Handlers.Player.Left += EventHandler.DeleteMessageManager;

            Exiled.Events.Handlers.Server.RestartingRound += MessagesList.ClearMessageList;
            Exiled.Events.Handlers.Server.RoundEnded += MessagesList.ClearMessageList;

            base.OnEnabled();
            instance = this;
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Verified -= EventHandler.CreateNewMessageManager;
            Exiled.Events.Handlers.Player.Left -= EventHandler.DeleteMessageManager;

            Exiled.Events.Handlers.Server.RestartingRound -= MessagesList.ClearMessageList;
            Exiled.Events.Handlers.Server.RoundEnded -= MessagesList.ClearMessageList;

            base.OnDisabled();
            instance = null;
        }
    }

    public static class EventHandler
    {
        public static void CreateNewMessageManager(VerifiedEventArgs ev)
        {
            new DisplayManager(ev);
        }

        public static void DeleteMessageManager(LeftEventArgs ev)
        {
            DisplayManager.RemoveMessageManager(ev.Player);
        }
    }
}

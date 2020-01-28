﻿using EXILED;
using Harmony;
using System.Collections.Generic;

namespace stalky106
{
	public class Stalky106 : Plugin
	{
		private EventHandlers events;
		public static HarmonyInstance HarmonyInstance { private set; get; }
		public static int harmonyCounter;
		public const string Version = "V1.1";
		public bool enabled;
		public static IEnumerable<MEC.CoroutineHandle> Coroutines { set; get; }
		public override void OnDisable()
		{
			if (HarmonyInstance != null || HarmonyInstance != default)
			{
				HarmonyInstance.UnpatchAll();
			}
			if (!enabled) return;
			if (Coroutines != null) MEC.Timing.KillCoroutines(Coroutines);
			Events.RoundStartEvent -= events.OnRoundStart;
			Events.SetClassEvent -= events.OnSetClass;
			Info("Larry won't ever stalk you again at night...");
		}

		public override void OnEnable()
		{
			enabled = Config.GetBool("stalky_enable", true);
			if (!enabled)
			{
				Error("Stalky106 is disabled via configs. It will not be loaded.");
				return;
			}
			Info("Prepare to face Larry...");
			events = new EventHandlers();
			harmonyCounter++;
			HarmonyInstance = HarmonyInstance.Create($"rogerfk.stalky106{harmonyCounter}");
			HarmonyInstance.PatchAll();
			StalkyConfigs.ReloadConfigs();
			Events.RoundStartEvent += events.OnRoundStart;
			Events.SetClassEvent += events.OnSetClass;
			Events.RemoteAdminCommandEvent += events.RACommand;
		}

		public override string getName => "Stalky106-[TAB]";

		public override void OnReload()
		{
		}
	}
}

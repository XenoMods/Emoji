using BepInEx;
using BepInEx.IL2CPP;
using Emoji.Helpers;
using HarmonyLib;
using Reactor;
using UnityEngine;
using XenoCore;
using XenoCore.Network;
using XenoCore.Utils;

namespace Emoji {
	[BepInPlugin(Id)]
	[BepInProcess(Globals.PROCESS)]
	[BepInDependency(ReactorPlugin.Id)]
	[BepInDependency(XenoPlugin.Id)]
	// ReSharper disable once ClassNeverInstantiated.Global
	public class EmojiPlugin : BasePlugin {
		public const string Id = "com.mishin870.emoji";
		public static readonly string Version = "1.0.0";

		public Harmony Harmony { get; } = new Harmony(Id);

		public override void Load() {
			Harmony.PatchAll();

			VersionsList.Add("Emoji", Version, true);
			
			HandleRpcPatch.AddListener(new RPCEmoji());

			StandardEmoji.Load();
		}
	}
}
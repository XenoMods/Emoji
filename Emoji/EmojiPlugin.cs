using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;
using XenoCore;
using XenoCore.Core;
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

		public static readonly XenoMod Mod = new XenoMod(Id, "Emoji", Version, true);

		public override void Load() {
			Harmony.PatchAll();
			
			Mod.RegisterMessage(SpawnEmojiMessage.INSTANCE);

			StandardEmoji.Load();
		}
	}
}
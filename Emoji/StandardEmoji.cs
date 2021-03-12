using System.Reflection;
using Reactor.Unstrip;
using UnityEngine;
using XenoCore.Utils;

namespace Emoji {
	public static class StandardEmoji {
		public static readonly ResourceLoader LOADER = new ResourceLoader("Emoji.Resources.",
			Assembly.GetExecutingAssembly());

		public static readonly BundleDefinition BUNDLE = LOADER.Bundle("emoji");
		
		public static readonly EmojiDefinition BUTTON = new EmojiDefinition(BUNDLE,
			"ButtonPrefab", KeyCode.T, KeyCode.Keypad1);
		public static readonly EmojiDefinition ATTENTION = new EmojiDefinition(BUNDLE,
			"AttentionPrefab", KeyCode.G, KeyCode.Keypad2);
		public static readonly EmojiDefinition FIRE = new EmojiDefinition(BUNDLE,
			"FirePrefab", KeyCode.B, KeyCode.Keypad3);
		public static readonly EmojiDefinition GHOST = new EmojiDefinition(BUNDLE,
			"GhostPrefab", KeyCode.Y, KeyCode.Keypad4);
		public static readonly EmojiDefinition POOP = new EmojiDefinition(BUNDLE,
			"PoopPrefab", KeyCode.H, KeyCode.Keypad5);
		public static readonly EmojiDefinition QUESTION = new EmojiDefinition(BUNDLE,
			"QuestionPrefab", KeyCode.N, KeyCode.Keypad6);
		public static readonly EmojiDefinition SLEEP = new EmojiDefinition(BUNDLE,
			"SleepPrefab", KeyCode.U, KeyCode.Keypad7);
		public static readonly EmojiDefinition THUNDER = new EmojiDefinition(BUNDLE,
			"ThunderPrefab", KeyCode.J, KeyCode.Keypad8);
		public static readonly EmojiDefinition LOVE = new EmojiDefinition(BUNDLE,
			"LovePrefab", KeyCode.M, KeyCode.Keypad9);

		public static void Load() {
			EmojiController.AddEmoji(BUTTON);
			EmojiController.AddEmoji(ATTENTION);
			EmojiController.AddEmoji(FIRE);
			EmojiController.AddEmoji(GHOST);
			EmojiController.AddEmoji(POOP);
			EmojiController.AddEmoji(QUESTION);
			EmojiController.AddEmoji(SLEEP);
			EmojiController.AddEmoji(THUNDER);
			EmojiController.AddEmoji(LOVE);
		}
	}
}
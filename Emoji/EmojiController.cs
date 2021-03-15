using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using XenoCore.Core;
using XenoCore.Utils;
using Object = UnityEngine.Object;

namespace Emoji {
	public static class EmojiController {
		private const float TIME_TO_LIVE = 4f;
		private static DateTime LastUsed = DateTime.MinValue;

		private static readonly List<EmojiDefinition> Definitions = new List<EmojiDefinition>();

		public static void AddEmoji(EmojiDefinition Definition) {
			Definition.Id = Definitions.Count;
			Definitions.Add(Definition);
		}

		public static EmojiDefinition GetById(int EmojiId) {
			return EmojiId >= Definitions.Count ? null : Definitions[EmojiId];
		}

		public static void SpawnEmoji(PlayerControl Player, int EmojiId) {
			if (EmojiId >= Definitions.Count) return;

			var Instance = Object.Instantiate(Definitions[EmojiId].Prefab,
				Player.nameText.transform);
			Instance.transform.localPosition = new Vector3(-Player.nameText.Width - 0.05f, 0f, 0f);
			Object.Destroy(Instance, TIME_TO_LIVE);
		}

		private static bool RPCSpawnEmoji(int EmojiId) {
			var Difference = DateTime.Now - LastUsed;
			if (Difference.Seconds < TIME_TO_LIVE) return false;
			LastUsed = DateTime.Now;

			SpawnEmojiMessage.INSTANCE.Send(EmojiId);
			SpawnEmoji(PlayerControl.LocalPlayer, EmojiId);
			return true;
		}

		private static void OnUseEmoji(int EmojiId, EmojiDefinition Definition) {
			if (RPCSpawnEmoji(EmojiId)) {
				Definition.OnSpawn?.Invoke();
			}
		}

		internal static void Update() {
			foreach (var Definition in Definitions) {
				foreach (var Action in Definition.Actions) {
					if (!Action.Last && Input.GetKeyDown(Action.Key)) {
						OnUseEmoji(Definition.Id, Definition);
					}

					Action.Last = Input.GetKeyUp(Action.Key);
				}
			}
		}
	}

	public class EmojiDefinition {
		internal readonly List<EmojiAction> Actions;
		public readonly GameObject Prefab;
		public int Id;
		public Action OnSpawn;

		public EmojiDefinition(BundleDefinition Bundle, string AssetName, params KeyCode[] Keys)
			: this(Bundle.Object(AssetName), Keys) {
		}

		public EmojiDefinition(GameObject Prefab, params KeyCode[] Keys) {
			this.Prefab = Prefab;

			Actions = new List<EmojiAction>(Keys.Length);
			Actions.AddRange(Keys.Select(Key => new EmojiAction {
				Key = Key
			}));
		}
	}
	
	internal class SpawnEmojiMessage : Message {
		public static readonly SpawnEmojiMessage INSTANCE = new SpawnEmojiMessage();

		private SpawnEmojiMessage() {
		}
		
		protected override void Handle() {
			var Player = ReadPlayer();
			var EmojiId = Reader.ReadInt32();
			
			if (Player != null) {
				EmojiController.SpawnEmoji(Player, EmojiId);
			}
		}

		public void Send(int EmojiId) {
			Write(Writer => {
				Writer.Write(PlayerControl.LocalPlayer.PlayerId);
				Writer.Write(EmojiId);
			});
		}
	}

	internal class EmojiAction {
		public bool Last;
		public KeyCode Key;
	}
	
	[HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
	internal class UpdateForEmojiPatch {
		public static void Postfix(HudManager __instance) {
			if (PlayerControl.LocalPlayer == null || !PlayerControl.LocalPlayer.CanMove) return;

			EmojiController.Update();
		}
	}
}
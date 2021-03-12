using Hazel;
using XenoCore.Network;
using XenoCore.Utils;

namespace Emoji {
	public enum CustomRPC : byte {
		SpawnEmoji = 90
	}
	
	public class RPCEmoji : RPCListener {
		public void Handle(byte PacketId, MessageReader Reader) {
			switch (PacketId) {
				case (byte) CustomRPC.SpawnEmoji: {
					var PlayerId = Reader.ReadByte();
					var EmojiId = Reader.ReadInt32();

					var Control = PlayerTools.GetPlayerById(PlayerId);
					if (Control != null) {
						EmojiController.SpawnEmoji(Control, EmojiId);
					}
					break;
				}
			}
		}
	}
}
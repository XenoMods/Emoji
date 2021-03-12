using System;
using Hazel;
using XenoCore.Utils;

namespace Emoji.Helpers {
	public static class EmojiNetwork {
		public static void Send(CustomRPC CustomRPC) {
			Network.Send((byte) CustomRPC);
		}

		public static void Send(CustomRPC CustomRPC, Action<MessageWriter> WriteData) {
			Network.Send((byte) CustomRPC, WriteData);
		}
	}
}
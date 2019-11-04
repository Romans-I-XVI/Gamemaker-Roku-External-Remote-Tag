using System;
using Microsoft.Xna.Framework;
using MonoEngine;
using NeonPartyGamesController.Rooms;

namespace NeonPartyGamesController.Entities.Buttons
{
	public class ButtonSetName : Button
	{
		private static Sprite _sprite => new Sprite(SpriteSheetHolder.SpriteSheet.GetRegion(AvailableRegions.Buttons.SetName));
		private static Rectangle _collider_rect => new Rectangle(-270 / 2, -140 / 2, 270, 140);

		public ButtonSetName(int x, int y, float scale) : base(x, y, scale, _sprite, _collider_rect, ButtonSetName.SetName) {

		}

		private static void SetName() {
#if ANDROID
			Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(NeonPartyGamesControllerGame.AndroidContext);
			Android.Widget.EditText input = new Android.Widget.EditText(NeonPartyGamesControllerGame.AndroidContext);

			builder.SetTitle("Enter Name");
			input.InputType = Android.Text.InputTypes.ClassText;
			input.SetFilters(new []{ new Android.Text.InputFilterLengthFilter(Settings.MaxNameLength) });
			builder.SetView(input);
			builder.SetPositiveButton("OK", (senderAlert, args) =>
			{
				if (!string.IsNullOrWhiteSpace(input.Text))
				{
					Settings.PlayerName = input.Text.Trim();
				}
			});
			builder.Show();
#elif IOS
#else
	#if DEBUG
			var debugger = Engine.GetFirstInstanceByType<DebuggerWithTerminal>();
			if (debugger != null) {
				Action<string> evaluator = s => {
					if (s != null && s.Trim() != "") {
						Settings.PlayerName = s.Trim();
					};
				};
				debugger.OpenConsoleWithCustomEvaluator(evaluator);
			}
	#endif
#endif
		}
	}
}

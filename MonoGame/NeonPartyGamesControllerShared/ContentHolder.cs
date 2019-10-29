using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoEngine;

namespace NeonPartyGamesController
{
	public static class ContentHolder
	{
		private static readonly Dictionary<AvailableTextures, Texture2D> Textures = new Dictionary<AvailableTextures, Texture2D>();
		private static readonly Dictionary<AvailableMusic, Song> Songs = new Dictionary<AvailableMusic, Song>();
		private static readonly Dictionary<AvailableSounds, SoundEffect> Sounds = new Dictionary<AvailableSounds, SoundEffect>();
		private static readonly Dictionary<AvailableFonts, SpriteFont> Fonts = new Dictionary<AvailableFonts, SpriteFont>();
		private static bool IsInitialized;

		public static Texture2D Get(AvailableTextures texture) {
			if (ContentHolder.IsInitialized)
				return ContentHolder.Textures[texture];
			else
				throw new Exception("You forgot to initialize ContentManager");
		}

		public static SpriteFont Get(AvailableFonts font) {
			if (ContentHolder.IsInitialized)
				return ContentHolder.Fonts[font];
			else
				throw new Exception("You forgot to initialize the ContentManager");
		}

		public static Song Get(AvailableMusic song) {
			if (ContentHolder.IsInitialized)
				return ContentHolder.Songs[song];
			else
				throw new Exception("You forgot to initialize the ContentManager");
		}

		public static SoundEffect Get(AvailableSounds sound) {
			if (ContentHolder.IsInitialized)
				return ContentHolder.Sounds[sound];
			else
				throw new Exception("You forgot to initialize the ContentManager");
		}

		public static void Init(Game game, Dictionary<AvailableTextures, string> custom_texture_locations = null, Dictionary<AvailableFonts, string> custom_font_locations = null, Dictionary<AvailableMusic, string> custom_music_locations = null, Dictionary<AvailableSounds, string> custom_sound_locations = null) {
			if (!ContentHolder.IsInitialized) {
				foreach (AvailableTextures available_texture in Enum.GetValues(typeof(AvailableTextures))) {
					string texture_location = "textures/" + available_texture;
					if (custom_texture_locations != null && custom_texture_locations.ContainsKey(available_texture))
						texture_location = custom_texture_locations[available_texture];

					ContentHolder.Textures.Add(available_texture, game.Content.Load<Texture2D>(texture_location));
				}

				foreach (AvailableFonts available_font in Enum.GetValues(typeof(AvailableFonts))) {
					string font_location = "fonts/" + available_font;
					if (custom_font_locations != null && custom_font_locations.ContainsKey(available_font))
						font_location = custom_font_locations[available_font];

					ContentHolder.Fonts.Add(available_font, game.Content.Load<SpriteFont>(font_location));
				}

				foreach (AvailableMusic available_song in Enum.GetValues(typeof(AvailableMusic))) {
					string song_location = "music/" + available_song;
					if (custom_music_locations != null && custom_music_locations.ContainsKey(available_song))
						song_location = custom_music_locations[available_song];

					Utilities.Try(() => ContentHolder.Songs.Add(available_song, game.Content.Load<Song>(song_location)));
				}

				foreach (AvailableSounds available_sound in Enum.GetValues(typeof(AvailableSounds))) {
					string sound_location = "sounds/" + available_sound;
					if (custom_sound_locations != null && custom_sound_locations.ContainsKey(available_sound))
						sound_location = custom_sound_locations[available_sound];

					Utilities.Try(() => ContentHolder.Sounds.Add(available_sound, game.Content.Load<SoundEffect>(sound_location)));
				}

				ContentHolder.IsInitialized = true;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core {
	public class RelationshipEngine {

		List<Character> loadedCharacters = new List<Character>();
		Dictionary<Character, Conversation> loadedConversationMap = new Dictionary<Character, Conversation>();

		public RelationshipEngine() {
		}

		public void LoadCharacters() {

			var assembly = typeof(RelationshipEngine).GetTypeInfo().Assembly;
			Stream stream = assembly.GetManifestResourceStream("Core.Content.Characters.characters.json");
			string text = "";
			using (var reader = new System.IO.StreamReader(stream)) {
				text = reader.ReadToEnd();
			}

			JToken json = JValue.Parse(text);

			foreach (var child in json) {
				JObject container = child.First as JObject;
				Dictionary<string, object> d = container.ToObject<Dictionary<string, object>>();
				Character character = Character.CreateCharacter(d);
				loadedCharacters.Add(character);
				// TODO this is demo data to put this character somewhere
				if (character.FirstName.Equals("Celilia")) {
					character.FacingDirection = Character.Facing.West;
					character.CurrentWorldPosition = new WorldPosition(1, 0);
				}
			}

		}

		public void LoadConversations() {

			foreach (Character c in this.loadedCharacters) {
				String resourcePath = "Core.Content.Dialog." + c.FirstName + ".dialog.json";
				var assembly = typeof(RelationshipEngine).GetTypeInfo().Assembly;
				Stream stream = assembly.GetManifestResourceStream(resourcePath);
				string text = "";

				if (stream == null) {
					// Warning
					continue; // can't load dialog if there is no file
				}

				using (var reader = new System.IO.StreamReader(stream)) {
					text = reader.ReadToEnd();
				}

				JToken json = JValue.Parse(text);

				foreach (var child in json) {
					JArray dialogs = child.First as JArray;
					loadedConversationMap.Add(c, Conversation.CreateSimpleConverstaion(c, dialogs));
				}

			}

		}

		protected void RebuildConversationDatabase() {
			
		}

		public Character FindCharacter(String name) {
			return loadedCharacters.FindLast(new Predicate<Core.Character>((c) => { return c.FirstName.Equals(name);}));
		}

		public WorldPosition WhereIs(String name) {
			Character character = loadedCharacters.FindLast(new Predicate<Core.Character>((c) => { return c.FirstName.Equals(name); }));

			if (character != null) {
				return character.CurrentWorldPosition;
			}

			return null;
		}

		public Character FindCharacter(WorldPosition position) {
			return loadedCharacters.FindLast(new Predicate<Core.Character>((c) => { return c.CurrentWorldPosition.Compare(position); }));
		}

		// conversations
		protected Character lastCharacterTalkedTo;
		public Conversation currentConversation;

		public Dialog StartConversation(String characterName) {
			return StartConversation(FindCharacter(characterName), null);
		}

		/*
		 * talker<Character> might be the NPC that the player wanted to talk to
		 * In most cases the talker character will be an NPC.
		 */
		public Dialog StartConversation(Character talker, Character listener) {
			// load the conversation

			if (currentConversation != null) {
				if (talker == lastCharacterTalkedTo) {
					return currentConversation.Next();
				}
			}

			if (loadedConversationMap.ContainsKey(talker)) {
				lastCharacterTalkedTo = talker;
				currentConversation = loadedConversationMap[talker];
				return currentConversation.Next();
			} else {
				return null;
			}
		}

		public Dialog Next() {
			return currentConversation.Next();
		}

		// Player action
		public Dialog Speak(Dialog spokenChoice) {
			return currentConversation.Next(spokenChoice);
		}

		public void EndConversation() {
		}

		//interactions
		public void Gift(Character giver, Character receiver, Item gift) {
			// Update the relationship level for the receiver.
		}

		// World space
		public void Update(Calendar calendar) {
		}

	}
}


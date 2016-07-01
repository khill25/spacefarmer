using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Core {
	public class Conversation {

		public List<Character> actors = new List<Character>();
		public List<Dialog> dialogs = new List<Dialog>();

		int lastDialogIndex = -1;

		Character currentlySpeaking;
		Dialog currentlySaying;

		public Conversation() {
		}

		public void AddDialog(Dialog d) {
			dialogs.Add(d);
		}

		public Dialog Next() {

			if (dialogs.Count == 0) return null;

			lastDialogIndex++;
			if (lastDialogIndex >= dialogs.Count) {
				lastDialogIndex = 0;
			}

			return dialogs[lastDialogIndex];
		}

		public Dialog Next(Dialog chosenItem) {
			return null;
		}

		public static Conversation CreateSimpleConverstaion(Character character, JArray json) {

			Conversation conversation = new Conversation();

			foreach (var p in json) {
				String phrase = p.Value<String>();
				Dialog d = new Dialog();
				d.speaker = character;
				d.phrase = phrase;
				conversation.AddDialog(d);
			}

			return conversation;
		}

	}

	public class Dialog {
		public Character speaker;
		public String condition;
		public String phrase;
		public List<Dialog> choices;
	}

	public class DialogCondition {

		string rawCondition;


		public DialogCondition(String rawConditon) {
			this.rawCondition = rawCondition;
		}

		public bool Evaluate() {

			string[] tokens = rawCondition.Split(' ');
			string lhs = tokens[0];
			string compString = tokens[1];
			string rhs = tokens[2];

			if (lhs.StartsWith("%", StringComparison.CurrentCulture)) {
				// variable
				lhs = lhs.Replace("%", "");
			}

			// transform the lhs into a variable we can use
			string[] lhsTokens = lhs.Split('.');
			// root object at index 0
			string objectTyprString = lhsTokens[0];
			Type t = Type.GetType(objectTyprString);
			// get a reference to the object
			// get the property referenced
			// comapre the property based on the comparitor
			// compare to the rhs property using the same 
			// steps as getting the lhs

			return false;
		}

	}
}


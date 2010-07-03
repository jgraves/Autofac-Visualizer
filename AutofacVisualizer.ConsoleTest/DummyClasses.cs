namespace AutofacVisualizer.ConsoleTest {
	public class MakesStrings {
		public MakesStrings(IGiveString stringGiver, string myString) {}
	}

	public interface IGiveString {
		string GimmeString();
	}

	public class UsesString : IGiveString {
		private readonly string gimme;
		private readonly int gimmeInt;

		public UsesString(int gimmeInt, string gimme) {
			this.gimmeInt = gimmeInt;
			this.gimme = gimme;
		}

		#region IGiveString Members

		public string GimmeString() {
			return gimme;
		}

		#endregion
	}

	public class UsesInt : IGiveString {
		private readonly int gimme;

		public UsesInt(int gimme) {
			this.gimme = gimme;
		}

		#region IGiveString Members

		public string GimmeString() {
			return gimme.ToString();
		}

		#endregion
	}
}
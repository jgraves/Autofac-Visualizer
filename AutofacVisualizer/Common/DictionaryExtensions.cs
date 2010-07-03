using System.Collections.Generic;

namespace AutofacVisualizer.Common {
	public static class DictionaryExtensions {
		public static void SafeAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value) {
			if (!dictionary.ContainsKey(key)) {
				dictionary.Add(key, value);
			}
			else dictionary[key] = value;
		}

		public static void SafeRemove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) {
			if (!dictionary.ContainsKey(key)) return;
			dictionary.Remove(key);
		}
	}
}
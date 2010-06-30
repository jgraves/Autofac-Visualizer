using System;
using System.Collections.Generic;
using System.Globalization;
using Graves.Visualizers.Autofac.UI;
using Graves.Visualizers.Autofac.UI.Controls;
using NUnit.Framework;

namespace Graves.Visualizer.Autofac.Tests {

	[TestFixture]
	public class TypeDisplayConverterTest {
	
		[Test]
		public void FormatsClosedGenerics() {
			var type = typeof (List<string>);
			var converter = new TypeDisplayConverter();

			var value = converter.Convert(type, null, null, CultureInfo.CurrentCulture);

			Assert.AreEqual("List<String>", value);
		}
		
		[Test]
		public void FormatsOpenGenerics() {
			var type = typeof (List<>);
			var converter = new TypeDisplayConverter();

			var value = converter.Convert(type, null, null, CultureInfo.CurrentCulture);

			Assert.AreEqual("List<T>", value);
		}


		[Test]
		public void FormatsOpenGenericsWithMultipleParams() {
			var type = typeof (Func<,,>);
			var converter = new TypeDisplayConverter();

			var value = converter.Convert(type, null, null, CultureInfo.CurrentCulture);

			Assert.AreEqual("Func<T1,T2,TResult>", value);
		}
	}
}
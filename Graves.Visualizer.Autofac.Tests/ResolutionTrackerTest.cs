using System.Collections.Generic;
using System.Linq;
using Autofac.Core;
using Graves.Visualizers.Autofac.Data;
using Moq;
using NUnit.Framework;

namespace Graves.Visualizer.Autofac.Tests {

	[TestFixture]
	public class ResolutionTrackerTest {
	
		[Test]
		public void BuildsRegistrationsInRightOrder() {
			var first = new Mock<IComponentRegistration>();
			var second = new Mock<IComponentRegistration>();

			var registrations = new List<IComponentRegistration> {
				first.Object,
				second.Object,
			};
			var tracker = new ResolutionTracker(registrations);

			first.Raise(r => r.Preparing += null);
			second.Raise(r => r.Preparing += null);
			second.Raise(r => r.Activating += null);
			first.Raise(r => r.Activating += null);

			var results = tracker.Activations.First();
			Assert.AreEqual(first, results.Built);
		}
	}
}
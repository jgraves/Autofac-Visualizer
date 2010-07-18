using System.Collections.Generic;
using System.Linq;
using AutofacVisualizer.Data;
using AutofacVisualizer.Data.Structures;
using Moq;
using NUnit.Framework;

namespace AutofacVisualizer.Tests {

	[TestFixture]
	public class ResolutionTrackerTest {

		[Test]
		public void BuildsRegistrationsInRightOrder() {
			var first = new Mock<IComponentRegistrationListener>();
			var second = new Mock<IComponentRegistrationListener>();

			var registrations = new List<IComponentRegistrationListener> {
				first.Object,
				second.Object,
			};

			var tracker = new ResolutionTracker(registrations);

			first.Raise(r => r.Preparing += null, new PreparingObjectEventArgs(typeof(string)));
			second.Raise(r => r.Preparing += null, new PreparingObjectEventArgs(typeof(int)));
			
			second.Raise(r => r.Activating += null, new ActivatingObjectEventArgs(
				typeof(int), typeof(long), null
			));

			first.Raise(r => r.Activating += null, new ActivatingObjectEventArgs(
				typeof(string), typeof(IEnumerable<char>), null
			));

			var expected = new ResolutionTree { Built = typeof(string), 
				Buildees = new List<ResolutionTree> {
					new ResolutionTree{Built = typeof(int)},
				} 
			};
			var results = tracker.Activations;

			Assert.AreEqual(expected.Built, results.Built);
			Assert.IsTrue(expected.Buildees.SequenceEqual(results.Buildees));
		}
		
		[Test]
		public void TracksSubTypes() {
			var first = new Mock<IComponentRegistrationListener>();
			var second = new Mock<IComponentRegistrationListener>();
			var third = new Mock<IComponentRegistrationListener>();

			var registrations = new List<IComponentRegistrationListener> {
				first.Object,
				second.Object,
				third.Object,
			};

			var tracker = new ResolutionTracker(registrations);

			first.Raise(r => r.Preparing += null, new PreparingObjectEventArgs(typeof(string)));
			
			second.Raise(r => r.Preparing += null, new PreparingObjectEventArgs(typeof(int)));
			second.Raise(r => r.Activating += null, new ActivatingObjectEventArgs(
				typeof(int), typeof(int), null
			));

			third.Raise(r => r.Preparing += null, new PreparingObjectEventArgs(typeof(char)));
			third.Raise(r => r.Activating += null, new ActivatingObjectEventArgs(
				typeof(char), typeof(char), null
			));

			first.Raise(r => r.Activating += null, new ActivatingObjectEventArgs(
				typeof(string), typeof(IEnumerable<char>), null
			));

			Assert.AreEqual(2, tracker.Activations.Buildees.Count());
		}
	}
}
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
			var first = new Mock<IRegistration>();
			var second = new Mock<IRegistration>();

			var registrations = new List<IRegistration> {
				first.Object,
				second.Object,
			};

			var tracker = new ResolutionTracker(registrations);

			first.Raise(r => r.Preparing += null, new PreparingObjectEventArgs(typeof(string)));
			second.Raise(r => r.Preparing += null, new PreparingObjectEventArgs(typeof(int)));
			
			second.Raise(r => r.Activating += null, new ActivatingObjectEventArgs(
				typeof(int), typeof(long)
			));

			first.Raise(r => r.Activating += null, new ActivatingObjectEventArgs(
				typeof(string), typeof(IEnumerable<char>)
			));

			var expected = new ActivationData { Built = typeof(string), 
				Buildees = new List<ActivationData> {
					new ActivationData{Built = typeof(int)},
				} 
			};
			var results = tracker.Activations;

			Assert.AreEqual(expected.Built, results.Built);
			Assert.IsTrue(expected.Buildees.SequenceEqual(results.Buildees));
		}
		
		[Test]
		public void TracksSubTypes() {
			var first = new Mock<IRegistration>();
			var second = new Mock<IRegistration>();
			var third = new Mock<IRegistration>();

			var registrations = new List<IRegistration> {
				first.Object,
				second.Object,
				third.Object,
			};

			var tracker = new ResolutionTracker(registrations);

			first.Raise(r => r.Preparing += null, new PreparingObjectEventArgs(typeof(string)));
			
			second.Raise(r => r.Preparing += null, new PreparingObjectEventArgs(typeof(int)));
			second.Raise(r => r.Activating += null, new ActivatingObjectEventArgs(
				typeof(int), typeof(int)
			));

			third.Raise(r => r.Preparing += null, new PreparingObjectEventArgs(typeof(char)));
			third.Raise(r => r.Activating += null, new ActivatingObjectEventArgs(
				typeof(char), typeof(char)
			));

			first.Raise(r => r.Activating += null, new ActivatingObjectEventArgs(
				typeof(string), typeof(IEnumerable<char>)
			));

			Assert.AreEqual(2, tracker.Activations.Buildees.Count());
		}
	}
}
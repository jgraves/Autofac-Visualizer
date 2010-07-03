using System.Collections.Generic;
using System.Linq;
using Autofac.Core;
using AutofacVisualizer.Data.Structures;
using Moq;
using NUnit.Framework;

namespace AutofacVisualizer.Tests {

	[TestFixture]
	public class ServiceDefinitionsTest {

		[Test]
		public void CollapsesIdenticalServices() {
			var registration1 = new Mock<IComponentRegistration>();
			registration1.Setup(s => s.Services).Returns(new List<Service> {
				new TypedService(typeof(string))
			});

			var activator1 = new Mock<IInstanceActivator>();
			activator1.Setup(a => a.LimitType).Returns(typeof(IEnumerable<char>));
			registration1.Setup(s => s.Activator).Returns(activator1.Object);


			var registration2 = new Mock<IComponentRegistration>();
			registration2.Setup(s => s.Services).Returns(new List<Service> {
				new TypedService(typeof(string))
			});

			var activator2 = new Mock<IInstanceActivator>();
			activator2.Setup(a => a.LimitType).Returns(typeof(string));
			registration2.Setup(s => s.Activator).Returns(activator2.Object);

			var services = new ServiceDefinitions(new List<IComponentRegistration> { registration1.Object, registration2.Object });

			Assert.AreEqual(1, services.Count());
		}
	}
}
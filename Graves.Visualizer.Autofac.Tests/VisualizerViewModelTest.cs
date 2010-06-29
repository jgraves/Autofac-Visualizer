using System;
using System.Collections.Generic;
using System.Linq;
using Graves.Visualizers.Autofac.Data;
using Graves.Visualizers.Autofac.Data.Structures;
using Graves.Visualizers.Autofac.UI;
using Moq;
using NUnit.Framework;
namespace Graves.Visualizer.Autofac.Tests {
	
	[TestFixture]
	public class VisualizerViewModelTest {
		[Test]	
		public void FiltersRegistrationsBasedOnFilterText() {
			var objectSource = new Mock<IObjectSource>();
			var stringService = new ServiceDefinition{ServiceType = typeof(string), RegisteredTypes = new List<Type>{typeof(string)}};
			var objectService = new ServiceDefinition{ServiceType = typeof(object), RegisteredTypes = new List<Type>{typeof(object)}};

			objectSource.Setup(o => o.GetRegistrations()).Returns(
				new List<ServiceDefinition> {
					stringService,
					objectService,
				}
			);
			
			var visualizerViewModel = new VisualizerViewModel(objectSource.Object);

			visualizerViewModel.FilterText = "s";

			Assert.IsTrue(visualizerViewModel.Services.Filter(stringService));
			Assert.IsFalse(visualizerViewModel.Services.Filter(objectService));
			
			visualizerViewModel.FilterText = "o";

			Assert.IsFalse(visualizerViewModel.Services.Filter(stringService));
			Assert.IsTrue(visualizerViewModel.Services.Filter(objectService));
		}
	}
}
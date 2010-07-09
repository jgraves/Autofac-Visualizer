using System;
using System.Collections.Generic;
using AutofacVisualizer.Data;
using AutofacVisualizer.Data.Structures;
using AutofacVisualizer.UI;
using Moq;
using NUnit.Framework;

namespace AutofacVisualizer.Tests {

  [TestFixture]
  public class VisualizerViewModelTest {
    [Test]
    public void FiltersRegistrationsBasedOnFilterText() {
      var objectSource = new Mock<IContainerInfo>();
      var stringService = new ServiceDefinition { ServiceType = typeof(string), RegisteredTypes = new List<Type> { typeof(string) } };
      var objectService = new ServiceDefinition { ServiceType = typeof(object), RegisteredTypes = new List<Type> { typeof(object) } };

      objectSource.Setup(o => o.GetServices()).Returns(
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

    [Test]
    public void DefaultsToContainerView() {
      var objectSource = new Mock<IContainerInfo>();

    	var visualizerViewModel = new VisualizerViewModel(objectSource.Object);


      Assert.AreEqual(View.Container, visualizerViewModel.CurrentView);
    }
    
    
    [Test]
    public void BuildCommandSwitchesToBuildMapView() {
      var objectSource = new Mock<IContainerInfo>();
      var stringService = new ServiceDefinition();
      objectSource.Setup(o => o.GetServices()).Returns(
        new List<ServiceDefinition> {
					stringService,
				}
      );

      var visualizerViewModel = new VisualizerViewModel(objectSource.Object);
      visualizerViewModel.Services.MoveCurrentToFirst();
      visualizerViewModel.BuildCommand.Execute(null);

      Assert.AreEqual(View.BuildMap, visualizerViewModel.CurrentView);
    }
    
    [Test]
    public void ReturnToContainerCommandSwitchesToContainerView() {
      var objectSource = new Mock<IContainerInfo>();
      var stringService = new ServiceDefinition();
      objectSource.Setup(o => o.GetServices()).Returns(
        new List<ServiceDefinition> {
					stringService,
				}
      );

      var visualizerViewModel = new VisualizerViewModel(objectSource.Object);
      visualizerViewModel.Services.MoveCurrentToFirst();
      visualizerViewModel.BuildCommand.Execute(null);


      visualizerViewModel.ReturnToContainerCommand.Execute(null);
      
      Assert.AreEqual(View.Container, visualizerViewModel.CurrentView);
    }
  }
}
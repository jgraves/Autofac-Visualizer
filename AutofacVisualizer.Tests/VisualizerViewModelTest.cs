using System;
using System.Collections.Generic;
using AutofacVisualizer.Data;
using AutofacVisualizer.Data.Interfaces;
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
      var stringService = new ComponentRegistration { Type = typeof(string), Services = new List<TypedService> { new TypedService{Type = typeof(string) }} };
      var objectService = new ComponentRegistration { Type = typeof(object), Services = new List<TypedService> { new TypedService{Type = typeof(object) } }};

      objectSource.Setup(o => o.GetServices()).Returns(
        new List<ComponentRegistration> {
					stringService,
					objectService,
				}
      );

      var visualizerViewModel = new VisualizerViewModel(objectSource.Object);

      visualizerViewModel.FilterText = "s";

      Assert.IsTrue(visualizerViewModel.Components.Filter(stringService));
      Assert.IsFalse(visualizerViewModel.Components.Filter(objectService));

      visualizerViewModel.FilterText = "o";

      Assert.IsFalse(visualizerViewModel.Components.Filter(stringService));
      Assert.IsTrue(visualizerViewModel.Components.Filter(objectService));
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
      var stringService = new ComponentRegistration();
      objectSource.Setup(o => o.GetServices()).Returns(
        new List<ComponentRegistration> {
					stringService,
				}
      );

      var visualizerViewModel = new VisualizerViewModel(objectSource.Object);
      visualizerViewModel.Components.MoveCurrentToFirst();
      visualizerViewModel.BuildCommand.Execute(stringService);

      Assert.AreEqual(View.BuildMap, visualizerViewModel.CurrentView);
    }
    
    [Test]
    public void ReturnToContainerCommandSwitchesToContainerView() {
      var objectSource = new Mock<IContainerInfo>();
      var stringService = new ComponentRegistration();
      objectSource.Setup(o => o.GetServices()).Returns(
        new List<ComponentRegistration> {
					stringService,
				}
      );

      var visualizerViewModel = new VisualizerViewModel(objectSource.Object);
      visualizerViewModel.Components.MoveCurrentToFirst();
      visualizerViewModel.BuildCommand.Execute(null);


      visualizerViewModel.ReturnToContainerCommand.Execute(null);
      
      Assert.AreEqual(View.Container, visualizerViewModel.CurrentView);
    }
  }
}
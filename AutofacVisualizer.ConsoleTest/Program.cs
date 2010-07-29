using System;
using System.Collections.Generic;
using System.Windows;
using Autofac;
using AutofacContrib.Profiling;
using AutofacVisualizer.Data;
using AutofacVisualizer.Data.Interfaces;
using AutofacVisualizer.Data.Structures;
using AutofacVisualizer.UI;
using NGenerics.DataStructures.Trees;

namespace AutofacVisualizer.ConsoleTest {

  public class Program {

    [STAThread]
    private static void Main() {
      var builder = new ContainerBuilder();

      builder.RegisterModule<ProfilingModule>();

      builder.RegisterType<List<string>>().As<IEnumerable<string>>();

      builder.RegisterGeneric(typeof(BinaryTree<>)).As(typeof(ITree<>));
      builder.Register(c => new TreeWrapper(c.Resolve<ITree<int>>(), c.Resolve<IEnumerable<string>>()));

      builder.RegisterType<UsesInt>().As<IGiveString>().Named<object>("My Name");
      builder.RegisterType<UsesString>().As<IGiveString>();

      builder.Register(c => "hello");
      builder.Register(c => 7);

      using (var container = builder.Build()) {

        var vm = new VisualizerViewModel(new TestContainerInfo(container));
        var window = new Window {
          Content = new VisualizerControl(vm) {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch
          },
          Width = 600,
          Height = 600
        };
        window.ShowDialog();
        //AutofacVisualizer.VS2010.VisualizerDialog.TestShowVisualizer(container);
      }
    }

    private class TestContainerInfo : IContainerInfo {
      private readonly ContainerRepository containerRepository;

      public TestContainerInfo(IContainer container) {
        containerRepository = new ContainerRepository(container);
      }

      public IEnumerable<ComponentRegistration> GetServices() {
        return containerRepository.GetComponents();
      }

      public ResolutionTree GetBuildMap(Guid componentId) {
        return containerRepository.GetBuildMap(componentId);
      }
    }
  }
}
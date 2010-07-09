using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using Autofac;
using AutofacVisualizer.Data;
using AutofacVisualizer.Data.Structures;
using AutofacVisualizer.UI;

namespace AutofacVisualizer.ConsoleTest {

	public class Program {

		[STAThread]
		private static void Main() {
			var builder = new ContainerBuilder();

			builder.RegisterType<List<string>>().As<IEnumerable<string>>().OnRegistered(args => args.ToString());
			builder.RegisterType<List<object>>().As<IEnumerable<object>>();
			builder.RegisterType<List<StringBuilder>>().As<IEnumerable<StringBuilder>>();
			builder.RegisterType<Dictionary<string, object>>().As<IDictionary<string, object>>();
			builder.RegisterType<HashSet<StringBuilder>>().As<IEnumerable<StringBuilder>>();
			builder.RegisterType<IEnumerable<StringBuilder>>().As<IEnumerable<StringBuilder>>();

			builder.Register(c => 1);
			builder.Register(c => "string");

			builder.RegisterType<UsesInt>().As<IGiveString>();
			builder.RegisterType<UsesString>().As<IGiveString>();
			builder.RegisterAssemblyTypes(Assembly.LoadFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\NGenerics.dll"));
			builder.RegisterType<MakesStrings>();
			using (var container = builder.Build()) {

				var vm = new VisualizerViewModel(new TestContainerInfo( container));
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

			public IEnumerable<ServiceDefinition> GetServices() {
				return containerRepository.GetServices();
			}

			public ActivationData GetBuildMap(ServiceDefinition item) {
				return containerRepository.GetBuildMap(item);
			}
		}
	}
}
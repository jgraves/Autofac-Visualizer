using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using AutofacVisualizer.Data;
using AutofacVisualizer.Data.Structures;

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

        //var vm = new VisualizerViewModel(new TestObjectSource(container));
        //new Window() {
        //  Content = new VisualizerControl(vm) {
        //    HorizontalAlignment = HorizontalAlignment.Stretch,
        //    VerticalAlignment = VerticalAlignment.Stretch
        //  },
        //  Width = 600,
        //  Height = 600
        //}.ShowDialog();
				AutofacDialogVisualizer.TestShowVisualizer(container);
			}
		}

		private class TestObjectSource : IObjectSource {
			private readonly IContainer container;

			public TestObjectSource(IContainer container) {
				this.container = container;
			}

			public IEnumerable<ServiceDefinition> GetRegistrations() {
				return new ServiceDefinitions(container.ComponentRegistry.Registrations);
			}

			public ActivationData GetBuildMap(ServiceDefinition item) {

				var wrappedRegistrations = container.ComponentRegistry.Registrations.Select(r => new Registration(r)).ToList();

				ActivationData data;
				using (var tracker = new ResolutionTracker(wrappedRegistrations)) {
					object registration;
					container.TryResolve(item.ServiceType, out registration);
					data = tracker.Activations;
				}

				return data;
			}
		}
	}
}
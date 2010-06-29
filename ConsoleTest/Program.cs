using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Graves.Visualizers.Autofac.Data;
using Autofac;

namespace Graves.Visualizers.Test {
	
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
				AutofacVisualizer.TestShowVisualizer(container);
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Graves.Visualizers.Autofac.Data;

namespace Graves.Visualizers.Test {
	
	public class Program {

		[STAThread]
		private static void Main() {
			var builder = new ContainerBuilder();

			builder.RegisterType<List<string>>().As<IEnumerable<object>>().OnRegistered(args => args.ToString());
			builder.RegisterType<List<object>>().As<IEnumerable<object>>();
			builder.RegisterType<List<StringBuilder>>().As<IEnumerable<object>>();

			builder.Register(c => 1);
			builder.Register(c => "string");

			builder.RegisterType<UsesInt>().As<IGiveString>();
			builder.RegisterType<UsesString>().As<IGiveString>();

			builder.RegisterType<MakesStrings>();

			using (var container = builder.Build()) AutofacVisualizer.TestShowVisualizer(container);
		}
	}
}
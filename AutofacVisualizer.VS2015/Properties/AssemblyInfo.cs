using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using Autofac;
using Autofac.Core;
using AutofacVisualizer.Data;
using AutofacVisualizer.VS2015;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

[assembly: AssemblyTitle("DebuggerVisualizer")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Administrator")]
[assembly: AssemblyProduct("DebuggerVisualizer")]
[assembly: AssemblyCopyright("Copyright � Administrator 2010")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM

[assembly: Guid("a36e82e3-6870-4773-977e-db13a5789013")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]

[assembly: AssemblyVersion("1.0.0.0")]
[assembly:
    DebuggerVisualizer(typeof(VisualizerDialog), typeof(VisualizerDataSource), Target = typeof(Container))]
[assembly: AssemblyFileVersion("1.0.0.0")]
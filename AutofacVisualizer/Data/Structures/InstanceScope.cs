namespace AutofacVisualizer.Data.Structures {

    public enum InstanceScope {
        Unknown,
        PerDependency,
        PerLifetimeScope,
        PerMatchingLifetimeScope,
        SingleInstance
    }
}

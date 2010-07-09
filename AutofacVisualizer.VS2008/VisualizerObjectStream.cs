using AutofacVisualizer.Data;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace AutofacVisualizer.VS2008 {
  public class VisualizerObjectStream : IObjectStream {
    private readonly IVisualizerObjectProvider provider;

    public VisualizerObjectStream(IVisualizerObjectProvider provider) {
      this.provider = provider;
    }

    public T GetObject<T>() {
      return (T)provider.GetObject();
    }

    public TReturn SendObject<T, TReturn>(T obj) {
      return (TReturn)provider.TransferObject(obj);
    }
  }
}
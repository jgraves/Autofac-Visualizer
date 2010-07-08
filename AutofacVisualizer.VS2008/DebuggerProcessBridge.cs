using AutofacVisualizer.Data;

namespace AutofacVisualizer.VS2008 {
  public class DebuggerProcessBridge : IBridgeDebuggerProcess {
    private readonly IVisualizerObjectProvider provider;

    public DebuggerProcessBridge(IVisualizerObjectProvider provider) {
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
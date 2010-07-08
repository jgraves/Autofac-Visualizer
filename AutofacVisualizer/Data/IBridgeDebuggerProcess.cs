namespace AutofacVisualizer.Data {
  public interface IBridgeDebuggerProcess {
    T GetObject<T>();
    TReturn SendObject<T, TReturn>(T obj);
  }
}
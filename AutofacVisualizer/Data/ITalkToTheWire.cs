namespace AutofacVisualizer.Data {
  public interface ITalkToTheWire {
    T GetObject<T>();
    TReturn SendObject<T, TReturn>(T obj);
  }
}
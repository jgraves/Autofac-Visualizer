namespace AutofacVisualizer.Data {
  public interface IObjectProvider {
    T GetObject<T>();
    TReturn SendObject<T, TReturn>(T obj);
  }
}
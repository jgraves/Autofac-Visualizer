namespace AutofacVisualizer.Data {
	public interface IObjectStream {
		T GetObject<T>();
		TReturn SendObject<T, TReturn>(T obj);
	}
}
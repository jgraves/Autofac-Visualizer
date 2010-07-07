using System;
using AutofacVisualizer.Data;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace AutofacVisualizer.VS2010 {
  public class WireTalker : ITalkToTheWire {
    private readonly IVisualizerObjectProvider provider;

    public WireTalker(IVisualizerObjectProvider provider) {
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
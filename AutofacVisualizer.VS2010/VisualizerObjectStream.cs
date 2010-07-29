using System;
using AutofacVisualizer.Data;
using AutofacVisualizer.Data.Interfaces;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace AutofacVisualizer.VS2010 {
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
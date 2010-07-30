using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using AutofacVisualizer.Common;
using AutofacVisualizer.Data;
using AutofacVisualizer.Data.Interfaces;
using AutofacVisualizer.Data.Structures;
using AutofacVisualizer.UI.Core;

namespace AutofacVisualizer.UI {

  public enum View {
    BuildMap,
    Container
  }

  public class VisualizerViewModel : BaseViewModel<VisualizerViewModel>, IVisualizerViewModel {

    private readonly IContainerInfo container;

    private ResolutionTree buildMap;
    private ICollectionView components;

    public VisualizerViewModel(IContainerInfo container) {
      this.container = container;
      BuildCommand = new RelayCommand(Build, o1 => Components.CurrentItem != null);
      ReturnToContainerCommand = new RelayCommand(o => CurrentView = View.Container, o1 => true);
      CurrentView = View.Container;
      RefreshTypes();
    }

    public ICommand BuildCommand { get; private set; }
    public ICommand ReturnToContainerCommand { get; private set; }

    public IEnumerable<ResolutionTree> BuildMap {
      get { yield return buildMap; }
    }

    private View currentView;

    public View CurrentView {
      get { return currentView; }
      private set {
        currentView = value;
        NotifyPropertyChanged(vm => vm.CurrentView);
      }
    }

    public ICollectionView Components {
      get { return components; }
      private set {
        components = value;
        NotifyPropertyChanged(vm => vm.Components);
      }
    }

    private string filterText = String.Empty;

    public string FilterText {
      get { return filterText; }
      set {
        filterText = value;
        Components.Filter =
          delegate(object o) {
            Func<Type, bool> contains = t => t.ToGenericTypeString().ToLower().Contains(value.ToLower());
            var definition = ((ComponentRegistration)o);
            return contains(definition.Type) || definition.Services.Select(s => s.Type).Any(contains);
          };
        NotifyPropertyChanged(vm => vm.FilterText);
      }
    }

    private void RefreshTypes() {
      Components = container.GetServices().ToView();
    }

    private void Build(object obj) {
      var item = obj as ComponentRegistration;
      if (item == null) return;

      buildMap = container.GetBuildMap(item.Id);
      CurrentView = View.BuildMap;
    }
  }
}
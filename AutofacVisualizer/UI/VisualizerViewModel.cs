using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using AutofacVisualizer.Common;
using AutofacVisualizer.Data;
using AutofacVisualizer.Data.Structures;
using AutofacVisualizer.UI.Core;

namespace AutofacVisualizer.UI {

  public enum View {
    BuildMap,
    Container
  }

  public class VisualizerViewModel : BaseViewModel<VisualizerViewModel>, IVisualizerViewModel {

    private readonly IContainerSource containerSource;

    private ActivationData buildMap;
    private ICollectionView services;

    public VisualizerViewModel(IContainerSource containerSource) {
      this.containerSource = containerSource;
      BuildCommand = new RelayCommand(o => Build(o), o1 => Services.CurrentItem != null);
      ReturnToContainerCommand = new RelayCommand(o => CurrentView = View.Container, o1 => true);
      CurrentView = View.Container;
      RefreshTypes();
    }

    public ICommand BuildCommand { get; private set; }
    public ICommand ReturnToContainerCommand { get; private set; }

    public ActivationData BuildMap {
      get { return buildMap; }
      private set {
        buildMap = value;
        NotifyPropertyChanged(vm => vm.BuildMap);
      }
    }

    private View currentView;

    public View CurrentView {
      get { return currentView; }
      private set {
        currentView = value;
        NotifyPropertyChanged(vm => vm.CurrentView);
      }
    }

    public ICollectionView Services {
      get { return services; }
      private set {
        services = value;
        NotifyPropertyChanged(vm => vm.Services);
      }
    }

    private string filterText = String.Empty;

    public string FilterText {
      get { return filterText; }
      set {
        filterText = value;
        Services.Filter =
          delegate(object o) {
            Func<Type, bool> contains = t => t.ToGenericTypeString().ToLower().Contains(value.ToLower());
            var definition = ((ServiceDefinition)o);
            return contains(definition.ServiceType) || definition.RegisteredTypes.Any(contains);
          };
        NotifyPropertyChanged(vm => vm.FilterText);
      }
    }

    private void RefreshTypes() {
      Services = containerSource.GetRegistrations().ToView();
    }

    private void Build(object obj) {
      var item = obj as ServiceDefinition;
      if (item == null) return;

      BuildMap = containerSource.GetBuildMap(item);
      CurrentView = View.BuildMap;
    }
  }
}
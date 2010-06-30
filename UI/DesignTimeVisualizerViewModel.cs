using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Graves.Visualizers.Autofac.Common;
using Graves.Visualizers.Autofac.Data.Structures;
using Graves.Visualizers.Autofac.UI.Controls;
using NGenerics.DataStructures.Mathematical;
using QuickGraph;

namespace Graves.Visualizers.Autofac.UI {

  public class DesignTimeVisualizerViewModel : IVisualizerViewModel {

    public ICommand BuildCommand {
      get { return null; }
    }

    public ActivationData BuildMap {
      get {
        return
          new ActivationData {
            Built = typeof(string),
            Buildees = new List<ActivationData> {
						 new ActivationData{Built	= typeof(int)},
						 new ActivationData{Built	= typeof(string)},
						 new ActivationData{Built	= typeof(long), Buildees=
						 new List<ActivationData> {
						 	new ActivationData{Built = typeof(DateTime)},
						 	new ActivationData{Built = typeof(ReflectionControl)},
						 	new ActivationData{Built = typeof(IEnumerable<>)},
						 }},
						}
          };
      }
    }

    public ICollectionView Services {
      get {
        return new List<ServiceDefinition> {
					new ServiceDefinition{ServiceType = typeof(IVector<>), 
						RegisteredTypes = new List<Type>{typeof(IVector<string>), typeof(IVector<int>)}
					},
					new ServiceDefinition{ServiceType = typeof(IEnumerable<string>), 
						RegisteredTypes = new List<Type>{typeof(List<IEnumerable<char>>), typeof(List<string>)}
					},
					new ServiceDefinition{ServiceType = typeof(IEnumerable<char>), 
						RegisteredTypes = new List<Type>{typeof(string)}
					},
					new ServiceDefinition{ServiceType = typeof(IEnumerable<char>), 
						RegisteredTypes = new List<Type>{typeof(string)}
					},
					new ServiceDefinition{ServiceType = typeof(IEnumerable<char>), 
						RegisteredTypes = new List<Type>{typeof(string)}
					},
					new ServiceDefinition{ServiceType = typeof(IEnumerable<char>), 
						RegisteredTypes = new List<Type>{typeof(string)}
					},
					new ServiceDefinition{ServiceType = typeof(IEnumerable<char>), 
						RegisteredTypes = new List<Type>{typeof(string)}
					}
				}
        .ToView();
      }
    }

    public string FilterText {
      get { return "Filter"; }
      set { }
    }

    public View CurrentView {
      get {
        return View.Container;
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
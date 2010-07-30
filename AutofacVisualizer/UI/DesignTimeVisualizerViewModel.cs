using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using AutofacVisualizer.Common;
using AutofacVisualizer.Data;
using AutofacVisualizer.Data.Structures;
using AutofacVisualizer.UI.Controls;
using NGenerics.DataStructures.Mathematical;

namespace AutofacVisualizer.UI {

  public class DesignTimeVisualizerViewModel : IVisualizerViewModel {

    public ICommand BuildCommand {
      get { return null; }
    }

    public IEnumerable<ResolutionTree> BuildMap {
      get {
        yield return
          new ResolutionTree {
            Built =
              new ComponentRegistration {
                Type = typeof(string)
              },
            Buildees = new List<ResolutionTree> {
              new ResolutionTree {
                Built = new ComponentRegistration {Type = typeof (IVector<>)},
                Buildees = new List<ResolutionTree> {
                  new ResolutionTree {
                    Built = new ComponentRegistration{Type = typeof(IEnumerable<string>)}
                  }
                }
              },
              new ResolutionTree {
                Built = new ComponentRegistration {Type = typeof (int)}
              }
            }
          };
      }
    }

    public ICollectionView Components {
      get {
        return new List<ComponentRegistration> {
					new ComponentRegistration{Type = typeof(IVector<>), 
						Services = new List<TypedService> {
							new TypedService{Type = typeof(IVector<string>)},
							new TypedService{Type = typeof(IVector<int>)}
						}
					},
					new ComponentRegistration{Type = typeof(IEnumerable<string>), 
						Services = new List<TypedService>{new TypedService{Type = typeof(List<IEnumerable<char>>)}
							, 
							new TypedService{Type = typeof(List<string>)}
					}},
					new ComponentRegistration{
						Type = typeof(IEnumerable<char>), 
						Services = new List<TypedService>{new TypedService{Type = typeof(string)}
					}},
					new ComponentRegistration{Type = typeof(IEnumerable<char>), 
						Services = new List<TypedService>{new TypedService{Type = typeof(string)}}
					},
					new ComponentRegistration{Type = typeof(IEnumerable<char>), 
						Services = new List<TypedService>{new TypedService{Type = typeof(string)}}
					},
					new ComponentRegistration{Type = typeof(IEnumerable<char>), 
						Services = new List<TypedService>{new TypedService{Type = typeof(string)}}
					},
					new ComponentRegistration{Type = typeof(IEnumerable<char>), 
						Services = new List<TypedService>{new TypedService{Type = typeof(string)}}
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
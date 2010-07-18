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

		public ResolutionTree BuildMap {
			get {
				return
					new ResolutionTree {
						Built = typeof(string),
						Buildees = new List<ResolutionTree> {
						 new ResolutionTree{Built	= typeof(int)},
						 new ResolutionTree{Built	= typeof(string),
                             Buildees = new List<ResolutionTree> {
                                                new ResolutionTree{Built = typeof(TextBox)},
                                                new ResolutionTree{Built = typeof(ListBox)},
                                            }},
						 new ResolutionTree{Built	= typeof(long), Buildees=
						 new List<ResolutionTree> {
						 	new ResolutionTree{Built = typeof(DateTime)},
						 	new ResolutionTree{Built = typeof(ReflectionControl)},
						 	new ResolutionTree{Built = typeof(IEnumerable<>)},
						 }},
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
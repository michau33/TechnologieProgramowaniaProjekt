using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using TpProjektForms.Utility.Commands;
using TpProjektForms.ViewModels.Assembly;
using TpProjektForms.ViewModels.Base;
using TpProjektForms.ViewModels.TreeView;
using TpProjektModel.Reflection;
using TpProjektModel.Reflection.Model;
using TpProjektModel.Serialization;

namespace TpProjektForms.ViewModels.Dashboard
{
    public class DashboardViewModel : BaseViewModel, IDashboardViewModel
    {
        public ObservableCollection<ITreeNodeViewModel> TreeNodes
        {
            get => treeNodes;
            set { treeNodes = value; OnPropertyChanged(); }
        }

        public bool IsTreeViewBuilt
        {
            get => isTreeViewBuilt;
            set { isTreeViewBuilt = value; OnPropertyChanged(); }
        }

        public AssemblyModel CurrentlyLoadedAssemblyModel { get; set; }

        public ICommand LoadFromFileCommand 
            => new RelayCommand(OpenFile, null);

        public ICommand SaveToFileCommand 
            => new RelayCommand(SaveFile, null);

        public ICommand ExitAppCommand => 
            new RelayCommand((sender) => Application.Current.Shutdown(), null);

        public readonly IModelSerializer ModelSerializer;
        public IReflector Reflector { get; private set; }
        private bool isTreeViewBuilt;
        private ObservableCollection<ITreeNodeViewModel> treeNodes;

        public DashboardViewModel(IReflector reflector, IModelSerializer modelSerializer)
        {
            Reflector = reflector ?? throw new ArgumentNullException(nameof(reflector));
            ModelSerializer = modelSerializer ?? throw new ArgumentNullException(nameof(modelSerializer));
        }

        void OpenFile(object sender)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Dynamic Library File (*.dll)| *.dll|" + "XML File (*.xml)| *.xml"
            };

            if (fileDialog.ShowDialog() == true)
            {
                LoadAssemblyFromFile(fileDialog.FileName);
            }
        }

        void LoadAssemblyFromFile(string path)
        {
            AssemblyModel model = null;

            if (path.Contains(".dll"))
            {
                Reflector.Reflect(path);
                model = Reflector.AssemblyModel;
            }

            if (path.Contains(".xml"))
            {
                Reflector = ModelSerializer.Read<Reflector>(path);
                model = Reflector.AssemblyModel;
            }

            CurrentlyLoadedAssemblyModel = model ?? throw new ArgumentNullException(nameof(model));
            AssemblyViewModel assemblyViewModel = new AssemblyViewModel(CurrentlyLoadedAssemblyModel);

            TreeNodes = new ObservableCollection<ITreeNodeViewModel> {assemblyViewModel};
            IsTreeViewBuilt = true;
        }

        void SaveFile(object sender)
        {
            if (CurrentlyLoadedAssemblyModel == null)
            {
                MessageBox.Show("There's nothing to deserialize. Load assembly first.", "Error");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "model",
                Filter = "XML file (*.xml)|*.xml",
                FilterIndex = 2,
                RestoreDirectory = true,
            };


            if (saveFileDialog.ShowDialog() == true)
            {
                if (saveFileDialog.CheckPathExists)
                    ModelSerializer.Write(Reflector, saveFileDialog.FileName);

                MessageBox.Show($"Assembly model serialized to file with the following path: {saveFileDialog.FileName}.", "Success");
            }

        }
    }
}
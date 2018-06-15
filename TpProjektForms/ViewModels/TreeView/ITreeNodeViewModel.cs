using System.Collections.ObjectModel;

namespace TpProjektForms.ViewModels.TreeView
{
    public interface ITreeNodeViewModel
    {
        string Name { get; }
        bool IsExpanded { get; }

        TreeNodeViewModel Parent { get; }
        ObservableCollection<TreeNodeViewModel> Children { get; }
    }
}
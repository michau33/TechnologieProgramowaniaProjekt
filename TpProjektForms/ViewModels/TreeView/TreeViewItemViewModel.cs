using System.Collections.ObjectModel;
using TpProjektForms.ViewModels.Base;

namespace TpProjektForms.ViewModels.TreeView
{
    public abstract class TreeViewItemViewModel : BaseViewModel
    { 
        private readonly TreeViewEmptyItemViewModel _emptyChild;

        public ObservableCollection<TreeViewItemViewModel> Relations { get; }
        public TreeViewItemViewModel Parent { get; }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (value == _isExpanded)
                    return;

                _isExpanded = value;
                OnPropertyChanged("IsExpanded");

                if (_isExpanded && Parent != null)
                    Parent.IsExpanded = true;

                if (HasEmptyChild)
                {
                    Relations.Remove(_emptyChild);
                    LoadChildren();
                }
            }
        }

        public bool HasEmptyChild => (Relations.Count == 1 && Relations[0] == _emptyChild);

        protected TreeViewItemViewModel(TreeViewItemViewModel parent, bool shouldLazyLoadChildren)
            : this()
        {
            Parent = parent;

            if (!shouldLazyLoadChildren)
                return;

            _emptyChild = new TreeViewEmptyItemViewModel();
            Relations.Add(_emptyChild);
        }

        protected TreeViewItemViewModel()
        {
            Relations = new ObservableCollection<TreeViewItemViewModel>();
        }

        protected abstract void LoadChildren();
    }
}
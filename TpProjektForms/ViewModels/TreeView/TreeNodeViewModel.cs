using System.Collections.ObjectModel;
using TpProjektForms.ViewModels.Base;

namespace TpProjektForms.ViewModels.TreeView
{
    public abstract class TreeNodeViewModel : BaseViewModel, ITreeNodeViewModel
    {
        public string Name { get; set; }

        public ObservableCollection<TreeNodeViewModel> Children { get; set; }
        public TreeNodeViewModel Parent { get; set; }

        public bool HasEmptyChild => (Children.Count == 1 && Children[0] is EmptyTreeNodeNode);

        bool isExpanded;
        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                if (value == isExpanded)
                    return;

                isExpanded = value;
                OnPropertyChanged("IsExpanded");

                if (isExpanded && Parent != null)
                    Parent.IsExpanded = true;

                if (HasEmptyChild)
                {
                    Children.Remove(Children[0]);
                    LoadChildren();
                }
            }
        }

        protected TreeNodeViewModel()
        {
            Children = new ObservableCollection<TreeNodeViewModel>();
        }

        protected TreeNodeViewModel(string name, TreeNodeViewModel parent)
            : this()
        {
            Name = name;
            Parent = parent;
        }

        protected void AddDummyChild()
        {
            Children.Add(new EmptyTreeNodeNode());
        }


        protected virtual void LoadChildren()
        {
            Children.Clear();
        }
    }
}
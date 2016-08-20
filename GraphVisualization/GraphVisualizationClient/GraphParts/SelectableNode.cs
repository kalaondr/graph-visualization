using System.ComponentModel;

namespace GraphVisualizationClient.GraphParts
{
    /// <summary>
    /// Represents a selectable node with id and label.
    /// </summary>
    public class SelectableNode : INotifyPropertyChanged
    {
        private string id;

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged(nameof(Id));
            }
        }

        private string label;

        public string Label
        {
            get { return label; }
            set
            {
                label = value;
                NotifyPropertyChanged(nameof(Label));
            }
        }

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                NotifyPropertyChanged(nameof(IsSelected));
            }
        }

        public SelectableNode(string id, string label, bool isSelected = false)
        {
            Id = id;
            Label = label;
            IsSelected = isSelected;
        }

        public override string ToString()
        {
            return Label;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
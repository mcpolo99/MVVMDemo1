using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace MVVM_LIB
{
    public class UICommand1 : RelayCommandHandlerObject, INotifyPropertyChanged
    {
        // Constructors for various use cases
        public UICommand1(string name, ICommand command, string localName, string localDescription, ImageSource imageSource = null, Func<bool> canExecuteFunc = null, object commandParam = null)
            : base((o) => command.Execute(o), (o) => (canExecuteFunc?.Invoke() ?? true) && command.CanExecute(o))
        {
            this._name = name;
            this._localName = localName;
            this._localDescription = localDescription;
            this._imageSource = imageSource;
            this._commandParameter = commandParam;
            this.HandleEvent = true;
        }

        public UICommand1(string name, ICommand command, string localName, string localDescription, string imagePath, Func<bool> canExecuteFunc = null, object commandParam = null)
            : this(name, command, localName, localDescription, new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute)), canExecuteFunc, commandParam)
        {
        }

        // Simplified constructor
        public UICommand1(string name)
            : base((o) => { }, (o) => true)
        {
            this._name = name;
            this.HandleEvent = true;
        }

        // Properties
        public object CommandParameter => this._commandParameter;
        public string Name => this._name;
        public string LocalName => this._localName;
        public string LocalDescription => this._localDescription;
        public virtual bool HandleEvent { get; set; }

        public ImageSource Icon => _imageSource ?? (_imagePath != null && Path.IsPathRooted(_imagePath) ? new BitmapImage(new Uri(_imagePath)) : null);

        public virtual bool ForDevelopers => false;

        public virtual string FormattedShortcut => null;

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Automation properties
        public Guid UId { get; } = Guid.NewGuid();
        public string AutomationIdViewModel => null;
        public string AutomationIdControl => null;
        public string AutomationIdProperty => this.Name;

        // Private fields
        private readonly string _name;
        private readonly string _localName;
        private readonly string _localDescription;
        private readonly string _imagePath;
        private readonly ImageSource _imageSource;
        private readonly object _commandParameter;
    }
}


using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MVVM_LIB
{
    public class UICommand : RelayCommandHandlerObject, INotifyPropertyChanged
    {
        public UICommand(string name, ICommand command, string localName, string localDescription, ImageSource imageSource, Func<bool> canExecuteFunc, object commandParam)
            : this(name, command, localName, localDescription, canExecuteFunc, commandParam)
        {
            this._imageSource = imageSource;
        }
        public UICommand(string name, ICommand command, string localName, string localDescription, string imagePath, Func<bool> canExecuteFunc, object commandParam)
            : this(name, command, localName, localDescription, canExecuteFunc, commandParam)
        {
            this._imagePath = imagePath;
        }
        public UICommand(string name, ICommand command, string localName, string localDescription, ImageSource imageSource)
            : this(name, command, localName, localDescription, () => true, null)
        {
            this._imageSource = imageSource;
        }
        public UICommand(string name, ICommand command, string localName, string localDescription, string imagePath )
            : this(name, command, localName, localDescription, () => true, null)
        {
            this._imagePath = imagePath;
        }
        public UICommand(string name, ICommand command, string localName, string localDescription, ImageSource imageSource, object commandParam)
            : this(name, command, localName, localDescription, () => true, commandParam)
        {
            this._imageSource = imageSource;
        }
        public UICommand(string name, ICommand command, string localName, string localDescription, string imagePath, object commandParam)
            : this(name, command, localName, localDescription, () => true, commandParam)
        {
            this._imagePath = imagePath;
        }


        //public UICommand(string name, ICommand command, string localName, string localDescription, string imagePath)
        //    : this(name, command, localName, localDescription, () => true, null)
        //{
        //    this._imagePath = imagePath;
        //}
        //public UICommand(string name, ICommand command, string localName, string localDescription, ImageSource imageSource)
        //    : this(name, command, localName, localDescription, () => true, null)
        //{
        //    this._imageSource = imageSource;
        //}

        private UICommand( string name,ICommand command,string localName,  string description, Func<bool> canExecuteFunc, object commandParam)
          : base(new Action<object>(command.Execute), (Func<object,bool>)(o => command.CanExecute(o) && canExecuteFunc()))
        {
            this._name = name;
            this._localName = localName;
            this._localDescription = description;
            this._commandParameter = commandParam;

        }
        public UICommand(string name)
          : this()
        {
            this._name = name;
        }

        public UICommand()
               : base( (Action<object>)(o => { }), (Func<object, bool>)(o => true))
        {
            this.HandleEvent = true;
        }
        public object CommandParameter
        {
            get
            {
                return this._commandParameter;
            }
        }
        public string Name
        {
            get
            {
                return this._name;
            }
        }
        public string LocalName
        {
            get
            {
                return this._localName;
            }
        }
        public string LocalDescription
        {
            get
            {
                return this._localDescription;
            }
        }
        public virtual bool HandleEvent { get; set; }
        public virtual string FormattedShortcut
        {
            get;

        }
        public ImageSource Icon
        {
            get
            {
                if (this._imageSource != null)
                {
                    return this._imageSource;
                }
                if (this._imagePath == null)
                {
                    return null;
                }
                if (Path.IsPathRooted(this._imagePath))
                {
                    return new BitmapImage(new Uri(this._imagePath));
                }
                return null;
            }
        }
        public virtual bool ForDevelopers
        {
            get
            {
                return false;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Guid UId { get; }
        public string AutomationIdViewModel
        {
            get
            {
                return null;
            }
        }
        public string AutomationIdControl
        {
            get
            {
                return null;
            }
        }
        public string AutomationIdProperty
        {
            get
            {
                return this.Name;
            }
        }
        private object _commandParameter;
        private readonly string _name;
        private readonly string _localName;
        private readonly string _localDescription;
        private readonly string _imagePath;
        private readonly ImageSource _imageSource;
        private readonly Guid _uId;
    }
}

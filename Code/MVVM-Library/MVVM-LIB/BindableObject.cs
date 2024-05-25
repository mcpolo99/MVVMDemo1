
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace MVVM_LIB
{
    public class BindableObject: INotifyPropertyChanged , IDataErrorInfo
    {
        private Dictionary<string, object> _propertyValues = new Dictionary<string, object>();
        private List<string> _changedPropertiesQueue = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;



        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            /*
             OLD
                        if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
             
             */
        }


        #region  Queue??
        public void SetProperty<T>(string propertyName, T value)
        {
            if (!_propertyValues.ContainsKey(propertyName) || !EqualityComparer<T>.Default.Equals((T)_propertyValues[propertyName], value))
            {
                _propertyValues[propertyName] = value;
                _changedPropertiesQueue.Add(propertyName);
            }
        }
        public void FlushChanges()
        {
            foreach (var propertyName in _changedPropertiesQueue)
            {
                OnPropertyChanged(propertyName);
            }
            _changedPropertiesQueue.Clear();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void QueuePropertyChanged<T>(string propertyName , T value)
        {
            // Using SetProperty to queue property changes
            SetProperty(propertyName, value);
        }

        #endregion


        #region Validation
        /// <summary>
        /// Validates a property whose name matches the specified <see cref="propertyName"/>.
        /// </summary>
        /// <param name="propertyName">The name of the property to validate.</param>
        /// <returns>Returns a validation error, if any, otherwise returns null.</returns>
        protected virtual string OnValidate(string propertyName)
        {
            var context = new ValidationContext(this)
            {
                MemberName = propertyName
            };

            var results = new Collection<ValidationResult>();
            bool isValid = Validator.TryValidateObject(this, context, results, true);

            return !isValid ? results[0].ErrorMessage : null;
        }
        /// <summary>
        /// Gets the validation error for a property whose name matches the specified <see cref="columnName"/>.
        /// </summary>
        /// <param name="columnName">The name of the property to validate.</param>
        /// <returns>Returns a validation error if there is one, otherwise returns null.</returns>
        public string this[string columnName]
        {
            get { return OnValidate(columnName); }
        }
        public string Error
        {
            // If you don't need to provide a general validation error for the entire object,
            // you can simply return null here.
            get { return null; }
        }
        #endregion


    }
}

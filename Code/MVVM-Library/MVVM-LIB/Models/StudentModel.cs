using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_LIB.Models
{

    public class StudentModel : BindableObject
    {
        private string _firstName;
        private string _lastName;
  
        public string FirstName
        {
            get
            {
                if (string.IsNullOrEmpty(_firstName))
                    return "no first name";
                return _firstName;
            }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    //RaisePropertyChanged(nameof(FirstName)); 
                    //RaisePropertyChanged(nameof(FullName));
                    base.RaisePropertyChanged(nameof(FirstName));
                    base.RaisePropertyChanged(nameof(FullName));
                }
            }
        }
        public string LastName
        {
            get
            {
                if (string.IsNullOrEmpty(_lastName))
                    return "no last name";
                return _lastName;
            }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    base.RaisePropertyChanged(nameof(LastName));
                    base.RaisePropertyChanged(nameof(FullName));
                }
            }
        }
        public string FullName => $"{FirstName} {LastName}";
    }
}

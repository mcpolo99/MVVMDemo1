using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVVM_LIB.Models
{
    public class PersonViewModel:BindableObject
    {
        private string _firstName;
        private string _lastName;
        private string _fullName;

     
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
                    RaisePropertyChanged(nameof(FirstName)); /*RaisePropertyChanged(FullName);*/
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
                    RaisePropertyChanged(nameof(LastName)); /* RaisePropertyChanged(FullName);*/

                }

            }
        }
        
        
        [Required]
        [StringLength(32, MinimumLength = 2)]
        //public string FullName => $"{FirstName} {LastName}";
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
            }
        }

        protected override string OnValidate(string propertyName)
        {
            if (FullName != null && !FullName.Contains(" "))
                return "Customer name must contain both a first and last name.";

            return base.OnValidate(propertyName);
        }

    }
}

using MVVM_LIB.Models;
using MVVM_LIB.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace MVVM_LIB.Views
{
    public partial class StudentMainViewModel : BindableObject
    {
        public StudentMainViewModel()
        {
            CommandAddStudent = new UICommand1("CommandAddStudent", new RelayCommandHandlerObject(ExecuteCommandAddStudent), "", "");
            CommandDeleteStudent = new UICommand1("CommandDeleteStudent", new RelayCommandHandlerObject(ExecuteCommandDeleteStudent), "", "");
            this.LoadStudent();
        }

       
        private string _newStudentFirstName;
        private string _newStudentLastName;
        [Required]
        [StringLength(32, MinimumLength = 2)]
        public string NewStudentFirstName
        {
            get { return _newStudentFirstName; }
            set
            {
                _newStudentFirstName = value;
                RaisePropertyChanged(nameof(NewStudentFirstName));
            }
        }
        [Required]
        [StringLength(32, MinimumLength = 2)]
        public string NewStudentLastName
        {
            get { return _newStudentLastName; }
            set
            {
                _newStudentLastName = value;
                RaisePropertyChanged(nameof(NewStudentLastName));
            }
        }

        protected override string OnValidate(string propertyName)
        {
            if (propertyName == nameof(NewStudentFirstName) && propertyName==null )
            {
                return "MUST HAVE NAMES";
            }
            if (propertyName == nameof(NewStudentLastName) && propertyName==null )
            {
                return "MUST HAVE NAMES";
            }

   

            return base.OnValidate(propertyName);
        }

        private StudentModel _selectedStudent; public StudentModel SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                RaisePropertyChanged(nameof(SelectedStudent));
                //RaisePropertyChanged("FullName");
            }
        }
        private StudentView _studentViewObject;public StudentView StudentViewObject
        {
            get { return _studentViewObject; }
            set { _studentViewObject = value; }
        }


        public ObservableCollection<StudentModel> Students { get; set; }
        public void LoadStudent()
        {
            ObservableCollection<StudentModel> students = new ObservableCollection<StudentModel>
            {
                new StudentModel { FirstName = "ALLAN0", LastName = "HELLO0" },
                new StudentModel { FirstName = "ALLAN1", LastName = "HELLO1" },
                new StudentModel { FirstName = "ALLAN2", LastName = "HELLO2" },
                new StudentModel { FirstName = "ALLAN3", LastName = "HELLO3" },
                new StudentModel { FirstName = "ALLAN4", LastName = "HELLO4" },
                new StudentModel { FirstName = "ALLAN5", LastName = "HELLO5" },
                new StudentModel { FirstName = "ALLAN6", LastName = "HELLO6" }
            };
            //base.FlushChanges();

            Students = students;
            AddStudent("FirstAdded", "LastAdded");
        }
        public void AddStudent(string firstname , string lastname)
        {
            Students.Add(new StudentModel { FirstName=firstname, LastName=lastname });  
        }





        //--------------------------
        public UICommand1 CommandAddStudent { get; private set; }
        public UICommand1 CommandDeleteStudent { get; private set; }


        //---------------------ADD STUDENT COMMAND------------------------------------
        private ICommand _addStudentCommand; public ICommand AddStudentCommand
        {
            get
            {
                if (this._addStudentCommand == null)
                {
                    return this._addStudentCommand ?? (_addStudentCommand = new RelayCommandHandler(() => ExecuteCommandAddStudent()));
                }
                return _addStudentCommand;
            }
        }
        private void ExecuteCommandAddStudent()
        {
            if (!string.IsNullOrWhiteSpace(NewStudentFirstName) && !string.IsNullOrWhiteSpace(NewStudentLastName))
            {
                Students.Add(new StudentModel { FirstName = NewStudentFirstName, LastName = NewStudentLastName });
                NewStudentFirstName = string.Empty;
                NewStudentLastName = string.Empty;
            }
        }

        //---------------------DELETE STUDENT COMMAND------------------------------------
        private ICommand _delStudentCommand;public ICommand DelStudentCommand
        {
            get
            {
                if (this._delStudentCommand == null)
                {
                    return this._delStudentCommand ?? (_delStudentCommand = new RelayCommandHandler(() => ExecuteCommandDeleteStudent()));
                }
                return _delStudentCommand;
            }
        }
 
        private void ExecuteCommandDeleteStudent()
        {
            if (SelectedStudent != null)
            {
                Students.Remove(SelectedStudent);
                SelectedStudent = null;
            }

        }
    }
}

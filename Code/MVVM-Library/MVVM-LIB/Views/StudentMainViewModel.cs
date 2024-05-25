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



        //---------------------DELETE STUDENT COMMAND------------------------------------
        private ICommand _addStudentCommand; public ICommand AddStudentCommand
        {
            get
            {
                if (this._addStudentCommand == null)
                {
                    return this._addStudentCommand ?? (_addStudentCommand = new RelayCommandHandler(() => ExecuteAddStudent()));
                }
                return _addStudentCommand;
            }
        }
        private void ExecuteAddStudent()
        {
            /*
            How would i pass the values from the 2 textboxes inside of StudentView.Xaml
                  < TextBox x: Name = "TBFirstName" Grid.Row = "1" Grid.Column = "0" />
                  < TextBox x: Name = "TBLastName" Grid.Row = "1" Grid.Column = "1" />
            to the auctual command that need to be executed like this:

                Students.Add(new StudentModel { FirstName = firstname, LastName = lastname });
            */
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
                    return this._delStudentCommand ?? (_delStudentCommand = new RelayCommandHandler(() => ExecuteDelSelectedStudent()));
                }
                return _delStudentCommand;
            }
        }
 
        private void ExecuteDelSelectedStudent()
        {
            /*
              And for the deleteSelectedStudent? how would I even make this work, The selected item should probably come from ListBoxStudents SelectedItem??

             But since we needed to implement this code inbehind the Xaml file:
                    public static readonly DependencyProperty SelectedStudentProperty =DependencyProperty.Register("SelectedStudent", typeof(StudentModel), typeof(StudentView));
        
         
        public StudentModel SelectedStudent
        {
            get { return (StudentModel)GetValue(SelectedStudentProperty); }
            set { SetValue(SelectedStudentProperty, value); }
        }

            Can i make use of it in some way or do i need to make separate property for the delete command?

             */
            if (SelectedStudent != null)
            {
                Students.Remove(SelectedStudent);
                SelectedStudent = null;
            }

        }
    }
}

using MVVM_LIB;
using MVVM_LIB.Models;
using MVVM_LIB.ViewModels;
using MVVM_LIB.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace MVVMDemo1
{
    public class MainViewModel : BindableObject
    {

        //Reason for SelectedStudent is inside of here is simply becuse StudentModel and Student SHALL not have to do anything with the UI to do. And since selection is a 
        //Ui related thing we need to put in MainViewModel
        private StudentModel _selectedStudent; public StudentModel SelectedStudent
        {
            //used for Changing the UI of what Item is selected in a ScrollView
            get { return _selectedStudent; }
            set { _selectedStudent = value; RaisePropertyChanged(nameof(SelectedStudent)); }
        }



        /// <summary>
        /// With StudentViewObject We only initiate a new UserControl with predefined data/controlls 
        /// Since StudentView already has context to StudentViewModel we do not need to initiate it. 
        /// </summary>
        private StudentView _studentViewObject; public StudentView StudentViewObject
        {
            //used to get the studentView UserControl
            get { return _studentViewObject; }
            set {
                _studentViewObject = value;
                RaisePropertyChanged(nameof(StudentViewObject));
            }
        }




        /// <summary>
        /// whit StudentViewModelObject, we have the possibility to define our own controlls based on the information inside the ViewModel object
        /// Also we have much more access to what we can do with the object
        /// </summary>
        private StudentViewModel _studentViewModelObject; public StudentViewModel StudentViewModelObject {
            //used to create and load StudentViewModel
            get {return _studentViewModelObject; } 
            set { _studentViewModelObject = value; } 
        }
        private StudentMainViewModel context;


        public PersonViewModel Person { get; set; }
        public UICommand1 ConsoleLogCommand { get; private set; }
        public UICommand1 CommandDeleteStudent{ get; private set; }
        private void ExecuteCommandDeleteStudent(StudentModel studentModel)
        {
            StudentViewModelObject.DeleteStudent(studentModel);
        }
        private void ExecuteCommandAddStudent(string name)
        {
            StudentViewModelObject.AddStudent(name);
            Person.FullName = string.Empty;
            base.RaisePropertyChanged(nameof(Person));
        }
        public UICommand1 CommandAddStudent { get; private set; }


        public MainViewModel()
        {
            //user controll, predifined UI
            StudentViewObject = new StudentView
            {
                ////If we want to reset the context of the StudentView
                //DataContext = new StudentMainViewModel()
            };
            ConsoleLogCommand = new UICommand1("ConsoleLog", new RelayCommandHandlerObject(ExecuteConsoleLog2), "Console Log", "Logs to the console");
            CommandDeleteStudent = new UICommand1("CommandDeleteStudent", new RelayCommandHandlerObject((parameter) => ExecuteCommandDeleteStudent((StudentModel)parameter)), "delete", "delete");
            CommandAddStudent = new UICommand1("CommandAddStudent", new RelayCommandHandlerObject((parameter) => ExecuteCommandAddStudent((string)parameter)), "Add", "Add");

            //IF we want to access the context of the StudentViewObject we simply assing a local context 
            context = (StudentViewObject.DataContext as StudentMainViewModel);
            context.AddStudent("TEST1","TESTo1");




            //Here We simply initiating a StudentViewModel to StudentViewModelObject No context needed
            StudentViewModelObject = new StudentViewModel();
            //StudentViewModelObject.Students = new ObservableCollection<StudentModel>();
            
            //StudentViewModelObject.init();
            //StudentViewModelObject.Students.Add(new StudentModel { FirstName = "ALLAN0", LastName = "HELLO0" } );
            StudentViewModelObject.AddStudent("Firstname", "lastnaem");
            StudentViewModelObject.AddStudent("Firstname1", "lastnaem1");
            StudentViewModelObject.AddStudent("Firstname2", "lastnaem2");
            StudentViewModelObject.AddStudent("FULLNAME TEST!!!");
            //Students = new Student();
            Person = new PersonViewModel();
        }




        /// <summary>
        /// Get name
        /// </summary>
        private ICommand _getName; public ICommand GetName {  
            get {
                if(this._getName == null)
                {
                    //return this._getName ?? (_getName = new RelayCommandHandler(() => ExecuteGetName()));
                    return this._getName ?? (_getName = new RelayCommandHandlerObject(() => ExecuteGetName()));
                }
                return _getName; 
            } 
        }
        private void ExecuteGetName()
        {
            SelectedStudentFirstName = StudentViewObject.SelectedStudent?.FirstName;
        }

        /// <summary>
        /// Console log
        /// </summary>
        private ICommand _consoleLog; public ICommand ConsoleLog
        {
            get
            {
                if (this._consoleLog == null)
                {
                    return this._consoleLog ?? (_consoleLog = new RelayCommandHandlerObject((parameter) => ExecuteConsoleLog(parameter)));
                }
                return _consoleLog;
            }
        }
        private void ExecuteConsoleLog(object parameter)
        {
            if (parameter != null)
            {
                //string fullName = parameter.ToString();
                Console.WriteLine($"ConsoleLogButton: {(string)parameter.ToString() }");//is taken from Column3
                //Console.WriteLine(context.SelectedStudent.FullName.ToString());//is taken from Column2
            }
            else
            {
                Console.WriteLine("Nothing to print! :D:D:D");
                //Console.WriteLine(context.SelectedStudent.FullName.ToString()); //is taken from Column2
            }

        }
        private void ExecuteConsoleLog2(object parameter)
        {
            Console.WriteLine("SUCCESS");
            if (parameter != null)
            {
                //string fullName = parameter.ToString();
                Console.WriteLine($"ConsoleLogButton: {(string)parameter.ToString()}");//is taken from Column3
                //Console.WriteLine(context.SelectedStudent.FullName.ToString());//is taken from Column2
            }
            else
            {
                Console.WriteLine("Nothing to print! :D:D:D");
                //Console.WriteLine(context.SelectedStudent.FullName.ToString()); //is taken from Column2
            }

        }

        private string _selectedStudentFirstName; public string SelectedStudentFirstName
        {
            get {
                if (string.IsNullOrEmpty(_selectedStudentFirstName))
                    return "Empty text for visual";
                return _selectedStudentFirstName; 
            }
            set
            {
                if (_selectedStudentFirstName != value)
                {
                    _selectedStudentFirstName = value;
                    RaisePropertyChanged(nameof(SelectedStudentFirstName));
                }
            }
        }








    }
}

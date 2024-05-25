using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_LIB.Models;

namespace MVVM_LIB.ViewModels
{
    public class StudentViewModel : BindableObject
    {


        public ObservableCollection<StudentModel> Students { get; set; }

        public void init()
        {
            //simply makes it possible for us to use  StudentViewModelObject.Students.Add(new StudentModel { FirstName = "ALLAN0", LastName = "HELLO0" } );
            ObservableCollection<StudentModel> students = new ObservableCollection<StudentModel>();
            Students = students;

        }


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

            Students = students;
        }

        public void AddStudent(string firstname, string lastname)
        {
            if(Students == null)
            {
                Students = new ObservableCollection<StudentModel>();
            }
            Students?.Add(new StudentModel { FirstName = firstname, LastName = lastname });
        }
        public void AddStudent(string fullName)
        {
            if (Students == null)
            {
                Students = new ObservableCollection<StudentModel>();
            }
            if (fullName == "")
                return;
            // Split the fullName based on spaces
            var nameParts = fullName.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

            string firstName;
            string lastName;

            if (nameParts.Length == 1)
            {
                // If there is no space, use the fullName as firstName and lastName as empty
                firstName = nameParts[0];
                lastName = "";
            }
            else
            {
                // If there is a space, use the first part as firstName and the second part as lastName
                firstName = nameParts[0];
                lastName = nameParts[1];
            }
            Students?.Add(new StudentModel { FirstName = firstName, LastName = lastName });
        }


        public void DeleteStudent(StudentModel student)
        {
            if (student != null)
            {
                Students?.Remove(student);
            }
        }









    }
}

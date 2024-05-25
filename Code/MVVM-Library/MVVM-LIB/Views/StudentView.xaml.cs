using MVVM_LIB.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVM_LIB.Views
{
    /// <summary>
    /// Interaction logic for StudentView.xaml
    /// </summary>
    public partial class StudentView : UserControl
    {
        public StudentView()
        {
            InitializeComponent();
        }
        public ObservableCollection<StudentModel> Students
        {
            get { return (DataContext as StudentMainViewModel)?.Students; }
        }


        
        public static readonly DependencyProperty SelectedStudentProperty =
            DependencyProperty.Register(
                "SelectedStudent", 
                typeof(StudentModel),
                typeof(StudentView)
                ,new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedStudentChanged)

                );

        //instead of using SelectedStudent inside of MainViewModel or to access inside of StudentMainViewModel :D 
        public StudentModel SelectedStudent
        {
            get { return (StudentModel)GetValue(SelectedStudentProperty); }
            set { SetValue(SelectedStudentProperty, value); }
        }

        //This used for possibility to delete selected item when we init StudentView As a userControll.
        private static void OnSelectedStudentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (StudentView)d;
            var viewModel = control.DataContext as StudentMainViewModel;
            if (viewModel != null)
            {
                viewModel.SelectedStudent = e.NewValue as StudentModel;
            }
        }
    }
}

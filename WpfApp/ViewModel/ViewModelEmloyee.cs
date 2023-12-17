using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp.ViewModel;

namespace WpfApp
{
    public class ViewModelEmloyee : INotifyPropertyChanged
    {
        public ObservableCollection<Employee> Employees { get; set; }
        public ViewModelEmloyee()
        {
            Employees = new ObservableCollection<Employee>()
            {
                new Employee() {Name="Ivan Kurochkin", Age=33, Faculty="Applied Informatics"},
                new Employee() {Name="Trent Alexander", Age=49, Faculty="Informational Systems"},
                new Employee() {Name="Oleg Nikitin", Age=61, Faculty="Programming"},
                new Employee() {Name="Cristiano Ronaldo", Age=25, Faculty="Applied Physics"}
            };
        }


        #region SelectedItem and NewEmployee propeties 

        /// <summary>
        /// Selected item property which contains clicked item
        /// </summary>
        private Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
            }
        }

        /// <summary>
        /// New emlpoyee propety which contains fields of Employee Model
        /// </summary>
        private Employee newEmployee = new Employee();
        public Employee NewEmployee
        {
            get { return newEmployee; }
            set
            {
                newEmployee = value;
                OnPropertyChanged("NewEmployee");
            }
        }

        #endregion


        #region Add and Remove commands

        private RelayCommand addEmployeeComand;
        public RelayCommand AddEmployeeComand
        {
            get
            {
                return addEmployeeComand ?? (addEmployeeComand = new RelayCommand(obj =>
                {
                    Employee employee = new Employee() { Name = NewEmployee?.Name, Age = (int)(NewEmployee?.Age), Faculty = NewEmployee?.Faculty };
                    Employees.Insert(0, employee);
                    selectedEmployee = employee;
                }));
            }
        }

        private RelayCommand removeEmployeeCommand;
        public RelayCommand RemoveEmployeeCommand
        {
            get
            {
                return removeEmployeeCommand ?? (removeEmployeeCommand = new RelayCommand(obj =>
                {
                    Employee employee = (Employee)obj;
                    if (employee != null)
                    {
                        Employees.Remove(employee);
                    }
                },
                obj => Employees.Count > 0));
            }
        }

        #endregion


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #endregion


    }
}

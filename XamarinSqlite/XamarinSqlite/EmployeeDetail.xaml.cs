using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinSqlite
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeDetail : ContentPage
    {
        public EmployeeDetail()
        {
            InitializeComponent();
        }


        EmployeeModel _employee;

        public EmployeeDetail(EmployeeModel employee)
        {
            InitializeComponent();
            Title = "Edit Infor";
            _employee = employee;
            nameEntry.Text = employee.Name;
            addressEntry.Text = employee.Address;
            nameEntry.Focus();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameEntry.Text) || string.IsNullOrWhiteSpace(addressEntry.Text))
            {
                await DisplayAlert("Invalid", "Blank or WhiteSpace value is Invalid!", "OK");
            }
            else if(_employee!=null)
            {
                UpdateEmployee();
            }
            else
                AddNewEmployee(); 
        }



        async void UpdateEmployee()
        {
            _employee.Name = nameEntry.Text;
            _employee.Address = addressEntry.Text;
            await App.MyDatabase.UpdateEmployee(_employee);
            await Navigation.PopAsync();
        }



        async void AddNewEmployee()
        {
            await App.MyDatabase.CreateEmployee(new EmployeeModel
            {
                Name= nameEntry.Text,
                Address = addressEntry.Text,
            });

            await Navigation.PopAsync();
        }
    }
}
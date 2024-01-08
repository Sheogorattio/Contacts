using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.IO;

namespace PersonRegister
{
    public class ContactsViewModel : INotifyPropertyChanged
    {

        private string? _selectedContact;
        public string? SelectedContact
        {
            get
            {
                return _selectedContact;
            }
            set
            {
                _selectedContact = value;
                RaisePropertyChanged(nameof(SelectedContact));
            }
        }

        private ObservableCollection<Contact> _contacts = new ObservableCollection<Contact>();

        public ObservableCollection<Contact> Contacts
        {
            get
            {
                return _contacts;
            }
            set
            {
                _contacts = value;
                RaisePropertyChanged(nameof(Contacts));
            }
        }

        private string? _currentName;
        public string? CurrentName
        {
            get
            {
                return _currentName;
            }
            set
            {
                _currentName = value;
                RaisePropertyChanged(nameof(CurrentName));
            }
        }

        private string? _currentAddress;
        public string? CurrentAddress
        {
            get
            {
                return _currentAddress;
            }
            set
            {
                _currentAddress = value;
                RaisePropertyChanged(nameof(CurrentAddress));
            }
        }

        private string? _currentPhone;
        public string? CurrentPhone
        {
            get
            {
                return _currentPhone;
            }
            set
            {
                _currentPhone = value;
                RaisePropertyChanged(nameof(CurrentPhone));
            }
        }

        public ContactsViewModel()
        {
            CurrentAddress = "";
            CurrentName = "";
            CurrentPhone = "";
            SelectedContact = "";
        }

        private DelegateCommand? _AddPersonCommand;
        private DelegateCommand? _RemovePersonCommand;
        private DelegateCommand? _SaveCommand;
        private DelegateCommand? _LoadCommand;


        private void Add(object? parameter)
        {
            Contacts.Add(new Contact(CurrentName, CurrentAddress, CurrentPhone));
            CurrentName = "";
            CurrentPhone = "";
            CurrentAddress = "";
        }

        private bool CanAdd(object? parameter)
        {
            if (CurrentName == "" ||
                CurrentPhone == "" ||
                CurrentAddress == "") return false;
            return true;
        }
        public ICommand AddPerson
        {
            get
            {
                if (_AddPersonCommand == null)
                {
                    _AddPersonCommand = new DelegateCommand(Add, CanAdd);
                }
                return _AddPersonCommand;
            }
        }

        private void Delete(object? parameter)
        {
            if (SelectedContact != "" && SelectedContact != null)
            {
                string[] info = SelectedContact.Split(" | ");
                Contact target = new Contact(info[0], info[0], info[0]);
                for (int i = 0; i < Contacts.Count; i++)
                {
                    if (Contacts[i].FullName == target.FullName)
                    {
                        target = Contacts[i];
                        break;
                    }
                }
                if (target != null)
                {
                    Contacts.Remove(target);
                }
            }
        }
        private bool CanDelete(object? paramater)
        {
            if (SelectedContact == "") return false;
            return true;
        }
        public ICommand DeletePerson
        {
            get
            {
                if(_RemovePersonCommand == null)
                {
                    _RemovePersonCommand = new DelegateCommand(Delete, CanDelete);
                }
                return _RemovePersonCommand;
            }
        }

        private void Save(object? parameter)
        {
            string json = JsonSerializer.Serialize(Contacts);
            File.WriteAllText("./Contacts", json);
        }

        private bool CanSave(object? parameter)
        {
            if (Contacts.Count > 0)
            {
                return true;
            }
            return false;
        }

        public ICommand SaveContacts
        {
            get
            {
                if(_SaveCommand == null)
                {
                    _SaveCommand = new DelegateCommand(Save, CanSave);
                }
                return _SaveCommand;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

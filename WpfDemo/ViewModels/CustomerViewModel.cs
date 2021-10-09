using System;
using System.ComponentModel;
using Prism.Events;
using WpfDemo.Bootstrappers;
using WpfDemo.Commands;
using WpfDemo.PubSubEvents;

namespace WpfDemo.ViewModels
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private DateTime? dateOfBirth;
        private string panNo;
        private string aadharNo;
        private bool isPrime;

        public event PropertyChangedEventHandler PropertyChanged;

        public CustomerViewModel()
        {
            var eventAggregator = Bootstrapper.Resolve<IEventAggregator>();

            ViewCustomer = new RelayCommand((c) =>
            {
                eventAggregator.GetEvent<CustomerSelectPubSubEvent>().Publish(c as CustomerViewModel);
            }, (c) => c != null);

            UpdateCustomer = new RelayCommand((c) =>
            {
                eventAggregator.GetEvent<CustomerUpdateSelectPubSubEvent>().Publish((c as CustomerViewModel).Copy());
            }, (c) => c != null);

            DeleteCustomer = new RelayCommand((c) =>
            {
                eventAggregator.GetEvent<CustomerDeletePubSubEvent>().Publish(c as CustomerViewModel);
            }, (c) => c != null);
        }

        public int Id { get; set; }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (value != firstName)
                {
                    firstName = value;
                    NotifyPropertyChanged(nameof(FirstName));
                }
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (value != lastName)
                {
                    lastName = value;
                    NotifyPropertyChanged(nameof(LastName));
                }
            }
        }

        public DateTime? DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
            set
            {
                if (value != dateOfBirth)
                {
                    dateOfBirth = value;
                    NotifyPropertyChanged(nameof(DateOfBirth));
                }
            }
        }

        public string PanNo
        {
            get
            {
                return panNo;
            }
            set
            {
                if (value != panNo)
                {
                    panNo = value;
                    NotifyPropertyChanged(nameof(PanNo));
                }
            }
        }

        public string AadharNo
        {
            get
            {
                return aadharNo;
            }
            set
            {
                if (value != aadharNo)
                {
                    aadharNo = value;
                    NotifyPropertyChanged(nameof(AadharNo));
                }
            }
        }

        public bool IsPrime
        {
            get
            {
                return isPrime;
            }
            set
            {
                if (value != isPrime)
                {
                    isPrime = value;
                    NotifyPropertyChanged(nameof(IsPrime));
                }
            }
        }

        public void Reset()
        {
            Id = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
            DateOfBirth = null;
            AadharNo = string.Empty;
            PanNo = string.Empty;
            IsPrime = false;
        }

        public CustomerViewModel Copy()
        {
            return new CustomerViewModel
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth,
                PanNo = PanNo,
                AadharNo = AadharNo,
                IsPrime = IsPrime
            };
        }

        public RelayCommand ViewCustomer { get; set; }

        public RelayCommand UpdateCustomer { get; set; }

        public RelayCommand DeleteCustomer { get; set; }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

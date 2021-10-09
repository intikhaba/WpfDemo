namespace WpfDemo.Converters
{
    internal static class CustomerConverter
    {
        internal static ViewModels.CustomerViewModel ToViewModelCustomer(this BusinessModels.Customer businessCustomer)
        {
            return new ViewModels.CustomerViewModel
            {
                Id = businessCustomer.Id,
                FirstName = businessCustomer.FirstName,
                LastName = businessCustomer.LastName,
                DateOfBirth = businessCustomer.DateOfBirth,
                PanNo = businessCustomer.PanNo,
                AadharNo = businessCustomer.AadharNo
            };
        }

        internal static BusinessModels.Customer ToBusinessCustomer(this WpfDemo.ViewModels.CustomerViewModel viewModelCustomer)
        {
            return new BusinessModels.Customer
            {
                Id = viewModelCustomer.Id,
                FirstName = viewModelCustomer.FirstName,
                LastName = viewModelCustomer.LastName,
                DateOfBirth = viewModelCustomer.DateOfBirth.Value,
                PanNo = viewModelCustomer.PanNo,
                AadharNo = viewModelCustomer.AadharNo
            };
        }
    }
}

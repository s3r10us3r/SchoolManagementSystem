using Microsoft.Extensions.DependencyInjection;
using SchoolManagementSystem.AdminPanel.Services.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace SchoolManagementSystem.AdminPanel.Services
{
    internal class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private Frame? _frame;
        private Window? window;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void SetFrame(Frame frame)
        {
            _frame = frame;
            MessageBox.Show("Frame set");
        }

        public async void NavigateTo<TPage>() where TPage : Page
        {
            var page = _serviceProvider.GetRequiredService<TPage>();
            NavigateTo(page);
        }

        public void ChangeWindow<TWindow>() where TWindow : Window
        {
            var newWindow = _serviceProvider.GetRequiredService<TWindow>();
            newWindow.Show();
            window?.Close();
            window = newWindow;
        }

        public async void NavigateTo(Page page)
        {
            if (_frame == null)
                throw new InvalidOperationException("Frame is not set.");
            await Application.Current.Dispatcher.InvokeAsync(() => _frame.Navigate(page));
        }

        public void OpenWindow(Window window)
        {
            Application.Current.Dispatcher.Invoke(() => window.Show());
        }
    }
}

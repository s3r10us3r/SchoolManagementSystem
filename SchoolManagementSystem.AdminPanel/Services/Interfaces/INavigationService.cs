using System.Windows;
using System.Windows.Controls;

namespace SchoolManagementSystem.AdminPanel.Services.Interfaces
{
    public interface INavigationService
    {
        public void NavigateTo<TPage>() where TPage : Page;
        public void NavigateTo(Page page);
        public void ChangeWindow<TWindow>() where TWindow : Window;
        public void SetFrame(Frame frame); 
        public void OpenWindow(Window window);
    }
}

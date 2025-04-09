using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TabDemo.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<TabItemViewModel> _tabs;

    [ObservableProperty]
    private TabItemViewModel? _selectedTab;

    public MainWindowViewModel()
    {
        _tabs = new ObservableCollection<TabItemViewModel>();
        AddNewTab();
    }

    [RelayCommand]
    private void AddNewTab()
    {
        var newTab = new TabItemViewModel(this, $"Вкладка {Tabs.Count + 1}");
        Tabs.Add(newTab);
        SelectedTab = newTab;
    }

    [RelayCommand]
    private void CloseTab(TabItemViewModel tab)
    {
        if (Tabs.Count > 1)
        {
            Tabs.Remove(tab);
            if (SelectedTab == tab)
            {
                SelectedTab = Tabs.Count > 0 ? Tabs[^1] : null;
            }
        }
    }
}

public partial class TabItemViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;

    [ObservableProperty]
    private string _title;

    public TabItemViewModel(MainWindowViewModel mainWindowViewModel, string title)
    {
        _mainWindowViewModel = mainWindowViewModel;
        Title = title;
    }

    [RelayCommand]
    public void Close()
    {
        _mainWindowViewModel.CloseTabCommand.Execute(this);
    }
}
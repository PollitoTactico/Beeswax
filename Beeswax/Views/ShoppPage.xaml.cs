using Beeswax.ViewModel;

namespace Beeswax.Views;

public partial class ShoppPage : ContentPage
{
    private ShoppPageViewModel _viewModel;

    public ShoppPage()
    {
        InitializeComponent();
        _viewModel = new ShoppPageViewModel();
        BindingContext = _viewModel;
    }
}
using CommunityToolkit.Maui.Views;

namespace Rezeptify.Pages;

public partial class AddFoodPopUpPage : Popup
{
	public AddFoodPopUpPage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		this.Close();
    }
}
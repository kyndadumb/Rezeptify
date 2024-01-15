using Rezeptify.AppComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezeptify.VM
{
    public class AddFoodVM : ViewModelBase
    {
        private ViewModelBase _backVM;
        public AddFoodVM(ViewModelBase vm)
        {
            _backVM = vm;
            CMD_Accept = new TaskCommand(AcceptChanges);
        }

        public AddFoodVM(ViewModelBase vm, string kat, string eancode)
        {
            _Kategorie = kat;
            _backVM = vm;
            CMD_Accept = new TaskCommand(AcceptChanges);
        }

        private async Task AcceptChanges()
        {
            if (String.IsNullOrWhiteSpace(Kategorie) || !Menge.HasValue)
            {
                await _viewManager.MessageBoxAsyncYesNo("Fehler", "Kategorie und Menge sind Pflichtfelder und müssen gefüllt werden!");
                return;
            }

            using (var conn = DatabaseHandler.OpenDatabaseConnection())
            {
                DatabaseHandler.AddIngredients(Kategorie, Menge.Value, Unit, EANCode);

                _viewManager.Show(_backVM);
            }

        }

        private string _Kategorie;
        public string Kategorie
        {
            get { return _Kategorie; }
            set { _Kategorie = value; NotifyPropertyChanged(); }
        }

        private string _Unit;
        public string Unit
        {
            get { return _Unit; }
            set { _Unit = value; NotifyPropertyChanged(); }
        }

        private string _EANCode;
        public string EANCode
        {
            get { return _EANCode; }
            set { _EANCode = value; NotifyPropertyChanged(); }
        }

        private float? _Menge;
        public float? Menge
        {
            get { return _Menge; }
            set { _Menge = value; NotifyPropertyChanged(); }
        }

        public TaskCommand CMD_Accept { get; set; }


    }
}

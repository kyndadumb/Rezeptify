﻿using Rezeptify.AppComponents;

namespace Rezeptify.VM
{
    public class AddFoodVM : ViewModelBase
    {
        private ViewModelBase _backVM;
        public AddFoodVM(ViewModelBase vm)
        {
            _backVM = vm;
            CMD_Accept = new TaskCommand(AcceptChanges);
            this.CMD_ShowStart = new ActionCommand(showStartPage);
        }

        public AddFoodVM(ViewModelBase vm, string kat, string eancode)
        {
            _Kategorie = kat;
            _EANCode = eancode;
            _backVM = vm;
            CMD_Accept = new TaskCommand(AcceptChanges);
            this.CMD_ShowStart = new ActionCommand(showStartPage);
        }

        private async Task AcceptChanges()
        {
            if (String.IsNullOrWhiteSpace(Kategorie) || !Menge.HasValue)
            {
                await _viewManager.MessageBoxAsync("Fehler", "Kategorie und Menge sind Pflichtfelder und müssen gefüllt werden!");
                return;
            }

            using (var conn = DatabaseHandler.OpenDatabaseConnection())
            {
                await DatabaseHandler.AddIngredients(Kategorie, Menge.Value, Unit, EANCode,conn);

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

        private void showStartPage()
        {
            var vm = new StartVM();
            _viewManager.Show(vm);
        }

        public ActionCommand CMD_ShowStart {  get; set; }
    }
}

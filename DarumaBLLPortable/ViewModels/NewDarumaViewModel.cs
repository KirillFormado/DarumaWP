using System.Collections.Generic;
using System.Threading.Tasks;
using DarumaBLLPortable.ApplicationServices.Abstractions;
using DarumaBLLPortable.ApplicationServices.Entites;
using DarumaBLLPortable.Commands;
using DarumaBLLPortable.Domain;

namespace DarumaBLLPortable.ViewModels
{
    public class NewDarumaViewModel : ViewModelBase
    {
        private IDarumaApplicationService _darumaService;
        private DarumaWishTheme _selectedTheme;
        private string _wish;
        public IEnumerable<DarumaWishTheme> ThemeList { get; set; }

        public DarumaView Daruma { get; set;}

        public DarumaWishTheme SelectedTheme
        {
            get
            {
                return _selectedTheme;
            }
            set
            {
                _selectedTheme = value;
                OnPropertyChanged();
            }
        }

        public string Wish
        {
            get
            {
                return _wish;
            }
            set
            {
                _wish = value;
                OnPropertyChanged();
            }
        }

       // public RelayCommand CreateDarumaCommand { get; private set; }

        public async Task<DarumaView> CreateDaruma(string wish, DarumaWishTheme theme)
        {
            return await _darumaService.CreateDaruma(wish, theme);
        }

        public NewDarumaViewModel(IDarumaApplicationService darumaService)
        {
            _darumaService = darumaService;

            //InitCommands();

            ThemeList = new List<DarumaWishTheme>
            {
                DarumaWishTheme.Friendship,
                DarumaWishTheme.Funny,
                DarumaWishTheme.Health,
                DarumaWishTheme.Love,
                DarumaWishTheme.Luck,
                DarumaWishTheme.Rich
            };
        }

        //private void InitCommands()
        //{
        //    CreateDarumaCommand = new RelayCommand(CreateDaruma);
        //}
    }
}
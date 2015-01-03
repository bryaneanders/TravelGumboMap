/* This class contains methods that control the Map Window
 * from setting map images, to manipulating polygon grids
 * and creating a side menu. It is the core of the program. 
 */

using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;


namespace TravelGumboMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TGMapWindow : Window
    {
        // various constant's
        private const double MENU_ITEM_WIDTH = 240;
        private const double MENU_ITEM_HEIGHT = 50;
        private const double FONT_SIZE = 24;
        private const int STATE_PROV_FIELDS = 5;

        private List<string[]> states, countries;

        private StreamWriter logFile;

        private DatabaseManager dbManager;

        // bools that indicate what level the app is on
        private bool stateInfoLevel, stateMenuLevel, regionalMenuLevel;

        public TGMapWindow()
        {
            InitializeComponent();

            // open the log file
            if (!File.Exists("tglog.txt"))
            {
                logFile = new StreamWriter("tglog.txt");
            }
            else
            {
                logFile = File.AppendText("tglog.txt");
            }

            dbManager = new DatabaseManager(this, logFile);

            // fill the state and country lists
            states = dbManager.BuildStates();
            countries = dbManager.BuildCountries();

            try
            {
                Uri regionImageUri = new Uri(System.IO.Path.GetFullPath("maps//northamerica.jpg"));
                RegionalMap.Source = new BitmapImage(regionImageUri);

                Uri backBUri = new Uri(System.IO.Path.GetFullPath("backb.png"));
                BackButton.Source = new BitmapImage(backBUri);

                Uri iconUri = new Uri(System.IO.Path.GetFullPath("TGfavicon.ico"));
                this.Icon = BitmapFrame.Create(iconUri);
            }
            catch(UriFormatException e)
            {
                logFile.WriteLine(Stopwatch.GetTimestamp().ToString() + ": " + e.ToString() + "\n");
            }

            // set the sizing of the map
            Grid.SetColumn(RegionalMap, 1);
            Grid.SetColumnSpan(RegionalMap, 1);
            Grid.SetRow(RegionalMap, 1);
            Grid.SetRowSpan(RegionalMap, 5);

            // set the icon, from the MSDN example at
            // http://msdn.microsoft.com/en-us/library/system.windows.window.icon(v=vs.110).aspx
                
            // setup the polygon grids for the start
            NorthAmericaPolygonGrid.IsEnabled = true;
            Grid.SetZIndex(UnitedStatesPolygonGrid, 0);
            Grid.SetZIndex(CanadaPolygonGrid, 0);
            Grid.SetZIndex(NorthAmericaPolygonGrid, 100);

            this.ResizeMode = ResizeMode.CanMinimize;

            BuildRegionalCountryMenu("NorthAmerica");

            // create the commands for the main menu
            SetupMainMenuCommands();

            // set the level the grid is on
            stateInfoLevel = false;
            stateMenuLevel = false;
            regionalMenuLevel = true;
        }

        public void DisplayMessageBox(string message)
        {
            MessageBox.Show(message);
        }

        // builds menu for US states or Canadian provinces
        private void BuildStateProvMenu(IEnumerable<string[]> stateNames)
        {

            MenuItem stateMenuItem;
            // setup and adds the 
            foreach(string[] state in stateNames) 
            {
                stateMenuItem = BuildMenuItem(state);
                stateMenuItem.Click += StateProvSelected_Click;
                
                // add the state menu item to the 
                SubregionalMenuPanel.Children.Add(stateMenuItem);
            }

            // change visibility settings
            StateInfoScrollViewer.Visibility = Visibility.Hidden;
            StatesMenuScrollViewer.Visibility = Visibility.Visible;
            RegionalMenuScrollViewer.Visibility = Visibility.Hidden;

            BackButton.IsEnabled = true;
            BackButton.Visibility = Visibility.Visible;

            // set the regional menu level variable
            regionalMenuLevel = false;
            stateMenuLevel = true;
            stateInfoLevel = false;

            // change which polygon grids are enabled 
            NorthAmericaPolygonGrid.IsEnabled = false;

            string country = stateNames.ElementAt(0).ToArray()[0];
            Uri countryImageUri;
            try
            {

                if (country.Contains("United") && country.Contains("States"))
                {
                    // set united states image, image size and polygon grid
                    countryImageUri = new Uri(System.IO.Path.GetFullPath("maps//UnitedStates.jpg"));
                    Grid.SetColumn(RegionalMap, 0);
                    Grid.SetColumnSpan(RegionalMap, 3);
                    Grid.SetRow(RegionalMap, 2);
                    Grid.SetRowSpan(RegionalMap, 3);
                    UnitedStatesPolygonGrid.IsEnabled = true;
                    Grid.SetZIndex(UnitedStatesPolygonGrid, 100);
                    Grid.SetZIndex(CanadaPolygonGrid, 0);
                    Grid.SetZIndex(NorthAmericaPolygonGrid, 0);

                }
                else if (country.Contains("Canada"))
                {
                    // set canada, image size and polygon grid
                    countryImageUri = new Uri(System.IO.Path.GetFullPath("maps//Canada.jpg"));
                    Grid.SetColumn(RegionalMap, 0);
                    Grid.SetColumnSpan(RegionalMap, 3);
                    Grid.SetRow(RegionalMap, 1);
                    Grid.SetRowSpan(RegionalMap, 5);
                    CanadaPolygonGrid.IsEnabled = true;
                    Grid.SetZIndex(UnitedStatesPolygonGrid, 0);
                    Grid.SetZIndex(CanadaPolygonGrid, 100);
                    Grid.SetZIndex(NorthAmericaPolygonGrid, 0);
                }
                else
                {
                    MessageBox.Show("US/Canada image not found");
                    countryImageUri = new Uri(System.IO.Path.GetFullPath("maps//northamerica.jpg"));
                }
                BitmapImage countryImage = new BitmapImage(countryImageUri);
                // set the source for the image on the site
                RegionalMap.Source = countryImage;
            }
            catch (UriFormatException urie)
            {
                logFile.WriteLine(Stopwatch.GetTimestamp().ToString() + ": " + urie.ToString() + "\n");
            }
        }

        // build the country menu for a given region
        private void BuildRegionalCountryMenu(string region)
        {
            if(!ValidRegion(region))
            {
                MessageBox.Show("Invalid Region");  
                return;
            }

            // empty whatever was in the regional menu
            RegionalMenuPanel.Children.Clear();

            IEnumerable<string[]> countryNames = from country in countries
                                                 where country[0] == region
                                                 select country;


            MenuItem countryMenuItem;
            // add all the country names to the regional menu
            foreach(string[] country in countryNames)
            {
                countryMenuItem = BuildMenuItem(country);

                // add the country menu item to the 
                if (countryMenuItem.Header.ToString() == "United States" ||
                    countryMenuItem.Header.ToString() == "Canada")
                {
                    countryMenuItem.Click += USCanadaSelected_Click;
                }
                else
                {
                    countryMenuItem.Click += CountrySelected_Click;
                }

                // make the regional menu visible
                RegionalMenuPanel.Children.Add(countryMenuItem);
                RegionalMenuScrollViewer.Visibility = Visibility.Visible;
                StatesMenuScrollViewer.Visibility = Visibility.Hidden;
                StateInfoScrollViewer.Visibility = Visibility.Hidden;

                BackButton.Visibility = Visibility.Visible;
                BackMenuItem.IsEnabled = true;
            }
        }

        // builds a menu item for the regional, US, or Canadian side menus
        private MenuItem BuildMenuItem(string[] strArr)
        {
            string name = strArr.ElementAt(1);

            MenuItem locationMenuItem = new MenuItem();
            locationMenuItem.Name = name.Replace(" ", "");
            locationMenuItem.Header = strArr.ElementAt(1);
            locationMenuItem.Padding = new Thickness(-10, 0, -30, 0);
            locationMenuItem.Background = Brushes.White;
            locationMenuItem.BorderBrush = Brushes.Orange;
            locationMenuItem.BorderThickness = new Thickness(2, 1, 2, 1);
            locationMenuItem.FontSize = FONT_SIZE;
            locationMenuItem.Height = MENU_ITEM_HEIGHT;
            locationMenuItem.Width = MENU_ITEM_WIDTH;
            locationMenuItem.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            locationMenuItem.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            locationMenuItem.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            locationMenuItem.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            return locationMenuItem;
        }
            
        // check if the string reperesents a valid region
        private bool ValidRegion(string region)
        {
            if(!(region.ToLower() == "northamerica") && 
               !(region.ToLower() == "southamerica") &&
               !(region.ToLower() == "centralamerica") &&
               !(region.ToLower() == "europe") &&
               !(region.ToLower() == "australiaandoceania") &&
               !(region.ToLower() == "centralandeastasia") &&
               !(region.ToLower() == "middleeastandnorthafrica") &&
               !(region.ToLower() == "subsaharanafrica"))
            {
                return false;
            }

            return true;
        }

        // creates the commands for the app's main menu
        private void SetupMainMenuCommands()
        {
            var exitBinding = new CommandBinding(MenuCommands.ExitCommand, ExitButton_Click);
            var backBinding = new CommandBinding(MenuCommands.BackCommand, BackButton_Click);
            var helpBinding = new CommandBinding(MenuCommands.HelpCommand, HelpButton_Click);
            var mapSourcesBinding = new CommandBinding(MenuCommands.MapSourcesCommand, MapSourcesButton_Click);
            var aboutBinding = new CommandBinding(MenuCommands.AboutCommand, AboutButton_Click);
            var homepageBinding = new CommandBinding(MenuCommands.HomepageCommand, HomepageButton_Click);

            CommandManager.RegisterClassCommandBinding(typeof(Window), exitBinding);
            CommandManager.RegisterClassCommandBinding(typeof(Window), backBinding);
            CommandManager.RegisterClassCommandBinding(typeof(Window), helpBinding);
            CommandManager.RegisterClassCommandBinding(typeof(Window), mapSourcesBinding);
            CommandManager.RegisterClassCommandBinding(typeof(Window), aboutBinding);  
            CommandManager.RegisterClassCommandBinding(typeof(Window), homepageBinding);
        }


        // Event that fires when the exit menu item is clicked
        // Exits the application
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Adapted from Tom_Overton's post at 
            // http://social.msdn.microsoft.com/Forums/vstudio/en-US/d3f223ac-7fca-486e-8939-adb46e9bf6c9/how-can-i-get-yesno-from-a-messagebox-in-wpf?forum=wpf
            if (MessageBox.Show("Do you want to exit the application?", "Confirmation",
                MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                this.logFile.Close();
                this.dbManager.CloseDatabase();
                Application.Current.Shutdown();
            }
        }

        // Event that fires when the help menu item is clicked
        // Creates a message box to display help information about the program
        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is a navigable 3 level map. The levels are regional," +
                            "national, and the state or provincial menu level. You can" +
                            "move down levels either by clicking the menu items on the" +
                            "side or by clicking on the country, state or province. To move" +
                            "back up the the menu levels use the back button in the top left" +
                            "corner or the back option in the main menu.\n\n" +
                            "Some state and provincial information pages contain a link to" +
                            "blogs on TravelGumbo.com. Clicking the link will open a window " +
                            "in your web browser that will display the blogs for that" +
                            "state or province");
        }
        
        // Event that fires when the map sources menu item is clicked
        // Creates a message box to display the sources of the maps
        private void MapSourcesButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The images used in this map are from the following sources:\n\n" +
                            "United States: The National Atlas\n" +
                            "Canada: Natural Resources Canada\n" +
                            "North America: CIA World Factbook\n" +
                            "States: Wikipedia\n" +
                            "Provinces: Wikipedia"
            );
        }

        // Event that fires when the about menu item is clicked
        // Creates a message box to display information about the map
        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is the prototype of a map program for TravelGumbo LLC,\n" +
                            "a travel blogging website.\n\nv0.1\n\nThis version includes only\n" +
                            "a North America with the United States and Canada functioning.\n\n" +
                            "This is a 32-bit application.");
        }

        // the event that fires when the us or canada is selected from the north american map
        private void USCanadaSelected_Click(object sender, RoutedEventArgs e)
        {
            MenuItem country = (MenuItem) sender;

            string cntry = country.Header.ToString();
            if (cntry != "United States" && cntry != "Canada")
            {
                return;
            }

            // empty the stack panel
            SubregionalMenuPanel.Children.Clear();

            IEnumerable<string[]> stateNames = from state in states
                                               where state[0] == cntry
                                               select state;
            // protective check
            if (stateNames.Count() == 0)
            {
                return;
            }

            // build the country menu
            BuildStateProvMenu(stateNames);
        }

        // the event that fires when a country other than the US or Canada is selected
        private void CountrySelected_Click(object sender, RoutedEventArgs e)
        {
            // information for a specific country goes here

        }

        // the event that fires when a US state or Canadian province is selected
        private void StateProvSelected_Click(object sender, RoutedEventArgs e)
        {
            MenuItem stateProv = (MenuItem)sender;

            IEnumerable<string[]> stateProvInformation = from s in this.states
                                                         where s[1] == stateProv.Header.ToString()
                                                          select s;


            // A just in case protection when something gets wrong
            if (stateProvInformation.Count() == 0)
            {
                return;
            }
            
            string[] stateProvStrings = stateProvInformation.ElementAt(0).ToArray();

            SetupStateProvInfo(stateProvStrings);
        }

        // set up state or prov information for a selected state or prov
        void SetupStateProvInfo(string[] stateProvStrings)
        {
            if (stateProvStrings == null || stateProvStrings.Length != STATE_PROV_FIELDS)
            {
                return;
            }

            if (stateProvStrings[0] == "United States")
            {
                StatehoodGrantedOnLabel.Content = "Statehood Granted On";
            }
            else if (stateProvStrings[0] == "Canada")
            {
                StatehoodGrantedOnLabel.Content = "Province Established On";
            }
            else
            {
                StatehoodGrantedOnLabel.Content = "StatehoodGrantedOn Error";
                return;
            }

            // get the state or provincial flag
            // credit to Simon at http://stackoverflow.com/questions/350027/setting-wpf-image-source-in-code
            Uri stateProvImageUri = new Uri(System.IO.Path.GetFullPath(stateProvStrings[2]));
            StateFlagImage.Source = new BitmapImage(stateProvImageUri);

            StateNameLabel.Content = stateProvStrings[1];
            StatehoodGrantedDateLabel.Content = stateProvStrings[3];

            // from http://stackoverflow.com/questions/7890159/programmatically-make-textblock-with-hyperlink-in-between-text

            // create blog hyperlinks
            Run tgRun;
            Hyperlink stateProvHyperlink;
            if (stateProvStrings[4] != "blank")
            {
                tgRun = new Run(stateProvStrings[1] + " TravelGumbo blogs");
                stateProvHyperlink = new Hyperlink(tgRun);
                stateProvHyperlink.NavigateUri = new Uri(stateProvStrings[4]);
                stateProvHyperlink.RequestNavigate += TGHyperlink_RequestNavigate;
                // not sure that this is all this will take, may need an event
            }
            else
            {
                tgRun = new Run("Share your stories");
                stateProvHyperlink = new Hyperlink(tgRun);
                stateProvHyperlink.NavigateUri = new Uri("http://www.travelgumbo.com/pages/Submissions");
                stateProvHyperlink.RequestNavigate += TGHyperlink_RequestNavigate;
                // not sure that this is all this will take, may need an event
            }

            // Insert the hyperlink
            TravelGumboLinkTextBlock.Inlines.Clear();
            TravelGumboLinkTextBlock.Inlines.Add(stateProvHyperlink);

            StateInfoScrollViewer.Visibility = Visibility.Visible;
            StatesMenuScrollViewer.Visibility = Visibility.Hidden;
            RegionalMenuScrollViewer.Visibility = Visibility.Hidden;

            regionalMenuLevel = false;
            stateInfoLevel = true;
            stateMenuLevel = false;
        }

        void TGHyperlink_RequestNavigate(object Sender, RequestNavigateEventArgs e)
        {
            Hyperlink tgLink = (Hyperlink) Sender;

            // open the webpage
            System.Diagnostics.Process.Start(tgLink.NavigateUri.AbsoluteUri);

        }

        void BackButton_Click(object Sender, RoutedEventArgs e)
        {
            if (stateInfoLevel) 
            { // go back from the state info level to the state menu level
                StateInfoScrollViewer.Visibility = Visibility.Hidden;
                StatesMenuScrollViewer.Visibility = Visibility.Visible;
                RegionalMenuScrollViewer.Visibility = Visibility.Hidden;

                regionalMenuLevel = false;
                stateMenuLevel = true;
                stateInfoLevel = false;
            }
            else if(stateMenuLevel)
            { // go back from the state menu level to the regional menu level
                StateInfoScrollViewer.Visibility = Visibility.Hidden;
                StatesMenuScrollViewer.Visibility = Visibility.Hidden;
                RegionalMenuScrollViewer.Visibility = Visibility.Visible;


                Grid.SetColumn(RegionalMap, 1);
                Grid.SetColumnSpan(RegionalMap, 1);
                Grid.SetRow(RegionalMap, 1);
                Grid.SetRowSpan(RegionalMap, 5);

                try
                {
                    Uri regionImageUri = new Uri(System.IO.Path.GetFullPath("maps//northamerica.jpg"));
                    RegionalMap.Source = new BitmapImage(regionImageUri);
                    Grid.SetColumn(RegionalMap, 1);
                    Grid.SetColumnSpan(RegionalMap, 1);
                    Grid.SetRow(RegionalMap, 1);
                    Grid.SetRowSpan(RegionalMap, 5);
                }
                catch (UriFormatException urie)
                {
                    logFile.WriteLine(Stopwatch.GetTimestamp().ToString() + ": " + urie.ToString() + "\n");
                }

                BackButton.Visibility = Visibility.Hidden;
                BackMenuItem.IsEnabled = false;

                UnitedStatesPolygonGrid.IsEnabled = false;
                CanadaPolygonGrid.IsEnabled = false;
                NorthAmericaPolygonGrid.IsEnabled = true;
                Grid.SetZIndex(UnitedStatesPolygonGrid, 0);
                Grid.SetZIndex(CanadaPolygonGrid, 0);
                Grid.SetZIndex(NorthAmericaPolygonGrid, 100);

                regionalMenuLevel = true;
                stateMenuLevel = false;
                stateInfoLevel = false;
            }
        }

        // the event that fires when a state or provincial polygon is clic1ked
        void StateProvPolygon_Clicked(object sender, RoutedEventArgs e)
        {
            Polygon stateProvPoly = (Polygon)sender;

            // prune "Polygon" from the name
            string name = stateProvPoly.Name.ToString();
            name = name.Replace("Polygon", "");

            IEnumerable<string[]> stateProvInformation = from s in this.states
                                                         where s[1].Replace(" ","") == name
                                                         select s;

            // A just in case protection when something gets wrong
            if(stateProvInformation.Count() == 0)
            {
                return;
            }

            string[] stateProvStrings = stateProvInformation.ElementAt(0).ToArray();

            // build the state/prov info
            SetupStateProvInfo(stateProvStrings);

            BackButton.Visibility = Visibility.Visible;
            BackMenuItem.IsEnabled = true;
        }

        // the event that fires when the United States, Alaska, or Canada polygons
        void UnitedStatesCanadaPolygon_Clicked(object sender, RoutedEventArgs e)
        {
            Polygon usCanadaPoly = (Polygon)sender;

            string name = usCanadaPoly.Name.ToString();
            name = name.Replace("Polygon", "");
            name = name.Replace("Alaska", "");

            // empty the stack panel
            SubregionalMenuPanel.Children.Clear();

            IEnumerable<string[]> stateNames = from state in states
                                               where state[0].Replace(" ","") == name
                                               select state;

            // protective check
            if (stateNames.Count() == 0)
            {
                return;
            }

            // build the country menu
            BuildStateProvMenu(stateNames);
        }

        // opens the TravelGumbo homepage
        void HomepageButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://travelgumbo.com");
        }

        // clean up a bit when the window is closing
        void MapWindow_Closing(object sender, CancelEventArgs e)
        {
            // close the log file and database
            this.logFile.Close();
            this.dbManager.CloseDatabase();
        }
    }
}

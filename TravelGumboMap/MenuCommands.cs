/* This class contains commands for the main menu 
 * of the TravelGumbo Map window. */

using System;
using System.Collections.Generic;
using System.Linq;
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

namespace TravelGumboMap
{
  /* This and all other commands text adapted from Shou Takeaka's response at
   * http://stackoverflow.com/questions/4709906/wpf-commands-how-to-declare-application-level-commands
   */ 

    public static class MenuCommands
    {
        // Command that will be applied to the exit menu item
        private static readonly RoutedUICommand exitCommand =
            new RoutedUICommand("Exit TravelGumbo World Map.", "ExitCommand", typeof(MenuCommands));

        // Command that will be applied to the back menu item
        private static readonly RoutedUICommand backCommand =
                new RoutedUICommand("Back up one level.", "BackCommand", typeof(MenuCommands));

        // Command that will be applied to the help menu item
        private static readonly RoutedUICommand helpCommand =
                new RoutedUICommand("Program help information.", "HelpCommand", typeof(MenuCommands));
        
        // Command that will be applied to the map sources menu item
        private static readonly RoutedUICommand mapSourcesCommand =
               new RoutedUICommand("Information on the sources of map images.", "MapSourcesCommands", typeof(MenuCommands));

        // Command that will be applied to the about menu item
        private static readonly RoutedUICommand aboutCommand =
                new RoutedUICommand("Information about the program.", "AboutCommand", typeof(MenuCommands));

        // Command that will be applied to the TG homepage menu item
        private static readonly RoutedUICommand homepageCommand =
                new RoutedUICommand("Visit TravelGumbo.com", "HomepageCommand", typeof(MenuCommands));

        // Property that returns the exit command
        public static RoutedUICommand ExitCommand
        {
            get
            {
                return exitCommand;
            }
        }

        // Property that returns the back command
        public static RoutedUICommand BackCommand
        {
            get
            {
                return backCommand;
            }
        }

        // Property that returns the help command
        public static RoutedUICommand HelpCommand
        {
            get
            {
                return helpCommand;
            }
        }

        // Property that returns the map sources command
        public static RoutedUICommand MapSourcesCommand
        {
            get
            {
                return mapSourcesCommand;
            }
        }

        // Property that returns the about command
        public static RoutedUICommand AboutCommand
        {
            get
            {
                return aboutCommand;
            }
        }

        // Property that returns the homepage command
        public static RoutedUICommand HomepageCommand
        {
            get
            {
                return homepageCommand;
            }
        }
    }
}

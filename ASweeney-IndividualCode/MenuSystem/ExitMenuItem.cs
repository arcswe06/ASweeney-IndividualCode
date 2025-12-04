using Capstone.MenuSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.MenuSystem
{
    /// <summary>
	/// Represents a menu item that exits the menu.
	/// </summary>
	internal class ExitMenuItem : MenuItem
    {
        // The parent in the menu hierarchy (not the base class) menu that will be exited if this item is selected
        private ConsoleMenu _menu;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExitMenuItem"/> class.
        /// </summary>
        /// <param name="parentItem">The parent menu that this item belongs to.</param>
        public ExitMenuItem(ConsoleMenu parentItem)
        {
            _menu = parentItem;
        }

        /// <summary>
        /// Gets the text to be displayed for the exit menu item.
        /// </summary>
        /// <returns>A string representing the menu text.</returns>
        public override string MenuText()
        {
            return "Exit";
        }

        /// <summary>
        /// Exits the parent menu
        /// </summary>
        public override void Select()
        {
            _menu.IsActive = false;
        }
    }
}

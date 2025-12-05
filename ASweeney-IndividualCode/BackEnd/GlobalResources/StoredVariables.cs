using ASweeney_IndividualCode.Backend.UserFunctions;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.Backend.GlobalData
{
    /// <summary>
    /// This stores values and the current user.
    /// </summary>
    public class StoredVariables
    {
        public static User CurrentUser { get; set; }
        public static SqliteConnection Connection { get; set; }
    }
}

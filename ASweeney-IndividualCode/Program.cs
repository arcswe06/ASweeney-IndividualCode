using ASweeney_IndividualCode.Backend.StartProcedure;
using ASweeney_IndividualCode.BackEnd.GlobalResources;
using ASweeney_IndividualCode.BackEnd.StartProcedure;
using System.Security.Cryptography;

class Program
{
    static void Main()
    {
        DatabaseInitialisation.Initialize();
        DefaultUser.Create();
        PressEnter.Press();
    }
}
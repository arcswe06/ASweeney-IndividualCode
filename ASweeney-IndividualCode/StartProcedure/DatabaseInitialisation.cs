using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndividualCode.GlobalData;
using Microsoft.Data.Sqlite;

namespace ASweeney_IndividualCode.StartProcedure
{
    internal class DatabaseInitialisation
    {
        public static void InitializeDatabase()
        {
            string connectionString = "Data Source=users.db;Version=3;";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            StoredVariables.Connection = connection;

            var command = connection.CreateCommand();

            command.CommandText = @"
        -- ============================
        -- USERS TABLE (Main Table)
        -- ============================
        CREATE TABLE IF NOT EXISTS Users (
            ID INTEGER PRIMARY KEY,
            Name TEXT NOT NULL,
            Password TEXT NOT NULL,
            UserType TEXT NOT NULL,   -- 'Student', 'PersonalSupervisor', 'SeniorTutor'
            Mood INTEGER,             -- only used for students
            HasPersonalSupervisor BOOLEAN DEFAULT 0
        );

        -- ====================================
        -- RELATIONSHIPS: Student - Supervisor
        -- ====================================
        -- Many students can have the same Personal Supervisor
        CREATE TABLE IF NOT EXISTS StudentSupervisor (
            StudentID INTEGER NOT NULL,
            PersonalSupervisorID INTEGER NOT NULL,
            PRIMARY KEY (StudentID, PersonalSupervisorID),
            FOREIGN KEY (StudentID) REFERENCES Users(ID),
            FOREIGN KEY (PersonalSupervisorID) REFERENCES Users(ID)
        );

        -------------------------------------
        -- MEETINGS TABLE
        -------------------------------------
        -- Students can have unlimited meetings
        -- Meeting is with either a Personal Supervisor or a Senior Tutor
        CREATE TABLE IF NOT EXISTS Meetings (
            MeetingID INTEGER PRIMARY KEY AUTOINCREMENT,
            StudentID INTEGER NOT NULL,
            StaffID INTEGER NOT NULL,
            MeetingName TEXT NOT NULL,
            MeetingDate TEXT NOT NULL,
            MeetingTime TEXT NOT NULL,
            FOREIGN KEY (StudentID) REFERENCES Users(ID),
            FOREIGN KEY (StaffID) REFERENCES Users(ID)
        );

        -------------------------------------
        -- REPORTS TABLE
        -------------------------------------
        -- Students may file unlimited reports
        -- Reports go to their Personal Supervisor
        CREATE TABLE IF NOT EXISTS Reports (
            ReportID INTEGER PRIMARY KEY AUTOINCREMENT,
            StudentID INTEGER NOT NULL,
            PersonalSupervisorID INTEGER NOT NULL,
            ReportText TEXT NOT NULL,
            Resolved BOOLEAN DEFAULT 0,
            CreatedDate TEXT NOT NULL,
            FOREIGN KEY (StudentID) REFERENCES Users(ID),
            FOREIGN KEY (PersonalSupervisorID) REFERENCES Users(ID)
        );

        -------------------------------------
        -- CHAT TABLE
        -------------------------------------
        -- Unlimited messages between any two users
        CREATE TABLE IF NOT EXISTS Chat (
            MessageID INTEGER PRIMARY KEY AUTOINCREMENT,
            SenderID INTEGER NOT NULL,
            ReceiverID INTEGER NOT NULL,
            Message TEXT NOT NULL,
            Timestamp TEXT NOT NULL,
            FOREIGN KEY (SenderID) REFERENCES Users(ID),
            FOREIGN KEY (ReceiverID) REFERENCES Users(ID)
        );
        ";

            command.ExecuteNonQuery();
        }

    }
}

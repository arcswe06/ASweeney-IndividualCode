using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASweeney_IndividualCode.StartProcedure
{
    internal class DatabaseInitialisation
    {
        public static void InitializeDatabase()
        {
            string connectionString = "Data Source=users.db;Version=3;";

            using var connection = new SqliteConnection(connectionString);
            connection.Open();

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
        -- RELATIONSHIPS: Student ↔ Supervisor
        -- ====================================
        -- Many students can have the same Personal Supervisor
        CREATE TABLE IF NOT EXISTS StudentSupervisor (
            StudentID INTEGER NOT NULL,
            PersonalSupervisorID INTEGER NOT NULL,
            PRIMARY KEY (StudentID, PersonalSupervisorID),
            FOREIGN KEY (StudentID) REFERENCES Users(ID),
            FOREIGN KEY (PersonalSupervisorID) REFERENCES Users(ID)
        );

        -- ============================
        -- MEETINGS TABLE
        -- ============================
        -- A meeting is between ONE student and EITHER:
        --   • a Personal Supervisor
        --   • a Senior Tutor
        CREATE TABLE IF NOT EXISTS Meetings (
            MeetingID INTEGER PRIMARY KEY AUTOINCREMENT,
            StudentID INTEGER NOT NULL,
            StaffID INTEGER NOT NULL,  -- PS or SeniorTutor
            MeetingDate TEXT NOT NULL, -- ISO 8601 string
            MeetingTime TEXT NOT NULL,
            FOREIGN KEY (StudentID) REFERENCES Users(ID),
            FOREIGN KEY (StaffID) REFERENCES Users(ID)
        );

        -- ============================
        -- REPORTS TABLE
        -- ============================
        -- Students create reports for their Personal Supervisors
        CREATE TABLE IF NOT EXISTS Reports (
            ReportID INTEGER PRIMARY KEY AUTOINCREMENT,
            StudentID INTEGER NOT NULL,
            PersonalSupervisorID INTEGER NOT NULL,
            Resolved BOOLEAN DEFAULT 0,
            CreatedDate TEXT NOT NULL,
            FOREIGN KEY (StudentID) REFERENCES Users(ID),
            FOREIGN KEY (PersonalSupervisorID) REFERENCES Users(ID)
        );

        -- ============================
        -- CHAT TABLE
        -- ============================
        -- Simple messaging system between any two IDs
        CREATE TABLE IF NOT EXISTS Chat (
            ChatID INTEGER PRIMARY KEY AUTOINCREMENT,
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

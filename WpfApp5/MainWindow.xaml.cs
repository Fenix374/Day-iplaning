using System;
using System.Collections.Generic;
using System.Windows;

namespace DailyPlanner
{
    public partial class MainWindow : Window
    {
        private Crud crUd;
        private List<Note> notes = new List<Note>();
        private DateTime selectedDate;

        public MainWindow()
        {
            InitializeComponent();
            crUd = new Crud("C:\\Users\\semen\\source\\repos\\WpfApp5\\WpfApp5\\dailyplaining.json");
            LoadNotes();
            selectedDate = DateTime.Today;
            DisplayNotes(selectedDate);
        }

        private void LoadNotes()
        {
            notes = crUd.Deserialize();
        }

        private void SaveNotes()
        {
            crUd.Serialize(notes);
        }

        private void DisplayNotes(DateTime date)
        {
            notesList.ItemsSource = null;
            notesList.ItemsSource = notes.FindAll(note => note.Date == date);
        }

        private void Calendar_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedDate = calendar.SelectedDate ?? DateTime.Today;
            DisplayNotes(selectedDate);
        }

        private void NotesList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (notesList.SelectedItem != null)
            {
                Note selectedNote = (Note)notesList.SelectedItem;
                noteTitleTextBox.Text = selectedNote.Title;
                noteDescriptionTextBox.Text = selectedNote.Description;
            }
        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
            Note newNote = new Note
            {
                Title = noteTitleTextBox.Text,
                Description = noteDescriptionTextBox.Text,
                Date = selectedDate
            };
            notes.Add(newNote);
            SaveNotes();
            DisplayNotes(selectedDate);
        }

        private void UpdateNoteButton_Click(object sender, RoutedEventArgs e)
        {
            if (notesList.SelectedItem != null)
            {
                Note selectedNote = (Note)notesList.SelectedItem;
                selectedNote.Title = noteTitleTextBox.Text;
                selectedNote.Description = noteDescriptionTextBox.Text;
                SaveNotes();
                DisplayNotes(selectedDate);
            }
        }

        private void DeleteNoteButton_Click(object sender, RoutedEventArgs e)
        {
            if (notesList.SelectedItem != null)
            {
                Note selectedNote = (Note)notesList.SelectedItem;
                notes.Remove(selectedNote);
                SaveNotes();
                DisplayNotes(selectedDate);
            }
        }
    }

    public class Note
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}

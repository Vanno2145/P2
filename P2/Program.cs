using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class Note
{
    public string Title { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }

    public Note(string title, string text)
    {
        Title = title;
        Text = text;
        CreatedAt = DateTime.Now;
    }
}

public class NoteManager
{
    private List<Note> notes;
    private const string FilePath = "notes.json";

    public NoteManager()
    {
        notes = new List<Note>();
        LoadNotes();
    }

    // Добавление заметки
    public void AddNote(string title, string text)
    {
        var note = new Note(title, text);
        notes.Add(note);
        SaveNotes();
    }

    // Удаление заметки по индексу
    public void RemoveNote(int index)
    {
        if (index >= 0 && index < notes.Count)
        {
            notes.RemoveAt(index);
            SaveNotes();
        }
        else
        {
            Console.WriteLine("Заметка с таким индексом не найдена.");
        }
    }

    // Редактирование заметки по индексу
    public void EditNote(int index, string newTitle, string newText)
    {
        if (index >= 0 && index < notes.Count)
        {
            notes[index].Title = newTitle;
            notes[index].Text = newText;
            SaveNotes();
        }
        else
        {
            Console.WriteLine("Заметка с таким индексом не найдена.");
        }
    }

    // Сохранение заметок в файл
    private void SaveNotes()
    {
        var json = JsonConvert.SerializeObject(notes, Formatting.Indented);
        File.WriteAllText(FilePath, json);
    }

    // Загрузка заметок из файла
    private void LoadNotes()
    {
        if (File.Exists(FilePath))
        {
            var json = File.ReadAllText(FilePath);
            notes = JsonConvert.DeserializeObject<List<Note>>(json) ?? new List<Note>();
        }
    }

    // Вывод всех заметок
    public void DisplayNotes()
    {
        for (int i = 0; i < notes.Count; i++)
        {
            var note = notes[i];
            Console.WriteLine($"[{i}] {note.Title} (Создано: {note.CreatedAt})");
            Console.WriteLine(note.Text);
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main()
    {
        var noteManager = new NoteManager();

        // Пример использования
        noteManager.AddNote("Первая заметка", "Это моя первая заметка!");
        noteManager.AddNote("Вторая заметка", "Эта заметка важна для меня.");
        
        Console.WriteLine("Список заметок:");
        noteManager.DisplayNotes();

        Console.WriteLine("Редактирование второй заметки...");
        noteManager.EditNote(1, "Обновленная вторая заметка", "Текст обновлен.");
        
        Console.WriteLine("Список заметок после редактирования:");
        noteManager.DisplayNotes();

        Console.WriteLine("Удаление первой заметки...");
        noteManager.RemoveNote(0);
        
        Console.WriteLine("Список заметок после удаления:");
        noteManager.DisplayNotes();
    }
}

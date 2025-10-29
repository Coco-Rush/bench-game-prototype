using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Unity;
using Unity.VisualScripting;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class ImportFromExternalFile
{
    private const string ROOT_PATH = "Assets/Resources/";
    private static List<WordData> listWordData;
    private static List<SentenceData> listSentenceData;
    private static List<PuzzleData> listPuzzleData;
    private static Dictionary<int, List<string>> chitChats;
    private static Dictionary<int, List<string>> solvedPuzzleDialogues;

#if UNITY_EDITOR
    [MenuItem("Tools/Import Everything")]
    public static void ImportEverythingAtOnce()
    {
        ImportChitChat();
        ImportSolvedDialogue();
        ImportWords();
        ImportSentences();
        ImportPuzzle();
    }
    [MenuItem("Tools/Import Sentences")]
    public static void ImportSentences()
    {
        // What do i need?
        // - path to csv file
        // - read content of the file
        // - make sure the file is built correctly
        // - create puzzle scriptableobject assets based on the values
        // - set these assets to npc
        // - puzzle needs correct sentence
        // - correct sentence need word assets and references to those words
        // - do that with id?
        // ---------------------------------------------------------
        
        string pathFile = EditorUtility.OpenFilePanel("Select SentenceData CSV", "", "csv");
        if (string.IsNullOrEmpty(pathFile)) return;
        
        string pathFolder = ROOT_PATH + "sentence-assets";

        string[] lines = File.ReadAllLines(pathFile);

        listSentenceData = new List<SentenceData>();

        if (!Directory.Exists(pathFolder))
            Directory.CreateDirectory(pathFolder);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');

            // first three are set and then afterwords are unspecified amount of numbers (linked to wordsId)
            SentenceData sentenceData = ScriptableObject.CreateInstance<SentenceData>();
            sentenceData.id = int.Parse(values[0]);
            if (Enum.TryParse(values[1], out Tense tense))
                sentenceData.tense = tense;
            if (Enum.TryParse(values[2], out Pronoun pronoun))
                sentenceData.pronoun = pronoun;

            sentenceData.words = new List<WordData>();

            for (int j = 3; j < values.Length; j++)
                if (int.TryParse(values[j], out int result))
                    sentenceData.words.Add(listWordData.Find(x => x.id == result));
                    
            sentenceData.SetSentence();
            string assetPath = $"{pathFolder}/{sentenceData.id}.asset";
            AssetDatabase.CreateAsset(sentenceData, assetPath);
            listSentenceData.Add(sentenceData);
        }


        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Import completed");
    }

    [MenuItem("Tools/Import Words From CSV")]
    public static void ImportWords()
    {
        string pathFile = EditorUtility.OpenFilePanel("Select Word CSV", "", "csv");
        if (string.IsNullOrEmpty(pathFile)) return;
        
        string pathFolder = ROOT_PATH + "word-assets/noun";

        listWordData = new List<WordData>();

        string[] lines = File.ReadAllLines(pathFile);

        if (!Directory.Exists(pathFolder))
            Directory.CreateDirectory(pathFolder);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');

            WordData wordData = ScriptableObject.CreateInstance<NounData>();
            wordData.id = int.Parse(values[0]);
            wordData.presentedWord = values[1];

            string assetPath = $"{pathFolder}/{wordData.presentedWord}.asset";
            AssetDatabase.CreateAsset(wordData, assetPath);
            listWordData.Add(wordData);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Import completed");
    }

    [MenuItem("Tools/Import Puzzles")]
    public static void ImportPuzzle()
    {
        listPuzzleData = new List<PuzzleData>();
        
        string pathFile = EditorUtility.OpenFilePanel("Select PuzzleData CSV", "", "csv");
        if (string.IsNullOrEmpty(pathFile)) return;
        
        string pathFolder = ROOT_PATH + "puzzle-assets";

        string[] lines = File.ReadAllLines(pathFile);

        if (!Directory.Exists(pathFolder))
            Directory.CreateDirectory(pathFolder);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');

            PuzzleData puzzleData = ScriptableObject.CreateInstance<PuzzleData>();
            puzzleData.id = int.Parse(values[0]);
            if (int.TryParse(values[1],out int sentenceKey))
                puzzleData.correctSentenceData = listSentenceData.Find(x => x.id == sentenceKey);
            
            if (int.TryParse(values[2], out int chitChatKey))
                puzzleData.dialogChitChat = chitChats[chitChatKey];
            if (int.TryParse(values[3], out int solvedDialoguesKey))
                puzzleData.dialogPuzzleSolved = solvedPuzzleDialogues[solvedDialoguesKey];
            
            puzzleData.dialogResponseFalse = values[4];
            puzzleData.dialogTimeRunOut = values[5];
            puzzleData.dialogPuzzlePrompt = values[6];
            
            if (int.TryParse(values[7], out int result))
                puzzleData.timelimitForPuzzleInSeconds = result;
            
            
            string assetPath = $"{pathFolder}/{puzzleData.id}.asset";
            AssetDatabase.CreateAsset(puzzleData, assetPath);
            listPuzzleData.Add(puzzleData);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Import Worked");
    }

    [MenuItem("Tools/Import ChitChat")]
    public static void ImportChitChat()
    {
        chitChats = new Dictionary<int, List<string>>();
        ImportStandardFileToDictionary("Select Chit Chat File", "","csv",chitChats);
        Debug.Log("Imported ChitChat");
    }

    [MenuItem("Tools/Import PuzzleData Solved Dialogue")]
    public static void ImportSolvedDialogue()
    {
        solvedPuzzleDialogues = new Dictionary<int, List<string>>();
        ImportStandardFileToDictionary("Select Puzzle Solved Dialogue","","csv",solvedPuzzleDialogues);
        Debug.Log("Imported Solved Dialogue Chats");
    }
    private static void ImportStandardFileToDictionary(string title, string directory, string extension, Dictionary<int, List<string>> dictionary)
    {
        string pathFile = EditorUtility.OpenFilePanel(title, directory, extension);
        if (string.IsNullOrEmpty(pathFile)) return;
        
        string[] lines = File.ReadAllLines(pathFile);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');
            dictionary.Add(int.Parse(values[0]), new List<string>());

            for (int j = 1; j < values.Length; j++) dictionary[int.Parse(values[0])].Add(values[j]);
        }
    }

    private static void ClearFolder(string path, Type type)
    {
        
    }
#endif
}

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Cards
{
    
    public class DatabaseHandler
    {

        public List<CardDataObject> loadedCards = new List<CardDataObject>();
        
        public List<CardTag> loadedTags = new List<CardTag>();
        
        public event Action OnLoadingStarted;
        public event Action<List<CardDataObject>, List<CardTag>> OnLoadingFinished;


        public DatabaseHandler()
        {
            
            
            
        }

        public void Initialize()
        {
            
            OnLoadingStarted?.Invoke();
            
            //Download *.csv file -> decode
            string csvPath = Application.persistentDataPath + "/Data/Cards.csv";

            string csvFile = File.ReadAllText(csvPath);
            
            loadedCards = GetCardData(csvFile);
            
            OnLoadingFinished?.Invoke(loadedCards, loadedTags);
            
        }

        List<CardDataObject> GetCardData(string csvContent)
        {
            List<CardDataObject> cards = new List<CardDataObject>();
            List<CardTag> tags = new List<CardTag>();
            
            CSVReader csv = new CSVReader(csvContent, ",");

            int index = -1;
            
            foreach (string[] line in csv)
            {

                if (index == -1)
                {

                    index++;
                    continue;
                    
                }

                CardDataObject card = CardDataObject.CreateInstance<CardDataObject>();

                card.name = line[0];
                card.description = line[1];
                card.icon = null;
                
                string[] tagArray = line[3].Split(' ');

                foreach (string tag in tagArray)
                {

                    CardTag newTag = new CardTag(tag, null);

                    if (tags.Find((x) => x.Name == newTag.Name) == null)
                    {
                        
                        tags.Add(newTag);
                        card.tags.Add(newTag);
                        
                    }
                    
                }
                
                cards.Add(card);
                index++;

            }

            loadedTags = tags;
            return cards;
        }

    }   
    
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Parseur
{
    public class Parseur
    {
        // Start is called before the first frame update
        protected FileInfo theSourceFile = null;
        protected StreamReader reader = null;
        protected string text = " "; // assigned to allow first line to be read below

        int count = 0;

        public Dictionary<string, List<Dictionary<string, object>>> readFile(string path)
        {
            //path = path.Replace("%2F", "/");
            //path = path.Replace("%3A", ":");
            theSourceFile = new FileInfo(path);
            reader = theSourceFile.OpenText();
            Dictionary<string, List<Dictionary<string, object>>> dictionary = new Dictionary<string, List<Dictionary<string, object>>>();
            text = reader.ReadLine();
            do
            {
                string[] element = text.Split(';');
                string plant = element[0];
                int x = Int32.Parse(element[1]);
                int y = Int32.Parse(element[2]);
                if (element.Length > 3) // Line case
                {
                    string stpoints = element[3].Replace("[", string.Empty).Replace("]", string.Empty);

                    string[] points = stpoints.Split(',');
                    List<Vector3> listpoint = new List<Vector3>();
                    for (int i = 0; i < points.Length; i += 2)
                    {

                        listpoint.Add(new Vector3(
                            float.Parse(points[i]),
                            0,
                            float.Parse(points[i + 1])));
                    };
                    AddElement(plant, new Vector3(x, 0, y), listpoint, dictionary);
                }
                else // case pixel element 
                {
                    AddElement(plant, new Vector3(x, 0, y), dictionary);

                }
                text = reader.ReadLine();
            } while (text != null);

         
            return dictionary;
        }

        void AddElement(string type, Vector3 coord, Dictionary<string, List<Dictionary<string, object>>> all)
        {
            if (all.ContainsKey(type))
            {
                all[type].Add(new Dictionary<string, object>() { { "coord", coord } });
            }
            else
            {
                all.Add(type, new List<Dictionary<string, object>> { new Dictionary<string, object>() { { "coord", coord } } });
            }
        }

        void AddElement(string type, Vector3 coord, List<Vector3> listpoints, Dictionary<string, List<Dictionary<string, object>>> all)
        {
            //  listpoints.Insert(0, coord);
            if (all.ContainsKey(type))
            {
                all[type].Add(new Dictionary<string, object>() { { "coord", coord }, { "isComposed", listpoints } });
            }
            else
            {
                all.Add(type, new List<Dictionary<string, object>> { new Dictionary<string, object>() { { "coord", coord }, { "isComposed", listpoints } } });
            }
        }
    }
}

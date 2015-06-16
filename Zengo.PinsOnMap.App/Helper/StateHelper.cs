#region Usings

using System;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

#endregion

namespace Zengo.PinsOnMap.App.Helper
{
    public class StateHelper
    {
        public const string StateFileName = "pinsonmap_state.dat";

        /// <summary>
        /// Serialise our state
        /// </summary>
        public static void Save(AppState state, string profileName)
        {
            using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream stream = file.CreateFile(profileName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(AppState));

                    serializer.Serialize(stream, state);
                    stream.Close();
                }
            }
        }

        /// <summary>
        /// De-Serialise our state
        /// </summary>
        public static AppState Load(string filename)
        {
            try
            {
                using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (file.FileExists(filename))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(AppState));

                        using (IsolatedStorageFileStream stream = file.OpenFile(filename, System.IO.FileMode.Open))
                        {
                            return (AppState)serializer.Deserialize(stream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return new AppState();
        }
    }
}

using Binottery.Constants;
using Binottery.Model;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Binottery.Utilities.Serializer
{
    public class XMLGameSerializer : IGameSerializer
    {
        public XMLGameSerializer()
        {
        }

        /// <summary>
        /// determines if there is a saved game
        /// </summary>
        /// <returns></returns>
        public bool HasGameSaved()
        {
            return File.Exists(Settings.SavedGameFileName);
        }

        /// <summary>
        /// load a game from the disk if exist
        /// </summary>
        /// <returns>a state of the loaded game</returns>
        public State Load()
        {
            State state = null;
            if(HasGameSaved())
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(State));
                    using (StreamReader reader = new StreamReader(Settings.SavedGameFileName))
                    {
                        state = (State)serializer.Deserialize(reader);
                    }
                }
                catch (InvalidOperationException e)
                {
                    ExceptionManager.Instance.RaiseException(e);
                }
                catch (IOException e)
                {
                    ExceptionManager.Instance.RaiseException(e);
                }
                catch (Exception e)
                {
                    ExceptionManager.Instance.RaiseException(e);
                }

                return state;
            }

            return state;
        }

        /// <summary>
        /// save the current state of the game to the disk
        /// </summary>
        /// <param name="state">current state to be saved</param>
        public void Save(State state)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(State));
                Stream fs = new FileStream(Settings.SavedGameFileName, FileMode.Create);
                XmlWriter writer = new XmlTextWriter(fs, Encoding.Unicode);
                serializer.Serialize(writer, state);
                writer.Close();
            }
            catch (IOException e)
            {
                ExceptionManager.Instance.RaiseException(e);
            }
            catch (InvalidOperationException e)
            {
                ExceptionManager.Instance.RaiseException(e);
            }
            catch (Exception e)
            {
                ExceptionManager.Instance.RaiseException(e);
            }
        }
    }
}

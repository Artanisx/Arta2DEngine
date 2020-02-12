using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arta2DEngine.Input
{
    /// <summary>
    /// This class will take care of handling all commands, creating a list of them and updating them all.
    /// </summary>
    public static class HandleInputs
    {
        // FIELDS
        private static List<Command> listOfCommands = new List<Command>();

        // METHODS

        /// <summary>
        /// This method will add a command to the list
        /// </summary>
        /// <param name="command">The command to add to the list of commands to be handled by HandleInput</param>
        /// <param name="singlePressEvent">The method to execute for a Single Press Event. If not specified it will be null.</param>
        /// <param name="continuedPressEvent">The method to execute for a Continued Press Event. If not specified it will be null.</param>
        public static void AddCommand(Command command, EventHandler singlePressEvent = null, EventHandler continuedPressEvent = null)
        {
            if (command != null)
            {
                // Check this command is NOT already in the list
                foreach (Command commandOfList in listOfCommands)
                {
                    if (commandOfList == command)
                    {
                        // The command is already in the list, do nothing
                        return;
                    }
                }

                // Apparently this is a new command, add it.
                listOfCommands.Add(command);

                // If we passed a singlepressevent, then add it
                if (singlePressEvent != null)
                    SetSinglePressEvent(command, singlePressEvent);

                // If we passed a continuedPressEvent, then add it
                if (continuedPressEvent != null)
                    SetContinuedPressEvent(command, continuedPressEvent);
            }
        }

        /// <summary>
        /// This method will remove a command from the list
        /// </summary>
        /// <param name="command">The command to remove to the list of commands to be handled by HandleInpu</param>
        public static void DelCommand(Command command)
        {
            if (command != null)
            {
                foreach (Command commandOfList in listOfCommands)
                {
                    if (commandOfList == command)
                    {
                        listOfCommands.Remove(commandOfList);
                        break;
                    }
                        
                }
            }                
        }

        /// <summary>
        /// This method will set a SinglePressEvent for a specified command, if it's present in the list.
        /// </summary>
        /// <param name="command">The commanda we want to update</param>
        /// <param name="singlePressEvent">The new event handler for the SinglePress event</param>
        public static void SetSinglePressEvent(Command command, EventHandler singlePressEvent)
        {
            if (command != null && singlePressEvent != null)
            {
                // There is a command that isn't null and an eventhandler for the single press event.

                // let's check if the command exists
                foreach (Command commandToCheck in listOfCommands)
                {
                    if (commandToCheck == command)
                    {
                        command.SinglePress += singlePressEvent;

                        // Event added, we can break out of this
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// This method will set a ContinuedPressEvent for a specified command, if it's present in the list.
        /// </summary>
        /// <param name="command">The commanda we want to update</param>
        /// <param name="continuedPressEvent">The new event handler for the SinglePress event</param>
        public static void SetContinuedPressEvent(Command command, EventHandler continuedPressEvent)
        {
            if (command != null && continuedPressEvent != null)
            {
                // There is a command that isn't null and an eventhandler for the continued press event.

                // let's check if the command exists
                foreach (Command commandToCheck in listOfCommands)
                {
                    if (commandToCheck == command)
                    {                        
                        command.ContinuedPress += continuedPressEvent;

                        // Event added, we can break out of this
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the total number of commands in the list
        /// </summary>
        /// <returns>The number of total commands stored in the list</returns>
        public static int GetCommandsCount()
        {
            return listOfCommands.Count();
        }

        /// <summary>
        /// Clears the command list
        /// </summary>        
        public static void Clear()
        {
            listOfCommands.Clear();
        }

        /// <summary>
        /// This method will update all commands. It must be called inside an Update()
        /// </summary>
        public static void Update()
        {
            foreach (Command command in listOfCommands)
            {
                command.Update();
            }
        }
    }
}

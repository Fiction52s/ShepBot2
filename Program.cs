using System;
using TwitchLib.Api;


namespace ShepBot2
{
    class Program
    {
        // Bot settings

        static IrcClient irc;

        static Deck deck;

        static Program()
        {
            // Initialize and connect to Twitch chat
            irc = new IrcClient("irc.twitch.tv", 6667,
                Env._botName, Env._twitchOAuth, Env._broadcasterName);
              deck = new Deck();
        }

        // Listen to the chat until program exits
        static void ProcessUserMessages()
        {
            while (true)
            {
                // Read any message from the chat room
                string message = irc.ReadMessage();
                Console.WriteLine(message); // Print raw irc messages

                if (message.Contains("PRIVMSG"))
                {
                    // Messages from the users will look something like this (without quotes):
                    // Format: ":[user]![user]@[user].tmi.twitch.tv PRIVMSG #[channel] :[message]"

                    // Modify message to only retrieve user and message
                    int intIndexParseSign = message.IndexOf('!');
                    string userName = message.Substring(1, intIndexParseSign - 1); // parse username from specific section (without quotes)
                                                                                   // Format: ":[user]!"
                                                                                   // Get user's message
                    intIndexParseSign = message.IndexOf(" :");
                    message = message.Substring(intIndexParseSign + 2);

                    //Console.WriteLine(message); // Print parsed irc message (debugging only)

                    HandleMessage(userName, message);
                }
            }
        }

        static void RunRPS( string playerMove )
        {
            string[] moves = { "rock", "scissors", "paper" };
            int moveIndex = -1;
            for (int i = 0; i < moves.Length; ++i)
            {
                if (playerMove.Equals(moves[i]))
                {
                    moveIndex = i;
                    break;
                }
            }
            if (moveIndex >= 0)
            {
                Random rand = new Random();
                int cMove = rand.Next() % 3;
                string cMoveStr = moves[cMove];

                string shepbotPlay = "shepbot plays " + cMoveStr + "..... ";

                if (moveIndex == cMove - 1 || (cMove == 0 && moveIndex == 2))
                {
                    irc.SendPublicChatMessage(shepbotPlay + "you beat me >:(");
                }
                else if (moveIndex == cMove)
                {
                    irc.SendPublicChatMessage(shepbotPlay + "tied :|");
                }
                else
                {
                    irc.SendPublicChatMessage(shepbotPlay + "WOOOOOO GET REKT I WIN");
                }
            }
        }

        static void RunMarth()
        {
            Random rand = new Random();
            int move = rand.Next() % 5;
            string moveStr = "";
            switch (move)
            {
                case 0:
                    moveStr = "DUURRRRR Fsmash";
                    break;
                case 1:
                    moveStr = "down tilt down tilt down tilt";
                    break;
                case 2:
                    moveStr = "I dash back so my spacing is good";
                    break;
                case 3:
                    moveStr = "my character has such a hard time recovering";
                    break;
                case 4:
                    moveStr = "marth/fox is even";
                    break;
            }

            string fullStr = "Marth main: " + moveStr;

            irc.SendPublicChatMessage(fullStr);
        }

        static void Main(string[] args)
        {
            // Ping to the server to make sure this bot stays connected to the chat
            // Server will respond back to this bot with a PONG (without quotes):
            // Example: ":tmi.twitch.tv PONG tmi.twitch.tv :irc.twitch.tv"
            PingSender ping = new PingSender(irc);
            ping.Start();

            // Listen to the chat until program exits
            ProcessUserMessages();
        }

        static void HandleMessage(string userName, string message )
        {
            string[] words = message.Split(' ');
            string command = words[0];

            if (userName.Equals(Env._broadcasterName))
            {
                HandleBroadcasterMessage(message);
            }
            // General commands anyone can use
            if (command.Equals("!hello"))
            {
                irc.SendPublicChatMessage("Hello World!");
            }
            else if (command.Equals("!chat"))
            {
                irc.SendPublicChatMessage("blahblah");
            }
            else if(command.Equals("!controller"))
            {
                irc.SendPublicChatMessage("n3zmodgod");
            }
            else if(command.Equals( "!marth"))
            {
                RunMarth();
            }
            else if(command.Equals( "!rps"))
            {
                if( words.Length == 2 )
                {
                    RunRPS(words[1]);
                }
            }
            else if( command.Equals( "!wobbling"))
            {
                irc.SendPublicChatMessage("Wobbling is a degenerate mechanic used by bad players to waste everyone's time. You should want it completely banned if you appreciate your own finite lifespan and memories at all.");
            }
            else if( command.Equals("!drawcard"))
            {
                irc.SendPublicChatMessage(deck.DrawCard());
            }
        }

        static void HandleBroadcasterMessage( string message )
        {
            if (message.Equals("!exitbot"))
            {
                irc.SendPublicChatMessage("shepbot OUT");
                Environment.Exit(0); // Stop the program
            }
        }
    }
}
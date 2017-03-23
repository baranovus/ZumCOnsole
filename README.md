# ZumConsole
Form1 - simple console window
Form2 - TCP connection settings - text windows fro enetring hostnsme/IP address and port number
Form3 - sends special console command "energyscan", parses response, getting power distribution
        along radio channels and builds the bar graph.
        
Both Form1 and Form3 have to use one TCP console connection. If the connection has been opened in Form1, Form3 have to use it for sending console command rather than open it's own.

Singleton NetConn class is used for network connection.
GlobalVars class has beed added for learning puposes and is not currently used

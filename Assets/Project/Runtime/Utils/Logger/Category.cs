namespace Utils
{
	/// <summary>
	/// Categories for sorting and filtering logs
	/// </summary>
	public enum Category
	{
		/// <summary>
		/// Category for the log isn't known or doesn't exist
		/// </summary>
		Unknown,

		//Core Functionality
		/// <summary>
		/// Logs relating to the programs threading behavior
		/// </summary>
		Threading,
		/// <summary>
		/// Logs relating to the Save System
		/// </summary>
		Save,
		
		//Servers and Admin
		/// <summary>
		/// Logs relating to general server functionality
		/// </summary>
		Server,
		/// <summary>
		/// Logs relating to client-server connections
		/// </summary>
		Connections,

		//Sound and Audio
		/// <summary>
		/// Logs relating to Sound Effects and Music
		/// </summary>
		Audio,

		//Sprites and Particles
		/// <summary>
		/// Logs relating to Sprites and the SpriteHandler
		/// </summary>
		Sprites,
		/// <summary>
		/// Logs relating to Particles and the Particle System
		/// </summary>
		Particles,

		//In-Game Systems
		/// <summary>
		/// Logs relating to the damage System
		/// </summary>
		Damage,
		/// <summary>
		/// Logs relating to the Voxel World System
		/// </summary>
		Voxel,

		//Interface and Controls
		/// <summary>
		/// Logs relating to displaying the general user interface
		/// </summary>
		UI,
		/// <summary>
		/// Logs relating registering keystrokes and mouse clicks
		/// </summary>
		Input,
		/// <summary>
		/// Logs relating to the keybinding settings
		/// </summary>
		Keybindings,
		/// <summary>
		/// Logs relating to the animation
		/// </summary>
		Animation,

		//Player and Mob Features
		/// <summary>
		/// Logs relating to Player Character settings and appearance
		/// </summary>
		Character,
		/// <summary>
		/// Logs relating to spawning players, mobs, and objects with inventories into the game
		/// </summary>
		EntitySpawn,
		/// <summary>
		/// Logs relating to the autonomous actions of non-player characters
		/// </summary>
		Mobs,
		/// <summary>
		/// Logs relating to player and mob conditions and health
		/// </summary>
		Health,

		//Interaction and Movement
		/// <summary>
		/// Logs relating to players and mobs interacting with the in-game environment
		/// </summary>
		Interaction,
		/// <summary>
		/// Logs relating to player, mob and object movement
		/// </summary>
		Movement,
		
		//Items and Inventory
		/// <summary>
		/// Logs relating to item storage and item slots
		/// </summary>
		Inventory,
		/// <summary>
		/// Logs relating specifically to player inventory
		/// </summary>
		PlayerInventory,
		
		//General Debugging and Editor logs
		/// <summary>
		/// Logs relating to the Debug Console itself
		/// </summary>
		DebugConsole,
		/// <summary>
		/// Logs relating to debugging and tests
		/// </summary>
		Tests,
		/// <summary>
		/// Logs for use in the editor
		/// </summary>
		Editor
	}
}
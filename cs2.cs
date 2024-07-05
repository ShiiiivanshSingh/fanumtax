using System;
using System.Collections.Generic;

// Define a Player class to represent players in the game
class Player
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Armor { get; set; }
    public bool IsAlive { get; set; }
    
    public Player(string name)
    {
        Name = name;
        Health = 100;
        Armor = 0;
        IsAlive = true;
    }
    
    public void TakeDamage(int damage)
    {
        int totalDamage = damage;
        if (Armor > 0)
        {
            if (Armor >= damage)
            {
                Armor -= damage;
                totalDamage = 0;
            }
            else
            {
                totalDamage -= Armor;
                Armor = 0;
            }
        }
        
        Health -= totalDamage;
        
        if (Health <= 0)
        {
            IsAlive = false;
            Health = 0;
        }
    }
    
    public void DisplayStatus()
    {
        Console.WriteLine($"Name: {Name}, Health: {Health}, Armor: {Armor}, Alive: {IsAlive}");
    }
}

// Define a Game class to manage the game flow
class Game
{
    private List<Player> players = new List<Player>();
    
    public void AddPlayer(Player player)
    {
        players.Add(player);
    }
    
    public void StartGame()
    {
        Console.WriteLine("Game starting with players:");
        foreach (var player in players)
        {
            player.DisplayStatus();
        }
        
        // Simulate a round of combat
        Random random = new Random();
        while (players.Count > 1)
        {
            int attackerIndex = random.Next(players.Count);
            int targetIndex = random.Next(players.Count);
            
            if (attackerIndex != targetIndex && players[attackerIndex].IsAlive && players[targetIndex].IsAlive)
            {
                int damage = random.Next(20, 50); // Random damage between 20 to 50
                players[targetIndex].TakeDamage(damage);
                
                Console.WriteLine($"{players[attackerIndex].Name} attacks {players[targetIndex].Name} for {damage} damage!");
                players[targetIndex].DisplayStatus();
                
                if (!players[targetIndex].IsAlive)
                {
                    Console.WriteLine($"{players[targetIndex].Name} has been eliminated!");
                    players.RemoveAt(targetIndex);
                }
            }
        }
        
        Console.WriteLine($"Game over! {players[0].Name} is the winner!");
    }
}

// Main entry point
class Program
{
    static void Main()
    {
        Game game = new Game();
        
        // Create players
        Player player1 = new Player("Player 1");
        Player player2 = new Player("Player 2");
        Player player3 = new Player("Player 3");
        
        // Add players to the game
        game.AddPlayer(player1);
        game.AddPlayer(player2);
        game.AddPlayer(player3);
        
        // Start the game
        game.StartGame();
    }
}

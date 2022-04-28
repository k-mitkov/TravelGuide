using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelGuide.Database.Entities;
using TravelGuide.Database.Interfaces;
using TravelGuide.Database.Repositories;

namespace DBTest
{
    class Program
    {
        static void Main(string[] args)
        {

            IRepositoryManager repositoryManager = new RepositoryManager();

            List<User> users = new List<User>();

            //Task.Run(async () => users = (await repositoryManager.UsersRepository.SelectAllAsync()).ToList()).Wait();

            string query = "SELECT * FROM Users";

            Task.Run(async () => users = (await repositoryManager.QueryAsync<User>(query)).ToList()).Wait();

            if (users != null && users.Count != 0)
            {
                foreach (var user in users)
                {

                    Console.WriteLine("Usename: " + user.Username + "\nPassword: " + user.Password + "\nEmail: " + user.Email);
                }
            }
            else
            {
                Console.WriteLine("No users found");
            }

            //int count = 0;

            //string query = "SELECT COUNT(Username) FROM Users";

            //Task.Run(async () => count = (await repositoryManager.ExecuteScalarAsync<int>(query))).Wait();

            //Console.WriteLine(count);
        }
    }
}


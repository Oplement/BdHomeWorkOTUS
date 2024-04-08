

using HW;
using HW.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq;

namespace HW.Storage
{
    public class ClientStorage
    {
        static public void ClientsInsert(string FirstName, string LastName, string Email, string PhoneNumber)
        {
            using (var context = new BankingDbContext())
            {
                // Создание нового клиента
                var newClient = new Client
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    PhoneNumber = PhoneNumber
                };

                // Добавление клиента в базу данных
                context.Clients.Add(newClient);

                // Сохранение изменений
                context.SaveChanges();
            }

        }

        static public List<Client> ClientsGetAll()
        {
            using (var context = new BankingDbContext())
            {
                // Получение клиента для удаления
                List<Client> clientToDelete = context.Clients.ToList();

                return clientToDelete;
            }
        }

        static Client? ClientsGetOneID(int clientId)
        {
            using (var context = new BankingDbContext())
            {
                // Получение списка клиентов
                var clients = context.Clients.ToList();

                // Получение конкретного клиента по идентификатору
                Client? specificClient = context.Clients.Find(clientId);

                return specificClient;
            }

        }

        static string ClientsUpdate(int clientId, string? FirstName = null, string? LastName = null, string? Email = null, string? PhoneNumber = null)
        {
            using (var context = new BankingDbContext())
            {
                // Получение клиента для обновления
                var clientToUpdate = context.Clients.Find(clientId);

                if(clientToUpdate == null)
                {
                    // Изменение данных клиента

                    if (FirstName != null)
                    {
                        clientToUpdate.FirstName = FirstName;
                    }

                    if (LastName != null)
                    {
                        clientToUpdate.LastName = LastName;
                    }

                    if(Email != null)
                    {
                        clientToUpdate.Email = Email;
                    }

                    if (PhoneNumber != null)
                    {
                        clientToUpdate.PhoneNumber = PhoneNumber;
                    }

                    // Сохранение изменений
                    context.SaveChanges();

                    return "Изменения внесены";
                }
                else
                {
                    return $"Отсутствует клиент с ID {clientId}";
                }

            
            }

        }

        static public string ClientsDeleteID(string clientId)
        {
            int Id = Convert.ToInt32(clientId);

            using (var context = new BankingDbContext())
            {
                // Получение клиента для удаления
                var clientToDelete = context.Clients.Find(Id);

                if (clientToDelete != null)
                {
                    // Удаление клиента из базы данных
                    context.Clients.Remove(clientToDelete);

                    // Сохранение изменений
                    context.SaveChanges();

                    return $"Удален клиент с ID {clientId}";
                }
                else
                {
                    return $"Отсутствует клиент с ID {clientId}";
                }


            }

        }

     


    }
}

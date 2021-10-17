using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Week4.EsFinale.Client.Contract;

namespace Week4.EsFinale.Client
{
    public class Menu
    {
        internal static void Start()
        {
            bool quit = false;
            char choice;
            do
            {
                Console.WriteLine("Seleziona un'opzione del menu" +
                        "\n[ 1 ] - Inserisci un nuovo ordine " +
                        "\n[ 2 ] - Elimina un ordine" +
                        "\n[ 3 ] - Modifica dati di un ordine" +
                        "\n[ 4 ] - Visualizza tutti gli ordini" +
                        "\n" +
                        "\n[ 5 ] - Inserisci un nuovo cliente " +
                        "\n[ 6 ] - Elimina un cliente" +
                        "\n[ 7 ] - Modifica dati di un cliente" +
                        "\n[ 8 ] - Visualizza tutti i clienti" +
                        "\n" +
                        "\n[ q ] - ESCI");

                choice = Console.ReadKey().KeyChar;

                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        AddNewOrder();
                        break;
                    case '2':
                        DeleteOrder();
                        break;
                    case '3':
                        //EditOrder();
                        break;
                    case '4':
                        //FetchOrders();
                        break;
                    case '5':
                        //AddNewCustomer();
                        break;
                    case '6':
                        //DeleteCustomer();
                        break;
                    case '7':
                        //EditCustomer();
                        break;
                    case '8':
                        //FetchCustomers();
                        break;
                    case 'q':
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("Scelta sconosciuta.");
                        break;
                }

            } while (!quit);

        }

        private static void DeleteOrder()
        {
            string orderToDelete = string.Empty;
            do
            {
                Console.WriteLine("Scegli codice ordine che vuoi cancellare:");
                orderToDelete = Console.ReadLine();

            } while (string.IsNullOrEmpty(orderToDelete));

            HttpClient client = new HttpClient();
            HttpRequestMessage postRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"https://localhost:44371/api/order/{orderToDelete}" )
            };
            HttpResponseMessage postResponse = client.SendAsync(postRequest).Result;
            if (postResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Ordine cancellato!");
            }

        }

        private static void AddNewOrder()
        {
            string orderCode = GetData("codice ordine");
            DateTime dt = GetDataOrder();
            string productCode = GetData("codice prodotto");
            decimal toPay = GetPay();
            int idCustomer = GetIdCustomer();

            HttpClient client = new HttpClient();
            HttpRequestMessage postRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post, 
                RequestUri = new Uri("https://localhost:44371/api/order")
            };

            OrderContract newOrder = new OrderContract
            {
                OrderCode = orderCode,
                ProductCode = productCode,
                DateOfOrder = dt,
                ToPay = toPay,
                IdCustomer = idCustomer
            };

            string newOrderJson = JsonConvert.SerializeObject(newOrder);

            postRequest.Content = new StringContent(
                newOrderJson,
                Encoding.UTF8,
                "application/json" 
            );

            HttpResponseMessage postResponse = client.SendAsync(postRequest).Result;
            if (postResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                string data = postResponse.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<OrderContract>(data);

                Console.WriteLine($"Ordine aggiunto con successo, con Id: {result.Id}");
            }
        }

        private static int GetIdCustomer()
        {
            int idCustomer;
            do
            {
                Console.WriteLine("Inserisci ID del customer:");

            } while (!int.TryParse(Console.ReadLine(), out idCustomer));
            return idCustomer;
        }

        private static decimal GetPay()
        {
            decimal payment;
            do
            {
                Console.WriteLine("Inserisci importo:");

            } while (!decimal.TryParse(Console.ReadLine(), out payment));
            return payment;
        }

        private static DateTime GetDataOrder()
        {
            DateTime dt = new DateTime();
            do
            {
                Console.WriteLine("Inserisci la data:");

            } while (!DateTime.TryParse(Console.ReadLine(), out dt));
            return dt;
        }

        private static string GetData(string v)
        {
            string data = string.Empty;
            do
            {
                Console.WriteLine($"Scrivi {v} ->\n");
                data = Console.ReadLine();

            } while (string.IsNullOrEmpty(data));
            return data;
        }
    } 
}

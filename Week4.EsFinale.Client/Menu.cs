using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Week4.EsFinale.Client.Contract;
using Week4.EsFinale.Core.Models;

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
                        EditOrder();
                        break;
                    case '4':
                        FetchOrders();
                        break;
                    case '5':
                        AddNewCustomer();
                        break;
                    case '6':
                        DeleteCustomer();
                        break;
                    case '7':
                        EditCustomer();
                        break;
                    case '8':
                        FetchCustomers();
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

        private static List<CustomerContract> FetchCustomers()
        {
            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://localhost:44371/api/customer")
            };

            HttpResponseMessage response = client.SendAsync(request).Result;
            List<CustomerContract> result = new List<CustomerContract>();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                result = JsonConvert.DeserializeObject<List<CustomerContract>>(data);

                foreach (CustomerContract c in result)
                {
                    Console.WriteLine($"Id:{c.Id},Customer Code: {c.CustomerCode},Nome: {c.Name}, Surname: {c.Surname}, Ordini: {c.orders.Count}");
                }
            }
            return result;
        }

        private static void EditCustomer()
        {
            FetchCustomers();
            string codiceCus = string.Empty;
            do
            {
                Console.WriteLine("Scegli codice del customer che vorresti modificare:");
                codiceCus = Console.ReadLine();

            } while (string.IsNullOrEmpty(codiceCus));

            string nameCustomer = GetDataCustomer("name");
            string surnameCustomer = GetDataCustomer("surname");
            List<Order> allOrders = new List<Order>();

            HttpClient client = new HttpClient();
            HttpRequestMessage postRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:44371/api/customer/{codiceCus}")
            };

            CustomerContract updateCustomer = new CustomerContract()
            {
                CustomerCode = codiceCus,
                Name = nameCustomer,
                Surname = surnameCustomer,
                orders = allOrders,
            };
            string updateCustomerJson = JsonConvert.SerializeObject(updateCustomer);

            postRequest.Content = new StringContent(
                updateCustomerJson,
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage editResponse = client.SendAsync(postRequest).Result;
            if (editResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string data = editResponse.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<CustomerContract>(data);

                Console.WriteLine($"Customer modificato con successo, con Codice: {result.CustomerCode}");
            }
        }

        private static void DeleteCustomer()
        {
            string customerToDelete = string.Empty;
            do
            {
                Console.WriteLine("Scegli codice del customer che vuoi cancellare:");
                customerToDelete = Console.ReadLine();

            } while (string.IsNullOrEmpty(customerToDelete));

            HttpClient client = new HttpClient();
            HttpRequestMessage postRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"https://localhost:44371/api/customer/{customerToDelete}")
            };
            HttpResponseMessage postResponse = client.SendAsync(postRequest).Result;
            if (postResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Customer cancellato!");
            }
        }

        private static void AddNewCustomer()
        {
            string codiceCustomer = GetDataCustomer("codice customer");
            string nameCustomer = GetDataCustomer("name");
            string surnameCustomer = GetDataCustomer("surname");
            List<Order> allOrders = new List<Order>();

            HttpClient client = new HttpClient();
            HttpRequestMessage postRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:44371/api/customer")
            };

            CustomerContract newCustomer = new CustomerContract()
            {
                CustomerCode = codiceCustomer,
                Name = nameCustomer,
                Surname = surnameCustomer,
                orders = allOrders,
            };
            string newCustomerJson = JsonConvert.SerializeObject(newCustomer);

            postRequest.Content = new StringContent(
                newCustomerJson,
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage postResponse = client.SendAsync(postRequest).Result;
            if (postResponse.StatusCode == System.Net.HttpStatusCode.Created)
            {
                string data = postResponse.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<CustomerContract>(data);

                Console.WriteLine($"Customer aggiunto con successo, con Codice: {result.CustomerCode}");
            }
        }

        private static string GetDataCustomer(string data)
        {
            string dataCustomer = string.Empty;
            do
            {
                Console.WriteLine($"Inserisci {data}:");
                dataCustomer = Console.ReadLine();

            } while (string.IsNullOrEmpty(dataCustomer));
            return dataCustomer;
        }

        private static void EditOrder()
        {
            FetchOrders();
            string codiceOrdine = string.Empty;
            do
            {
                Console.WriteLine("Scegli codice ordine che vuoi modificare:");
                codiceOrdine = Console.ReadLine();

            } while (string.IsNullOrEmpty(codiceOrdine));

            HttpClient client = new HttpClient();
            HttpRequestMessage postRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:44371/api/order/{codiceOrdine}")
            };

            var orderCode = GetData("orderCode");
            DateTime dt = GetDataOrder();
            var productCode = GetData("productCode");
            var payment = GetPay();

            OrderContract updatedOrder = new OrderContract()
            {
                OrderCode = orderCode,
                DateOfOrder = dt,
                ProductCode = productCode,
                ToPay = payment,
            };
            string updatedOrderJson = JsonConvert.SerializeObject(updatedOrder);

            postRequest.Content = new StringContent(
                updatedOrderJson,
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage putResponse = client.SendAsync(postRequest).Result;
            if (putResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string data = putResponse.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<OrderContract>(data);

                Console.WriteLine($"Ordine: {result.OrderCode} -> modificato con successo!");
            }
        }

        private static List<OrderContract> FetchOrders()
        {
            HttpClient client = new HttpClient(); 

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://localhost:44371/api/order")
            };

            HttpResponseMessage response = client.SendAsync(request).Result;
            List<OrderContract> result = new List<OrderContract>();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                result = JsonConvert.DeserializeObject<List<OrderContract>>(data);

                foreach (OrderContract o in result)
                {
                    Console.WriteLine($"Id:{o.Id},Order Code: {o.OrderCode},Codice prodotto: {o.ProductCode}, Pay:{o.ToPay}");
                }
            }
            return result;
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

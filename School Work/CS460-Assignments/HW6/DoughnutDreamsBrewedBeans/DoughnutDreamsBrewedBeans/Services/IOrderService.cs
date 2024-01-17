using System.Text.Json;
using DoughnutDreamsBrewedBeans.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoughnutDreamsBrewedBeans.Services
{
    public interface IOrderService
    {
        void CreateOrder(JsonElement userOrder);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stock)
        {
            return new StockDto {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Industry = stock.Industry,
                LastDiv = stock.LastDiv,
                Purchase = stock.Purchase,
                MarketCap = stock.MarketCap,
                Comments = stock.Comments.Select(comment => comment.ToCommentDto()).ToList()

            };
        }

        public static Stock ToStockModel(this CreateStockDto stock)
        {
            return new Stock 
            {   
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Industry = stock.Industry,
                LastDiv = stock.LastDiv,
                Purchase = stock.Purchase,
                MarketCap = stock.MarketCap

            };
        }

        public static Stock ToStockModel(this UpdateStockDto stock)
        {
            return new Stock 
            {   
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Industry = stock.Industry,
                LastDiv = stock.LastDiv,
                Purchase = stock.Purchase,
                MarketCap = stock.MarketCap

            };
        }
    }
}
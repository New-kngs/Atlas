﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtlasDTO;

namespace AtlasMVCAPI.Models
{
    public class CartLine // 카트 rowLine
    {
        public ItemVO Product { get; set; }
        public int Qty { get; set; }
    }

    public class Cart // 카트 Table
    {
        List<CartLine> lines = new List<CartLine>();

        public List<CartLine> Lines
        {
            get { return lines; } // 현재 장바구니 table을 return
        }
        public int AddOnceItem(ItemVO prd, int qty)
        {
            // 목록 중에 추가할 제품과 동일한 제품이 있는지 체크
            CartLine line = lines.Where<CartLine>((p) => p.Product.ItemID.Equals(prd.ItemID)).FirstOrDefault();

            if (line != null) // 수량이 1개면
            {
                return 1; // false
            }
            else // 수량이 0개이면
            {
                lines.Add(new CartLine { Product = prd, Qty = qty });
                // aler('itemID 제품이 카트에 담겼습니다')
                return 2;
            }
        }
        public void AddItem(ItemVO prd, int qty)
        {
            // 목록 중에 추가할 제품과 동일한 제품이 있는지 체크
            CartLine line = lines.Where<CartLine>((p) => p.Product.ItemID.Equals(prd.ItemID)).FirstOrDefault();

            if (line != null)
            {
                line.Qty = line.Qty + qty;
            }
            else
            {
                lines.Add(new CartLine { Product = prd, Qty = qty });
            }
        }

        public void RemoveItem(string prdNo)
        {
            CartLine line = lines.Where<CartLine>((p) => p.Product.ItemID.Equals(prdNo)).FirstOrDefault();

            if (line != null)
            {
                lines.Remove(line);
            }
        }

        public decimal CalcTotalValue()
        {
            return lines.Sum<CartLine>((p) => p.Product.ItemPrice * p.Qty);

            //decimal total = 0;
            //foreach (var item in lines)
            //{
            //    total += (item.Product.ItemPrice * item.Qty);
            //}
            //return total;
        }
    }
}
﻿using dal.models;
using DAL.DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IDal
{
    public interface IItemTag : ICrud<ItemTag>
    {
        public Task<List<ItemTag>> ReadAll(int itemId);
    }
}

﻿using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductDao
{
    interface IProductDao : IGenericDao<Product, Int64>
    {
        Product FindByName(string name);

        Product FindByNameAndCategory(string name, string category);
    }
}

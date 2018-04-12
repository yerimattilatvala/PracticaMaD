using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ModelService
{
    interface IModelService
    {

        long GenerateOrder(long usrId, int cardNumber,int postalAddress,List<ProductDetails> productList);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public interface IRabbitMQAssociationUConsumerController
    {
        public void StartConsuming();
    }
}
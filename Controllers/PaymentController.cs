using Microsoft.AspNetCore.Mvc;
using OCP_Factory_Strategy.BLL.Interfaces;
using System;

namespace OCP_Factory_Strategy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController: ControllerBase
    {
        private readonly Func<int, IPaymentService> _paymentService;

        public PaymentController(Func<int, IPaymentService> paymentService)
        {
            _paymentService = paymentService;
        }

        //Pay by fawry
        //For Test : https://localhost:44323/Payment/DoPayment?paymentType=1
        [HttpGet]
        [Route("DoPayment")]
        public void DoPayment(int paymentType)
        {
            var service = _paymentService(paymentType);
            service.Pay();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReceiptsAPI.Models;


namespace ReceiptsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class receiptsController : ControllerBase
    {

        private ReceiptList _receiptList;

        public receiptsController(ReceiptList receiptList)
        {

            _receiptList = receiptList;
        }

        [HttpPost]
        public ActionResult<Response> receipts(Receipt receipt)
        {
            //Check to make sure that the receipt is valid
            if (ModelState.IsValid && receipt.items?.Count > 0)
            {
                //Generate String GUID
                //Update ID
                //Add to queue
                string guid = Guid.NewGuid().ToString();
                receipt.id = guid;
                _receiptList.Queue?.Add(receipt);


                return new Response{ id = guid};
            }
            else
            {
                //This is if some of the fields are missing
                return BadRequest();
            }


        }


        [HttpGet("{id}/points")]
        public ActionResult<Response> process(string id)
        {

            if(id == null || id == string.Empty)
            {
                //return 400
                return BadRequest("id is corrupted or incorrect");
            }

            var receipt = _receiptList.Queue?.FirstOrDefault(r => r.id == id);

            if(receipt == null)
            {
                //return 500
                return NotFound();
            }

            //Return points associated with response
            return new Response { points = receipt.GetPoints() };

        }

    }
}


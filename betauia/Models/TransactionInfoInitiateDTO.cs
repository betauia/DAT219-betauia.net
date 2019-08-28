using System;
namespace betauia.Models{

    public class TransactionInfoInitiateDTO {
        /*  pattern: ^\d{3,}$
            example: 20000
            Amount in øre. 32 bit Integer (2147483647)  */
        public int amount { get; set; }

        /*  string
            example: order123abc
            pattern: ^[a-zA-Z0-9-]{1,30}$
            maxLength: 30
            Id which uniquely identifies a payment. Maximum length is 30 alphanumeric characters: a-z, A-Z, 0-9 and '-'.    */
        public string orderId { get; set; }

        /*  example: 2018-11-14T15:44:26.590Z
            ISO formatted date time string. */
        DateTime timeStamp { get; set; }

        /*  example: One pair of Vipps socks
            maxLength: 100
            Transaction text to be displayed in Vipps */
        public string transactionText { get; set; }
    }
    

}


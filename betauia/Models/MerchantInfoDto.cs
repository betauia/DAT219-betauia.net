namespace betauia.Models{

    public class MerchantInfoDto{
        /*  maxLength: 255
            example: eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1Ni */
        public string authToken { get; set; }

        /*  maxLength: 255
            example: https://example.com/vipps/callbacks */
        public string callbackPrefix { get; set; }

        /*  maxLength: 255
            example: https://example.com/vipps */
        public string consentRemovalPrefix {get; set; }

        /*  maxLength: 255
            example: https://example.com/vipps/fallback/order123abc */
         public string fallBack {get; set; }

        /*  example: false
            default: false */
         public bool isApp { get; set; }

        /*  minLength: 6
            maxLength: 6
            example: 123456
            pattern: ^\d{6}$ */
         public string merchantSerialNumber { get; set; }

        /*  Enum:
            [ eComm Regular Payment, eComm Express Payment ] */
         public string paymentType { get; set;}

    }

}